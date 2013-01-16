using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Owin.AutoStartup
{
    public static class AutoStartupInfrastructure
    {
        class AutoStartupData
        {
            public Type Type { get; set; }
            public string MethodName { get; set; }
        }


        public static void ExecuteConfigurationMethods(object startup, IAppBuilder app)
        {
            var configurations = startup.GetType().GetMethods()
                .Where(ShouldCallForConfiguration)
                .OrderBy(x => SortableName(x.Name), StringComparer.OrdinalIgnoreCase);

            var configurationArgs = new object[] { app };
            foreach (var configuration in configurations)
            {
                configuration.Invoke(startup, configurationArgs);
            }
        }

        private static string SortableName(string name)
        {
            if (name.StartsWith("Pre", StringComparison.OrdinalIgnoreCase))
            {
                return "0" + name;
            }
            if (name.StartsWith("Post", StringComparison.OrdinalIgnoreCase))
            {
                return "2" + name;
            }
            return "1" + name;
        }

        private static bool ShouldCallForConfiguration(MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters();
            return parameters.Length == 1 &&
                   parameters[0].ParameterType == typeof(IAppBuilder) &&
                   !methodInfo.IsStatic &&
                   methodInfo.ReturnType == typeof(void) &&
                   methodInfo.Name != "AutoConfiguration";
        }


        public static void ExecuteOwinAutoStartups(IServiceProvider serviceProvider, IAppBuilder app)
        {
            var allAssemblies = GetBuildManagerAssemblies() ?? GetAppDomainAssemblies();

            foreach (var pluginData in allAssemblies.SelectMany(AutoStartupDataForAssembly))
            {
                var methodInfo = pluginData.Type.GetMethod(pluginData.MethodName, new[] { typeof(IAppBuilder) });
                var instance = methodInfo.IsStatic ? null : Activate(serviceProvider, pluginData.Type);
                methodInfo.Invoke(instance, new object[] { app });
            }
        }

        private static IEnumerable<AutoStartupData> AutoStartupDataForAssembly(Assembly assembly)
        {
            foreach (var attributeData in assembly.GetCustomAttributesData())
            {
                if (attributeData.Constructor.DeclaringType == null)
                {
                    continue;
                }
                var attributeTypeName = attributeData.Constructor.DeclaringType.Name;
                if (attributeTypeName != "OwinAutoStartup" && attributeTypeName != "OwinAutoStartupAttribute")
                {
                    continue;
                }

                var data = new AutoStartupData();

                data = attributeData.Constructor.GetParameters().Zip(attributeData.ConstructorArguments, (a, b) => new { a, b }).Aggregate(data, (c, d) => AddValue(c, d.a.Name, d.b.ArgumentType, d.b.Value));

                if (attributeData.NamedArguments != null)
                {
                    data = attributeData.NamedArguments.Aggregate(data, (c, d) => AddValue(c, d.MemberInfo.Name, d.TypedValue.ArgumentType, d.TypedValue.Value));
                }

                if (data != null)
                {
                    yield return data;
                }
            }
        }

        private static AutoStartupData AddValue(AutoStartupData data, string name, Type type, object value)
        {
            if (data == null)
            {
                return null;
            }

            if (string.Equals(name, "Type", StringComparison.OrdinalIgnoreCase) &&
                type == typeof(Type) &&
                data.Type == null)
            {
                data.Type = (Type)value;
                return data;
            }

            if (string.Equals(name, "MethodName", StringComparison.OrdinalIgnoreCase) &&
                type == typeof(string) &&
                data.MethodName == null)
            {
                data.MethodName = (String)value;
                return data;
            }

            return null;
        }

        private static IEnumerable<Assembly> GetBuildManagerAssemblies()
        {
            var systemWebAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(x => x.GetName().Name == "System.Web");

            if (systemWebAssembly != null)
            {
                var hostingEnvironmentType = systemWebAssembly.GetType("System.Web.Hosting.HostingEnvironment");
                var isHostedProperty = hostingEnvironmentType.GetProperty("IsHosted");
                var isHosted = (bool)isHostedProperty.GetValue(null, null);

                if (isHosted)
                {
                    var buildManagerType = systemWebAssembly.GetType("System.Web.Compilation.BuildManager");
                    var getReferencedAssembliesMethod = buildManagerType.GetMethod("GetReferencedAssemblies");
                    var referencedAssemblies = (ICollection)getReferencedAssembliesMethod.Invoke(null, null);
                    return referencedAssemblies.Cast<Assembly>();
                }
            }
            return null;
        }

        private static IEnumerable<Assembly> GetAppDomainAssemblies()
        {
            var notYetLoaded = NotYetLoaded(PotentialAssemblyNames());
            var needToLoad = notYetLoaded.Where(NotYetLoadedHasAutoStartupAttribute);
            foreach (var load in needToLoad)
            {
                Assembly.Load(load);
            }
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        private static IEnumerable<string> PotentialAssemblyNames()
        {
            var setupInformation = AppDomain.CurrentDomain.SetupInformation;
            var isProbing = !string.IsNullOrEmpty(setupInformation.PrivateBinPathProbe);
            var assemblyFolder = setupInformation.ApplicationBase;
            if (isProbing)
            {
                assemblyFolder = Path.Combine(assemblyFolder, setupInformation.PrivateBinPath);
            }
            return Directory.EnumerateFiles(assemblyFolder, "*.dll").Select(Path.GetFileNameWithoutExtension);
        }

        private static IEnumerable<string> NotYetLoaded(IEnumerable<string> potentialAssemblyNames)
        {
            var loadedNames = AppDomain.CurrentDomain
                .GetAssemblies()
                .Select(x => x.GetName().Name)
                .ToArray();
            return potentialAssemblyNames
                .Where(name => loadedNames.Any(loadedName => string.Equals(loadedName, name, StringComparison.OrdinalIgnoreCase)));
        }

        private static bool NotYetLoadedHasAutoStartupAttribute(string potentialAssemblyName)
        {
            try
            {
                var assembly = Assembly.ReflectionOnlyLoad(potentialAssemblyName);
                return AutoStartupDataForAssembly(assembly).Any();
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static object SafeGetService(IServiceProvider serviceProvider, Type type)
        {
            try
            {
                return serviceProvider.GetService(type);
            }
            catch
            {
                return null;
            }
        }

        private static object Activate(IServiceProvider serviceProvider, Type type)
        {
            if (serviceProvider != null)
            {
                var service = SafeGetService(serviceProvider, type);
                if (service != null)
                {
                    return service;
                }

                var constructors = type
                    .GetConstructors()
                    .Where(IsInjectable)
                    .ToArray();

                if (constructors.Length == 1)
                {
                    var parameters = constructors[0].GetParameters();
                    var args = new object[parameters.Length];
                    for (int index = 0; index != parameters.Length; ++index)
                    {
                        args[index] = SafeGetService(serviceProvider, parameters[index].ParameterType);
                    }
                    if (args.All(arg => arg != null))
                    {
                        return constructors[0].Invoke(args);
                    }
                }
            }
            return Activator.CreateInstance(type);
        }

        private static bool IsInjectable(ConstructorInfo constructor)
        {
            return constructor.IsPublic && constructor.GetParameters().Length != 0;
        }
    }
}
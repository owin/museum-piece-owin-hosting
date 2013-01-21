// Licensed to Monkey Square, Inc. under one or more contributor 
// license agreements.  See the NOTICE file distributed with 
// this work or additional information regarding copyright 
// ownership.  Monkey Square, Inc. licenses this file to you 
// under the Apache License, Version 2.0 (the "License"); you 
// may not use this file except in compliance with the License.
// You may obtain a copy of the License at 
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.

using System;
using System.Diagnostics.CodeAnalysis;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Owin
{
    /// <summary>
    /// Extension methods for IAppBuilder that provide syntax for commonly supported patterns.
    /// </summary>
    public static partial class StartupExtensions
    {
        /// <summary>
        /// Specifies a middleware instance generator of the given type.
        /// </summary>
        /// <typeparam name="TApp">The applicaiton signature.</typeparam>
        /// <param name="builder"></param>
        /// <param name="middleware">A Func that generates a middleware instance given a reference to the next middleware.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseFunc<TApp>(this IAppBuilder builder, Func<TApp, TApp> middleware)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            return builder.Use(middleware);
        }

        /// <summary>
        /// Specifies a middleware instance generator.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="middleware">A Func that generates a middleware instance given a reference to the next middleware.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseFunc(this IAppBuilder builder, Func<AppFunc, AppFunc> middleware)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            return builder.Use(middleware);
        }

        /// <summary>
        /// Specifies a middleware instance generator that takes one additional argument.
        /// </summary>
        /// <typeparam name="T1">The Type of the given extra argument.</typeparam>
        /// <param name="builder"></param>
        /// <param name="middleware">A Func that generates a middleware instance given a reference to the next middleware and any extra arguments.</param>
        /// <param name="arg1">Extra arguments for the middleware generator.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseFunc<T1>(this IAppBuilder builder, Func<AppFunc, T1, AppFunc> middleware, T1 arg1)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            return builder.UseFunc<AppFunc>(app => middleware(app, arg1));
        }

        /// <summary>
        /// Specifies a middleware instance generator that takes two additional arguments.
        /// </summary>
        /// <typeparam name="T1">The Type of the given extra argument.</typeparam>
        /// <typeparam name="T2">The Type of the given extra argument.</typeparam>
        /// <param name="builder"></param>
        /// <param name="middleware">A Func that generates a middleware instance given a reference to the next middleware and any extra arguments.</param>
        /// <param name="arg1">Extra arguments for the middleware generator.</param>
        /// <param name="arg2">Extra arguments for the middleware generator.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseFunc<T1, T2>(this IAppBuilder builder, Func<AppFunc, T1, T2, AppFunc> middleware, T1 arg1, T2 arg2)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            return builder.UseFunc<AppFunc>(app => middleware(app, arg1, arg2));
        }

        /// <summary>
        /// Specifies a middleware instance generator that takes three additional arguments.
        /// </summary>
        /// <typeparam name="T1">The Type of the given extra argument.</typeparam>
        /// <typeparam name="T2">The Type of the given extra argument.</typeparam>
        /// <typeparam name="T3">The Type of the given extra argument.</typeparam>
        /// <param name="builder"></param>
        /// <param name="middleware">A Func that generates a middleware instance given a reference to the next middleware and any extra arguments.</param>
        /// <param name="arg1">Extra arguments for the middleware generator.</param>
        /// <param name="arg2">Extra arguments for the middleware generator.</param>
        /// <param name="arg3">Extra arguments for the middleware generator.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseFunc<T1, T2, T3>(this IAppBuilder builder, Func<AppFunc, T1, T2, T3, AppFunc> middleware, T1 arg1, T2 arg2, T3 arg3)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            return builder.UseFunc<AppFunc>(app => middleware(app, arg1, arg2, arg3));
        }

        /// <summary>
        /// Specifies a middleware instance generator that takes four additional arguments.
        /// </summary>
        /// <typeparam name="T1">The Type of the given extra argument.</typeparam>
        /// <typeparam name="T2">The Type of the given extra argument.</typeparam>
        /// <typeparam name="T3">The Type of the given extra argument.</typeparam>
        /// <typeparam name="T4">The Type of the given extra argument.</typeparam>
        /// <param name="builder"></param>
        /// <param name="middleware">A Func that generates a middleware instance given a reference to the next middleware and any extra arguments.</param>
        /// <param name="arg1">Extra arguments for the middleware generator.</param>
        /// <param name="arg2">Extra arguments for the middleware generator.</param>
        /// <param name="arg3">Extra arguments for the middleware generator.</param>
        /// <param name="arg4">Extra arguments for the middleware generator.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseFunc<T1, T2, T3, T4>(this IAppBuilder builder, Func<AppFunc, T1, T2, T3, T4, AppFunc> middleware, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            return builder.UseFunc<AppFunc>(app => middleware(app, arg1, arg2, arg3, arg4));
        }

        /// <summary>
        /// Specifies a middleware instance generator that takes one additional argument.
        /// </summary>
        /// <typeparam name="T1">The Type of the given extra argument.</typeparam>
        /// <param name="builder"></param>
        /// <param name="middleware">A Func that generates a middleware instance given a reference to the next middleware and any extra arguments.</param>
        /// <param name="arg1">Extra arguments for the middleware generator.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseFunc<T1>(this IAppBuilder builder, Func<T1, Func<AppFunc, AppFunc>> middleware, T1 arg1)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            return builder.UseFunc<AppFunc>(app => middleware(arg1)(app));
        }

        /// <summary>
        /// Specifies a middleware instance generator that takes two additional arguments.
        /// </summary>
        /// <typeparam name="T1">The Type of the given extra argument.</typeparam>
        /// <typeparam name="T2">The Type of the given extra argument.</typeparam>
        /// <param name="builder"></param>
        /// <param name="middleware">A Func that generates a middleware instance given a reference to the next middleware and any extra arguments.</param>
        /// <param name="arg1">Extra arguments for the middleware generator.</param>
        /// <param name="arg2">Extra arguments for the middleware generator.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseFunc<T1, T2>(this IAppBuilder builder, Func<T1, T2, Func<AppFunc, AppFunc>> middleware, T1 arg1, T2 arg2)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            return builder.UseFunc<AppFunc>(app => middleware(arg1, arg2)(app));
        }

        /// <summary>
        /// Specifies a middleware instance generator that takes three additional arguments.
        /// </summary>
        /// <typeparam name="T1">The Type of the given extra argument.</typeparam>
        /// <typeparam name="T2">The Type of the given extra argument.</typeparam>
        /// <typeparam name="T3">The Type of the given extra argument.</typeparam>
        /// <param name="builder"></param>
        /// <param name="middleware">A Func that generates a middleware instance given a reference to the next middleware and any extra arguments.</param>
        /// <param name="arg1">Extra arguments for the middleware generator.</param>
        /// <param name="arg2">Extra arguments for the middleware generator.</param>
        /// <param name="arg3">Extra arguments for the middleware generator.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseFunc<T1, T2, T3>(this IAppBuilder builder, Func<T1, T2, T3, Func<AppFunc, AppFunc>> middleware, T1 arg1, T2 arg2, T3 arg3)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            return builder.UseFunc<AppFunc>(app => middleware(arg1, arg2, arg3)(app));
        }

        /// <summary>
        /// Specifies a middleware instance generator that takes four additional arguments.
        /// </summary>
        /// <typeparam name="T1">The Type of the given extra argument.</typeparam>
        /// <typeparam name="T2">The Type of the given extra argument.</typeparam>
        /// <typeparam name="T3">The Type of the given extra argument.</typeparam>
        /// <typeparam name="T4">The Type of the given extra argument.</typeparam>
        /// <param name="builder"></param>
        /// <param name="middleware">A Func that generates a middleware instance given a reference to the next middleware and any extra arguments.</param>
        /// <param name="arg1">Extra arguments for the middleware generator.</param>
        /// <param name="arg2">Extra arguments for the middleware generator.</param>
        /// <param name="arg3">Extra arguments for the middleware generator.</param>
        /// <param name="arg4">Extra arguments for the middleware generator.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseFunc<T1, T2, T3, T4>(this IAppBuilder builder, Func<T1, T2, T3, T4, Func<AppFunc, AppFunc>> middleware, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            return builder.UseFunc<AppFunc>(app => middleware(arg1, arg2, arg3, arg4)(app));
        }
    }
}

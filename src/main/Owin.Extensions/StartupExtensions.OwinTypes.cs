// <copyright file="StartupExtensions.OwinTypes.cs" company="Microsoft Open Technologies, Inc.">
// Copyright 2013 Microsoft Open Technologies, Inc. All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Owin.Types;

namespace Owin
{
    public static partial class StartupExtensions
    {
        private static readonly Task CompletedTask = MakeCompletedTask();

        private static Task MakeCompletedTask()
        {
            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(null);
            return tcs.Task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Relayed to callback")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseHandler(
            this IAppBuilder builder,
            Action<OwinRequest, OwinResponse> handler)
        {
            return builder.UseFunc(
                next => env =>
                    {
                        try
                        {
                            handler(
                                new OwinRequest(env),
                                new OwinResponse(env));
                            return CompletedTask;
                        }
                        catch (Exception ex)
                        {
                            var tcs = new TaskCompletionSource<object>();
                            tcs.SetException(ex);
                            return tcs.Task;
                        }
                    });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseHandlerAsync(
            this IAppBuilder builder,
            Func<OwinRequest, OwinResponse, Task> handler)
        {
            return UseHandler(builder, handler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseHandler(
            this IAppBuilder builder,
            Func<OwinRequest, OwinResponse, Task> handler)
        {
            return builder.UseFunc(
                next => env => handler(
                    new OwinRequest(env),
                    new OwinResponse(env)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static IAppBuilder UseHandlerAsync(
            this IAppBuilder builder,
            Func<OwinRequest, OwinResponse, Func<Task>, Task> handler)
        {
            return UseHandler(builder, handler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseHandler(
            this IAppBuilder builder,
            Func<OwinRequest, OwinResponse, Func<Task>, Task> handler)
        {
            return builder.UseFunc(
                next => env => handler(
                    new OwinRequest(env),
                    new OwinResponse(env),
                    () => next(env)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseFilter(
            this IAppBuilder builder,
            Action<OwinRequest> filter)
        {
            return builder.UseFunc(
                next => env =>
                {
                    filter(new OwinRequest(env));
                    return next(env);
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IAppBuilder UseFilterAsync(
            this IAppBuilder builder,
            Func<OwinRequest, Task> filter)
        {
            return UseFilter(builder, filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Exception produces faulted task")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseFilter(
            this IAppBuilder builder,
            Func<OwinRequest, Task> filter)
        {
            return builder.UseFunc(
                next => env =>
                {
                    var task = filter(new OwinRequest(env));
                    if (task.IsCompleted)
                    {
                        if (task.IsFaulted || task.IsCanceled)
                        {
                            return task;
                        }
                        return next(env);
                    }

                    var syncContext = SynchronizationContext.Current;
                    return task.ContinueWith(t =>
                        {
                            if (t.IsFaulted || t.IsCanceled)
                            {
                                return t;
                            }
                            if (syncContext == null)
                            {
                                return next(env);
                            }
                            var tcs = new TaskCompletionSource<Task>();
                            syncContext.Post(_ =>
                                {
                                    try
                                    {
                                        tcs.TrySetResult(next(env));
                                    }
                                    catch (Exception ex)
                                    {
                                        tcs.TrySetException(ex);
                                    }
                                }, null);
                            return tcs.Task.Unwrap();
                        }, TaskContinuationOptions.ExecuteSynchronously).Unwrap();
                });
        }
    }
}

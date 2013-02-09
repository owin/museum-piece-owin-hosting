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
using System.Threading;
using System.Threading.Tasks;
using Owin.Types;

namespace Owin
{
    public static partial class StartupExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public delegate void OwinHandler(OwinRequest request, OwinResponse response);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public delegate Task OwinHandlerAsync(OwinRequest request, OwinResponse response);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="next"></param>
        public delegate Task OwinHandlerChained(OwinRequest request, OwinResponse response, Func<Task> next);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        public delegate void OwinFilter(OwinRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        public delegate Task OwinFilterAsync(OwinRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseHandler(
            this IAppBuilder builder,
            OwinHandler handler)
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
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseHandler(
            this IAppBuilder builder,
            OwinHandlerAsync handler)
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
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseHandler(
            this IAppBuilder builder,
            OwinHandlerChained handler)
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
        /// <param name="process"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseFilter(
            this IAppBuilder builder,
            OwinFilter filter)
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
        /// <param name="process"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Exception produces faulted task")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseFilter(
            this IAppBuilder builder,
            OwinFilterAsync filter)
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

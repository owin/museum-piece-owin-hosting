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
using System.Threading.Tasks;
using Owin.Types;

namespace Owin
{
    public static partial class StartupExtensions
    {
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseOwin(
            this IAppBuilder builder,
            Func<OwinRequest, OwinResponse, Func<Task>, Task> process)
        {
            return builder.UseFunc(
                next => env => process(
                    new OwinRequest(env),
                    new OwinResponse(env),
                    () => next(env)));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseOwin(
            this IAppBuilder builder,
            Action<OwinRequest> process)
        {
            return builder.UseFunc(
                next => env =>
                {
                    process(new OwinRequest(env));
                    return next(env);
                });
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        public static IAppBuilder UseOwin(
            this IAppBuilder builder,
            Func<OwinRequest, Task> process)
        {
            return builder.UseFunc(
                next => env =>
                {
                    var task = process(new OwinRequest(env));
                    if (task.IsCompleted)
                    {
                        if (task.IsFaulted || task.IsCanceled)
                        {
                            return task;
                        }
                        return next(env);
                    }
                    return task.ContinueWith(t => next(env), TaskContinuationOptions.OnlyOnRanToCompletion);
                });
        }
    }
}

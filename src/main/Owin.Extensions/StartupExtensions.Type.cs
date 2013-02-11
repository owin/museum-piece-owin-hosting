// <copyright file="StartupExtensions.Type.cs" company="Microsoft Open Technologies, Inc.">
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

namespace Owin
{
    /// <summary>
    /// Extension methods for IAppBuilder that provide syntax for commonly supported patterns.
    /// </summary>
    public static partial class StartupExtensions
    {
        /// <summary>
        /// Adds an instance of the given middleware type to the pipeline using the constructor that takes
        /// an application delegate as the first parameter and the given params args for any remaining parameters.
        /// </summary>
        /// <typeparam name="TMiddleware">The Type of the middleware to construct.</typeparam>
        /// <param name="builder"></param>
        /// <param name="args">Any additional arguments to pass into the middleware constructor.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "By design")]
        public static IAppBuilder UseType<TMiddleware>(this IAppBuilder builder, params object[] args)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            return builder.Use(typeof(TMiddleware), args);
        }

        /// <summary>
        /// Adds an instance of the given middleware type to the pipeline using the constructor that takes
        /// an application delegate as the first parameter and the given params args for any remaining parameters.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="type">The Type of the middleware to construct.</param>
        /// <param name="args">Any additional arguments to pass into the middleware constructor.</param>
        /// <returns></returns>
        public static IAppBuilder UseType(this IAppBuilder builder, Type type, params object[] args)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return builder.Use(type, args);
        }
    }
}

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
using System.Collections.Generic;
using System.IO;

namespace Utils
{
    public class OwinHelper
    {
        public IDictionary<string, object> Env { get; private set; }

        public OwinHelper(IDictionary<string, object> env)
        {
            Env = env;
        }

        public OwinHelper()
        {
            Env = new Dictionary<string, object>
            {
                {"owin.RequestHeaders",new Dictionary<string,string[]>(StringComparer.OrdinalIgnoreCase)},
                {"owin.ResponseHeaders",new Dictionary<string,string[]>(StringComparer.OrdinalIgnoreCase)},
            };
        }

        public string RequestPath
        {
            get { return Get<string>("owin.RequestPath"); }
        }

        public IDictionary<string, string[]> ResponseHeaders
        {
            get { return Get<IDictionary<string, string[]>>("owin.ResponseHeaders"); }
        }

        public Stream OutputStream
        {
            get { return Get<Stream>("owin.ResponseBody"); }
        }

        public int ResponseStatusCode
        {
            get { return Get<int>("owin.ResponseStatusCode"); }
        }

        T Get<T>(string key)
        {
            object value;
            return Env.TryGetValue(key, out value) ? (T)value : default(T);
        }
    }
}

// <copyright file="AssemblyInfo.cs" company="Microsoft Open Technologies, Inc.">
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
using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Owin.Types")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("dd53aa0b-5d4b-4efd-9d44-094ed213c992")]

[assembly: AssemblyVersion("0.8.5")]
[assembly: AssemblyFileVersion("0.8.5")]
[assembly: AssemblyInformationalVersion("0.8.5")]
[assembly: CLSCompliant(true)]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Owin.Types", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Owin.Types.Opaque", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Owin.Types.WebSockets", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Owin.Types.AppBuilder", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Owin.Types.OwinRequest.#AcceptDelegate", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Owin.Types.OwinRequest.#OnSendingHeaders", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Owin.Types.OwinRequest.#UpgradeDelegate", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Owin.Types.OwinResponse.#SendFileAsyncDelegate", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Owin.Types.WebSockets.OwinWebSocket.#ReceiveAsyncDelegate", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Owin.Types.WebSockets.OwinWebSocket.#SendAsyncDelegate", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Owin.Types.AppBuilder.AppProperties.#DefaultApp", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Owin.Types.AppBuilder.AddressCollection.#.ctor(System.Collections.Generic.IList`1<System.Collections.Generic.IDictionary`2<System.String,System.Object>>)", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Owin.Types.AppBuilder.AddressCollection.#List", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Owin.Types.OwinConstants+CommonKeys", Justification = "Nesting groups constants")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Owin.Types.OwinConstants+OpaqueConstants", Justification = "Nesting groups constants")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Owin.Types.OwinConstants+SendFiles", Justification = "Nesting groups constants")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Owin.Types.OwinConstants+WebSocket", Justification = "Nesting groups constants")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Owin.Types.OwinConstants+Builder", Justification = "Nesting groups constants")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ip", Scope = "member", Target = "Owin.Types.OwinConstants+CommonKeys.#LocalIpAddress", Justification = "Technology name")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ip", Scope = "member", Target = "Owin.Types.OwinConstants+CommonKeys.#RemoteIpAddress", Justification = "Technology name")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ip", Scope = "member", Target = "Owin.Types.OwinRequest.#LocalIpAddress", Justification = "Technology name")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ip", Scope = "member", Target = "Owin.Types.OwinRequest.#RemoteIpAddress", Justification = "Technology name")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Ip", Scope = "member", Target = "Owin.Types.OwinConstants+CommonKeys.#LocalIpAddress", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Ip", Scope = "member", Target = "Owin.Types.OwinConstants+CommonKeys.#RemoteIpAddress", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Ip", Scope = "member", Target = "Owin.Types.OwinRequest.#LocalIpAddress", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Ip", Scope = "member", Target = "Owin.Types.OwinRequest.#RemoteIpAddress", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Cancelled", Scope = "member", Target = "Owin.Types.OwinConstants.#CallCancelled", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Cancelled", Scope = "member", Target = "Owin.Types.OwinConstants+OpaqueConstants.#CallCancelled", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Cancelled", Scope = "member", Target = "Owin.Types.OwinConstants+WebSocket.#CallCancelled", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Cancelled", Scope = "member", Target = "Owin.Types.Opaque.OwinOpaque.#CallCancelled", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Cancelled", Scope = "member", Target = "Owin.Types.OwinRequest.#CallCancelled", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Cancelled", Scope = "member", Target = "Owin.Types.OwinResponse.#CallCancelled", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Cancelled", Scope = "member", Target = "Owin.Types.WebSockets.OwinWebSocket.#CallCancelled", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SendAsync", Scope = "member", Target = "Owin.Types.OwinResponse.#SendFileAsync(System.String,System.Int64,System.Nullable`1<System.Int64>,System.Threading.CancellationToken)", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "sendfile", Scope = "member", Target = "Owin.Types.OwinResponse.#SendFileAsync(System.String,System.Int64,System.Nullable`1<System.Int64>,System.Threading.CancellationToken)", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "websocket", Scope = "member", Target = "Owin.Types.OwinRequest.#Accept(Owin.Types.WebSockets.OwinWebSocketParameters,System.Func`2<Owin.Types.WebSockets.OwinWebSocket,System.Threading.Tasks.Task>)", Justification = "Per specification")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Owin.Types.OwinRequest.#Headers", Justification = "Collection may be assigned")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Owin.Types.OwinResponse.#Headers", Justification = "Collection may be assigned")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2243:AttributeStringLiteralsShouldParseCorrectly", Justification = "SemVer used for FileInformationalVersion")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Owin.Types.Helpers", Justification = "Namespace used to isolate optional components")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Owin.Types.Extensions", Justification = "Namespace used to isolate optional components")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Scope = "member", Target = "Owin.Types.Helpers.HeaderSegmentCollection+Enumerator.#MoveNext()", Justification = "Heapless iterator is complex")]
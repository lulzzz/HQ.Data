#region LICENSE

// Unless explicitly acquired and licensed from Licensor under another
// license, the contents of this file are subject to the Reciprocal Public
// License ("RPL") Version 1.5, or subsequent versions as allowed by the RPL,
// and You may not copy or use this file in either source code or executable
// form, except in compliance with the terms and conditions of the RPL.
// 
// All software distributed under the RPL is provided strictly on an "AS
// IS" basis, WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESS OR IMPLIED, AND
// LICENSOR HEREBY DISCLAIMS ALL SUCH WARRANTIES, INCLUDING WITHOUT
// LIMITATION, ANY WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE, QUIET ENJOYMENT, OR NON-INFRINGEMENT. See the RPL for specific
// language governing rights and limitations under the RPL.

#endregion

using System;
using System.Collections.Generic;
using System.Net;
using FastMember;
using HQ.Common.Extensions;

namespace HQ.Data.Contracts
{
    internal static class FieldValidations
    {
        public static List<Error> MustExistOnType<T>(SelfEnumerable<string> fields)
        {
            return MustExistOnType(typeof(T), fields);
        }

        public static List<Error> MustExistOnType(Type type, SelfEnumerable<string> fields)
        {
            var accessor = TypeAccessor.Create(type);
            var members = accessor.GetMembers();

            var list = new List<Error>();
            foreach (var field in fields)
            {
                var valid = false;
                foreach (var member in members)
                    if (field.Equals(member.Name, StringComparison.OrdinalIgnoreCase))
                        valid = true;
                if (!valid)
                    list.Add(new Error(ErrorEvents.FieldDoesNotMatch,
                        string.Format(ErrorStrings.FieldToPropertyMismatch, field, type.Name),
                        HttpStatusCode.BadRequest));
            }

            return list;
        }

        public static List<Error> MustExistOnType<T>(FuncEnumerable<T, string> fields)
        {
            var type = typeof(T);
            var accessor = TypeAccessor.Create(type);
            var members = accessor.GetMembers();

            var list = new List<Error>();
            foreach (var field in fields)
            {
                var valid = false;
                foreach (var member in members)
                    if (field.Equals(member.Name, StringComparison.OrdinalIgnoreCase))
                        valid = true;
                if (!valid)
                    list.Add(new Error(ErrorEvents.FieldDoesNotMatch,
                        string.Format(ErrorStrings.FieldToPropertyMismatch, field, type.Name),
                        HttpStatusCode.BadRequest));
            }

            return list;
        }
    }
}

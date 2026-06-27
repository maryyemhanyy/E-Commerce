using E_Commerce.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public sealed record Error(string code , string description , ErrorType ErrorType = ErrorType.Failure)
    {
        public static Error Failure(string code = "General.Failure" , string description = "A general error has happened")
                                     => new Error(code, description , ErrorType.Failure);

        public static Error Validation(string code = "General.Validation", string description = "A validation error has happened")
                                     => new Error(code, description, ErrorType.Validation);

        public static Error NotFound(string code = "General.NotFound", string description = "Data not found")
                                     => new Error(code, description, ErrorType.NotFound);

        public static Error Conflict(string code = "General.Conflict", string description = "A conflict error has happened")
                                     => new Error(code, description, ErrorType.Conflict);

        public static Error Unauthorized(string code = "General.Unauthorized", string description = "Access is denied")
                                     => new Error(code, description, ErrorType.Unauthorized);

        public static Error Forbidden(string code = "General.Forbidden", string description = "The operation is forbidden")
                                     => new Error(code, description, ErrorType.Forbidden);

        public static Error InvalidCredentails(string code = "General.InvalidCredentails", string description = "The provided credentails are not valid")
                                     => new Error(code, description, ErrorType.InvalidCredentails);
    }
}

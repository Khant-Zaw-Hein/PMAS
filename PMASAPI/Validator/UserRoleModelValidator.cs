using FluentValidation;
using PMASAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMASAPI.Validator
{
    public class UserRoleModelValidator : AbstractValidator<RolePermissionViewModel>
    {
        public UserRoleModelValidator()
        {
            RuleFor(data => data.Name).NotNull().NotEmpty();
            RuleFor(data => data.Description).NotNull().NotEmpty();
            RuleFor(data => data.CurrentUserID).NotNull().NotEmpty();
        }
    }
    public class UserRoleUpdateModelValidator : AbstractValidator<RolePermissionViewModel>
    {
        public UserRoleUpdateModelValidator()
        {
            RuleFor(data => data.ID).NotNull().NotEmpty();
            RuleFor(data => data.Name).NotNull().NotEmpty();
            RuleFor(data => data.Description).NotNull().NotEmpty();
            RuleFor(data => data.CurrentUserID).NotNull().NotEmpty();
        }
    }
}
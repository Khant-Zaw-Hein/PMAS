using FluentValidation;
using PMASAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMASAPI.Validator
{
    public class UserAccountModelValidator : AbstractValidator<UserAccountCreateModel>
    {
        public UserAccountModelValidator()
        {
            RuleFor(data => data.LoginID).NotNull().NotEmpty().Must(id => !id.Contains(" "))
                                        .WithMessage("User account ID cannot contain spaces"); 
            RuleFor(data => data.Password).NotNull().NotEmpty();
            RuleFor(data => data.Name).NotNull().NotEmpty();
            RuleFor(data => data.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(data => data.CurrentUserID).NotNull().NotEmpty();
            RuleFor( x => x.Department_Id).NotNull().NotEmpty();
            RuleFor( x => x.Designation_Id).NotNull().NotEmpty();
            RuleFor( x => x.Rank_Id).NotNull().NotEmpty();
            RuleFor(x => x.Role_Ids).Must(x => x != null && x.Any())
                                    .WithMessage("At least one Role_Id is required.");
            //RuleFor(x => x.LastDayDate).GreaterThan(x => x.JoinDate).WithMessage("Last day of employment cannot be earlier than Join Date.");

        }
    }
    public class UserAccountUpdateModelValidator : AbstractValidator<UserAccountCreateModel>
    {
        public UserAccountUpdateModelValidator()
        {
            RuleFor(data => data.UserAccountID).NotNull().NotEmpty();
            RuleFor(data => data.LoginID).NotNull().NotEmpty();
            RuleFor(data => data.Password).NotNull().NotEmpty();
            RuleFor(data => data.Name).NotNull().NotEmpty();
            RuleFor(data => data.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(data => data.CurrentUserID).NotNull().NotEmpty();
            RuleFor(x => x.Department_Id).NotNull().NotEmpty();
            RuleFor(x => x.Designation_Id).NotNull().NotEmpty();
            RuleFor(x => x.Rank_Id).NotNull().NotEmpty();
            RuleFor(x => x.Role_Ids).Must(x => x != null && x.Any()).WithMessage("At least one Role_Id is required.");
        }
    }
}
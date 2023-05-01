using FluentValidation;
using PMASAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMASAPI.Validator
{
    public class LoginModelValidator : AbstractValidator<LoginModel.Request>
    {
        public LoginModelValidator()
        {
            RuleFor(data => data.LoginID).NotNull().NotEmpty().WithMessage("LoginId must not be empty");
            RuleFor(data => data.Password).NotNull().NotEmpty().WithMessage("Password must not be empty");
        }
    }
}
using FluentValidation;
using PMASAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMASAPI.Validator
{
    public class MenuModelValidator : AbstractValidator<MenuCreateModel>
    {
        public MenuModelValidator()
        {
            RuleFor(data => data.Name).NotNull().NotEmpty().WithMessage("Name must not be empty");
            RuleFor(data => data.ProgramID).NotNull().NotEmpty().WithMessage("ProgramID must not be empty");
            RuleFor(data => data.MenuLevel).NotNull().NotEmpty().WithMessage("MenuLevel must not be empty");
            RuleFor(data => data.CurrentUserID).NotNull().NotEmpty().WithMessage("Created User Id code must not be empty");
        }
    }
    public class MenuUpdateModelValidator : AbstractValidator<MenuCreateModel>
    {
        public MenuUpdateModelValidator()
        {
            RuleFor(data => data.ID).NotNull().NotEmpty().WithMessage("Menu id must not be empty");
            RuleFor(data => data.Name).NotNull().NotEmpty().WithMessage("Name must not be empty");
            RuleFor(data => data.ProgramID).NotNull().NotEmpty().WithMessage("ProgramID must not be empty");
            RuleFor(data => data.MenuLevel).NotNull().NotEmpty().WithMessage("MenuLevel must not be empty");
            RuleFor(data => data.CurrentUserID).NotNull().NotEmpty().WithMessage("Created User Id code must not be empty");
        }
    }
}
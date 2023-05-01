using FluentValidation;
using PMASAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMASAPI.Validator
{
    public class ProgramModelValidator : AbstractValidator<ProgramCreateModel>
    {
        public ProgramModelValidator()
        {
            RuleFor(data => data.Name).NotNull().NotEmpty().WithMessage("Name must not be empty");
            RuleFor(data => data.ProgramURL).NotNull().NotEmpty().WithMessage("ProgramURL must not be empty");
        }
    }
    public class ProgramUpdateModelValidator : AbstractValidator<ProgramCreateModel>
    {
        public ProgramUpdateModelValidator()
        {
            RuleFor(data => data.ID).NotNull().NotEmpty().WithMessage("Program id must not be empty");
            RuleFor(data => data.Name).NotNull().NotEmpty().WithMessage("Name must not be empty");
            RuleFor(data => data.ProgramURL).NotNull().NotEmpty().WithMessage("ProgramURL must not be empty");
        }
    }
}
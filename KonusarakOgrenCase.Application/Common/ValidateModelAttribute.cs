﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KonusarakOgrenCase.Application.Common;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Select(m => new
            {
                m.Key,
                Errors = m.Value.Errors.Select(x => x.ErrorMessage)
            }).ToList();

            context.Result = new BadRequestObjectResult(new ApiResponse
            {
                Message = "Given data not validated.",
                InternalMessage = "One or many validation errors.",
                Data = errors,
                Errors = errors.SelectMany(s => s.Errors).ToList() //same
            });
        }

        base.OnActionExecuting(context);
    }
}
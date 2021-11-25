﻿using Microsoft.AspNetCore.Mvc;
using MSBase.Core.Application.Mediator;

namespace MSBase.Core.API
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleMediatorResult(IMediatorResult mediatorResult)
        {
            if (mediatorResult is null)
            {
                return NotFound();
            }

            if (!mediatorResult.IsValid())
            {
                return BadRequest(new { mediatorResult.Errors });
            }

            return Ok(mediatorResult);
        }
    }
}
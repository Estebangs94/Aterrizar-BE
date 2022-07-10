using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Aterrizar.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    [Route("/error")]
    public IActionResult Problem(IError error)
    {
        return Problem(detail: error.Message, statusCode: (int)error.Metadata["StatusCode"]);
    }
}
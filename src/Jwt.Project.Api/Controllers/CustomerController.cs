using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Jwt.Project.Application.Commands.CreateCustomer;
using Jwt.Project.Application.Commands.LoginCustomer;
using Jwt.Project.Application.Commands.UpdateCustomer;
using Jwt.Project.Application.Querys.GetCustomerByName;
using Jwt.Project.Domain.Enums;
using Jwt.Project.Domain.Interfaces.Services;
using Jwt.Project.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Toolkit.Services;

namespace Jwt.Project.Api.Controllers;

[Route("[controller]")]
[ApiController]
public sealed class CustomerController : ControllerBase
{
    private IMediator _mediator;
    private ILogger<CustomerController> _logger;
    private ITokenService _tokenService;

    public CustomerController(IMediator mediator, ILogger<CustomerController> logger, ITokenService tokenService)
    {
        _mediator = mediator;
        _logger = logger;
        _tokenService = tokenService;
    }

    [HttpPost(nameof(CreateCustomer))]
    [AllowAnonymous]
    public async Task<ActionResult<ResponseBase>> CreateCustomer(CreateCustomerCommand request)
    {
        try
        {
            _logger.LogInformation("Start {name}.", nameof(CreateCustomer));
            var response = await _mediator.Send(request);
            _logger.LogInformation("{name} Success - Name: {customerName}, Login: {customerLogin}", 
                nameof(CreateCustomer), 
                request.Name, 
                request.Login);
            return Created("Sucess.", ResponseBase.GenerateResponse(data: response));
        }
        catch(Exception ex)
        {
            _logger.LogError("Error in {CreateCustomer}: {ex}", nameof(CreateCustomer), ex);  
            var response = ResponseBase.GenerateResponse(errors: [ex.Message]);
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

   [HttpPost(nameof(LoginCustomer))]
   [AllowAnonymous]
   public async Task<ActionResult<ResponseBase>> LoginCustomer(LoginCustomerCommand request)
   {
        try
        {
            _logger.LogInformation("Start {name} trying login {customerlogin}", nameof(LoginCustomer), request.Login);
            var response = await _mediator.Send(request);
            _logger.LogInformation("Login {customerLogin} Success.", request.Login);
            return Ok(ResponseBase.GenerateResponse(data: response));
        }
        catch(UserNotFoundException ex)
        {
            _logger.LogError("{name} - Customer Not Found.", nameof(LoginCustomer));
            return NotFound(ResponseBase.GenerateResponse(errors: [ex.Message])); 
        }
        catch(AuthenticationException ex)
        {
            _logger.LogError("{name} - Invalid Password.", nameof(LoginCustomer));
            return BadRequest(ResponseBase.GenerateResponse(errors: [ex.Message]));
        }
        catch(Exception ex)
        {
            _logger.LogError("Error in {LoginCustomer}: {ex}", nameof(LoginCustomer), ex);  
            var response = ResponseBase.GenerateResponse(errors: [ex.Message]);
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
   }

   [HttpPut(nameof(UpdateCustomerPassword))]
   [Authorize]
   public async Task<ActionResult<ResponseBase>> UpdateCustomerPassword([FromQuery] string login, UpdateCustomerPasswordCommand request)
   {
        try
        {
            _logger.LogInformation("Start {name} trying login {customerlogin}", nameof(UpdateCustomerPassword), request.Login);
            request.Login = login;
            var response = await _mediator.Send(request);
            _logger.LogInformation("Login {customerLogin} Success.", request.Login);
            return Ok(ResponseBase.GenerateResponse(data: response));
        }
        catch(UserNotFoundException ex)
        {
            _logger.LogError("{name} - Customer Not Found.", nameof(UpdateCustomerPassword));
            return NotFound(ResponseBase.GenerateResponse(errors: [ex.Message])); 
        }
        catch(Exception ex)
        {
            _logger.LogError("Error in {UpdateCustomer}: {ex}", nameof(UpdateCustomerPassword), ex);  
            var response = ResponseBase.GenerateResponse(errors: [ex.Message]);
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
   }

   [HttpGet(nameof(GetCustomerByName))]
   [Authorize(Roles = "Premium")]
   public async Task<ActionResult<ResponseBase>> GetCustomerByName([FromQuery]GetCustomerByNameQuery request)
   {
        try
        {
            _logger.LogInformation("Start {methodName} trying login {customerName}", nameof(GetCustomerByName), request.Name);
            var response = await _mediator.Send(request);
            _logger.LogInformation("Feach {customerName} Success.", request.Name);
            return Ok(ResponseBase.GenerateResponse(data: response));
        }
        catch(UserNotFoundException ex)
        {
            _logger.LogError("{name} - Customer Not Found.", nameof(GetCustomerByName));
            return NotFound(ResponseBase.GenerateResponse(errors: [ex.Message])); 
        }
        catch(Exception ex)
        {
            _logger.LogError("Error in {methodName}: {ex}", nameof(GetCustomerByName), ex);  
            var response = ResponseBase.GenerateResponse(errors: [ex.Message]);
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
   }
    
}

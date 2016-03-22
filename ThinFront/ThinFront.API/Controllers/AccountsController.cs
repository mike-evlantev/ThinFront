using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ThinFront.Core.Infrastructure;
using ThinFront.Core.Models;

namespace ThinFront.API.Controllers
{
    public class AccountsController : ApiController
    {
        private IAuthorizationRepository _authorizationRepository;

        public AccountsController(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        // Register a Customer
        [AllowAnonymous]
        [Route("api/accounts/register/customers")]
        public async Task<IHttpActionResult> RegisterCustomer(RegistrationsModel.Customer registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authorizationRepository.RegisterCustomer(registration);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Registration form is invalid");
            }
        }

        // Register a Reseller

        [AllowAnonymous]
        // [Authorize(Roles = "Reseller")]
        [Route("api/accounts/register/resellers")]
        public async Task<IHttpActionResult> RegisterReseller(RegistrationsModel.Reseller registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authorizationRepository.RegisterReseller(registration);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Registration form is invalid");
            }
        }

        // Register a Supplier

        [AllowAnonymous]
        // [Authorize(Roles = "Supplier")]
        [Route("api/accounts/register/suppliers")]
        public async Task<IHttpActionResult> RegisterSupplier(RegistrationsModel.Supplier registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authorizationRepository.RegisterSupplier(registration);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Registration form is invalid");
            }
        }
    }
}

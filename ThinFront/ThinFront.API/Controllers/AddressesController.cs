using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ThinFront.API.Infrastructure;
using ThinFront.Core.Domain;
using ThinFront.Core.Infrastructure;
using ThinFront.Core.Models;
using ThinFront.Core.Repository;

namespace ThinFront.API.Controllers
{
    [Authorize]
    public class AddressesController : BaseApiController
    {
        private IAddressRepository _addressRepository;
        private IAddressTypeRepository _addressTypeRepository;
        private IUnitOfWork _unitOfWork;

        public AddressesController(IAddressRepository addressRepository, IAddressTypeRepository addressTypeRepository, IThinFrontUserRepository userRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _addressRepository = addressRepository;
            _addressTypeRepository = addressTypeRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Addresses/AddressType/5
        
        // GET: api/Addresses/user
        [Route("api/addresses/user")]
        public AddressesModel GetAddressForCurrentUser()
        {
            // var currentUser = _userRepository.GetFirstOrDefault(u => u.UserName == CurrentUser.UserName);
            var dbAddress = _addressRepository.GetWhere(a => a.User.UserName == CurrentUser.UserName);

            if(dbAddress == null)
            {
                return NotFound();
            }
            else
            {
                return Mapper.Map<AddressesModel>(dbAddress);
            }
        }

        // GET: api/Addresses/5/AddressType/5

        // GET: api/Addresses/5
        [ResponseType(typeof(AddressesModel))]
        public IHttpActionResult GetAddress (int id)
        {
            Address dbAddress = _addressRepository.GetFirstOrDefault(a => a.User.UserName == CurrentUser.UserName && a.AddressId == id);
            if (dbAddress == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<AddressesModel>(dbAddress));
        }

        // PUT: api/Addresses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAddress(int id, AddressesModel address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // ???What does this do in the PUT method???
            Address dbAddress = _addressRepository.GetFirstOrDefault(a => a.User.UserName == CurrentUser.UserName && a.AddressId == id);

            if (id != address.AddressId)
            {
                return BadRequest();
            }
            dbAddress.Update(address);
            _addressRepository.Update(dbAddress);
        }

        // POST: api/Addresses/5

        // DELETE: api/Address/5
    }
}

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

        // GET: api/AddressType/5/Addresses
        [Route("api/addresstypes/{id}/addresses")]
        public IEnumerable<AddressesModel> GetAddressesForAddressType(int id)
        {
            return Mapper.Map<IEnumerable<AddressesModel>>(
                _addressRepository.GetWhere(a => a.AddressType.AddressTypeId == id)
            );
        }
        
        // GET: api/Addresses/user
        [Route("api/addresses/user")]
        [ResponseType(typeof(AddressesModel))]
        public IHttpActionResult GetAddressForCurrentUser()
        {
            var dbAddress = _addressRepository.GetWhere(a => a.User.UserName == CurrentUser.UserName);

            if(dbAddress == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(Mapper.Map<AddressesModel>(dbAddress));
            }
        }

        // GET: api/Addresses/5
        [ResponseType(typeof(AddressesModel))]
        public IHttpActionResult GetAddress (int id)
        {
            // Only users should be able to update their address
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

            Address dbAddress = _addressRepository.GetFirstOrDefault(a => a.User.UserName == CurrentUser.UserName && a.AddressId == id);
            if (dbAddress == null)
            {
                return BadRequest();
            }
            if (id != address.AddressId)
            {
                return BadRequest();
            }

            dbAddress.Update(address);
            _addressRepository.Update(dbAddress);
            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // Returns 204: OK without content
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Addresses/5
        // [ResponseType(typeof(AddressesModel))]
        // public IHttpActionResult PostAddress(AddressesModel address)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        //     var newAddress = new Address();
        //     newAddress.Update(address);
        // 
        //     _unitOfWork.Commit();
        // 
        //     address.AddressId = newAddress.AddressId;
        // 
        //     return CreatedAtRoute("DefaultApi", new { id = address.AddressId }, address);
        // }

        //  DELETE: api/Address/5
        // [ResponseType(typeof(AddressesModel))]
        // public IHttpActionResult DeleteAddress(int id)
        // {
        //     Address dbAddress = _addressRepository.GetFirstOrDefault(a => a.User.UserName == CurrentUser.UserName && a.AddressId == id);
        //     if (dbAddress == null)
        //     {
        //         return NotFound();
        //     }
           
        //     _addressRepository.Delete(dbAddress);
        //     _unitOfWork.Commit();
           
        //     return Ok(Mapper.Map<AddressesModel>(dbAddress));
        // }

        private bool AddressExists(int id)
        {
            return _addressRepository.Any(e => e.AddressId == id);
        }
    }
}

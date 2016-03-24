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
    public class ThinFrontUsersController : BaseApiController
    {
        private IRoleRepository _roleRepository;
        private IThinFrontUserRepository _thinFrontUserRepository;
        private IUnitOfWork _unitOfWork;

        public ThinFrontUsersController(IRoleRepository roleRepository, IThinFrontUserRepository userRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _roleRepository = roleRepository;
            _thinFrontUserRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/ThinFrontUser
        public IEnumerable<ThinFrontUsersModel> GetThinFrontUser()
        {
            return Mapper.Map<IEnumerable<ThinFrontUsersModel>>(
                _thinFrontUserRepository.GetAll()
            );
        }

        // GET: api/resellers
        [Route("api/resellers")]
        [Authorize(Roles = "Supplier")]
        public IEnumerable<ThinFrontUsersModel> GetResellers()
        {
            return Mapper.Map<IEnumerable<ThinFrontUsersModel>>(
                _thinFrontUserRepository.GetWhere(u => u.Role.Name == "Reseller")
                );
        }

        // GET: api/customers
        [Route("api/customers")]
        [Authorize(Roles = "Reseller")]
        public IEnumerable<ThinFrontUsersModel> GetCustomers()
        {
            return Mapper.Map<IEnumerable<ThinFrontUsersModel>>(
                _thinFrontUserRepository.GetWhere(u => u.ResellerId == CurrentUser.Id)
                );
        }

        // GET: api/ThinFrontUser/5
        [ResponseType(typeof(ThinFrontUsersModel))]
        public IHttpActionResult GetPawzeUser(int id)
        {
            ThinFrontUser dbThinFrontUser = _thinFrontUserRepository.GetById(id);

            if (dbThinFrontUser == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ThinFrontUsersModel>(dbThinFrontUser));
        }

        // PUT: api/ThinFrontUser/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutThinFrontUser(int id, ThinFrontUsersModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            ThinFrontUser dbThinFrontUser = _thinFrontUserRepository.GetById(id);
            dbThinFrontUser.Update(user);

            _thinFrontUserRepository.Update(dbThinFrontUser);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!ThinFrontUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // PUT: api/ThinFrontUser/user
        [Route("api/thinfrontuser/user")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCurrentPawzeUser(ThinFrontUsersModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ThinFrontUser dbThinFrontUser = _thinFrontUserRepository.GetFirstOrDefault(u => u.UserName == CurrentUser.UserName);
            dbThinFrontUser.Update(user);

            _thinFrontUserRepository.Update(dbThinFrontUser);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!ThinFrontUserExists(user.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        //  POST: api/PawzeUser
        // [ResponseType(typeof(ThinFrontUsersModel))]
        // public IHttpActionResult PostPawzeUser(ThinFrontUsersModel user)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
           
        //     var dbThinFrontUser = new ThinFrontUser();
        //     _thinFrontUserRepository.Add(dbThinFrontUser);
           
        //     _unitOfWork.Commit();
           
        //     user.Id = dbThinFrontUser.Id;
           
        //     return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        // }
           
        //  DELETE: api/PawzeUser/5
        // [ResponseType(typeof(ThinFrontUsersModel))]
        // public IHttpActionResult DeletePawzeUser(int id)
        // {
        //     ThinFrontUser user = _thinFrontUserRepository.GetById(id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
           
        //     _thinFrontUserRepository.Delete(user);
        //     _unitOfWork.Commit();
           
        //     return Ok(Mapper.Map<ThinFrontUsersModel>(user));
        // }

        private bool ThinFrontUserExists(int id)
        {
            return _thinFrontUserRepository.Count(e => e.Id == id) > 0;
        }

    }
}

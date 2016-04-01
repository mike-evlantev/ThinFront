using AutoMapper;
using System;
using System.Collections;
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
        private IResellerProductRepository _resellerProductRepository;
        private IUnitOfWork _unitOfWork;

        public ThinFrontUsersController(IRoleRepository roleRepository, IResellerProductRepository resellerProductRepository, IThinFrontUserRepository userRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _roleRepository = roleRepository;
            _thinFrontUserRepository = userRepository;
            _resellerProductRepository = resellerProductRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/ThinFrontUsers
        public IEnumerable<ThinFrontUsersModel> GetThinFrontUsers()
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

        [Route("api/resellers/{resellerId}/products")]
        [Authorize(Roles = "Customer")]
        public IEnumerable<ProductsModel> GetProductsForReseller(int resellerId)
        {
            var reseller = _userRepository.GetById(resellerId);

            return Mapper.Map<IEnumerable<ProductsModel>>(reseller.ResellerProducts.Select(p => p.Product));
        }

        [Route("api/resellers/{resellerId}/productCategories")]
        [Authorize(Roles = "Customer")]
        public IEnumerable<ProductCategoriesModel> GetCategoriesForReseller(int resellerId)
        {
            var reseller = _userRepository.GetById(resellerId);

            var mappedCategories = Mapper.Map<IEnumerable<ProductCategoriesModel>>(reseller.ResellerProductCategories.Select(rp => rp.ProductCategory));

            foreach(var mc in mappedCategories)
            {
                foreach(var sc in mc.ProductSubcategories)
                {
                    sc.Products = sc.Products.Where(scp => reseller.ResellerProducts.Any(rpc => rpc.ProductId == scp.ProductId));
                }
            }

            return mappedCategories;
        }

        [Route("api/resellers/products")]
        [Authorize(Roles = "Reseller")]
        [HttpGet]
        public IEnumerable<ProductsModel> GetProductsForCurrentReseller()
        {
            return Mapper.Map<IEnumerable<ProductsModel>>(CurrentUser.ResellerProducts.Select(p => p.Product));
        }

        [Route("api/resellers/products/{productId}")]
        [HttpPost]
        [Authorize(Roles = "Reseller")]
        public IHttpActionResult MakeProductAvailable(int productId)
        {
            CurrentUser.ResellerProducts.Add(new ResellerProduct
            {
                ProductId = productId
            });

            _userRepository.Update(CurrentUser);

            _unitOfWork.Commit();

            return Ok();
        }

        [Route("api/resellers/products/{productId}")]
        [HttpDelete]
        [Authorize(Roles = "Reseller")]
        public IHttpActionResult MakeProductUnavailable(int productId)
        {
            var resellerProduct = CurrentUser.ResellerProducts.FirstOrDefault(p => p.ProductId == productId);

            if(resellerProduct != null)
            {
                _resellerProductRepository.Delete(resellerProduct);

                _unitOfWork.Commit();
            }

            return Ok();
        }

        // GET: /api/ThinFrontUser/user
        [Route("api/thinfrontuser/user")]
        public IHttpActionResult GetCurrentUser()
        {
            var currentUser = _thinFrontUserRepository.GetFirstOrDefault(u => u.UserName == CurrentUser.UserName);
            if (currentUser == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ThinFrontUsersModel>(currentUser));

        }

        // GET: api/ThinFrontUser/5
        [ResponseType(typeof(ThinFrontUsersModel))]
        public IHttpActionResult GetThinFrontUser(int id)
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

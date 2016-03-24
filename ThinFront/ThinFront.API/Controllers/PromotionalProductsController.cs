//using AutoMapper;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using System.Web.Http.Description;
//using ThinFront.API.Infrastructure;
//using ThinFront.Core.Domain;
//using ThinFront.Core.Infrastructure;
//using ThinFront.Core.Models;
//using ThinFront.Core.Repository;

//namespace ThinFront.API.Controllers
//{
//    public class PromotionalProductsController : BaseApiController
//    {
//        private IProductRepository _productRepository;
//        private IPromotionalProductRepository _promotionalProductRepository;
//        private IPromotionRepository _promotionRepository;
//        private IUnitOfWork _unitOfWork;

//        public PromotionalProductsController(IProductRepository productRepository, IPromotionalProductRepository promotionalProductRepository, IPromotionRepository promotionRepository, IThinFrontUserRepository userRepository, IUnitOfWork unitOfWork) : base(userRepository)
//        {
//            _productRepository = productRepository;
//            _promotionalProductRepository = promotionalProductRepository;
//            _promotionRepository = promotionRepository;
//            _unitOfWork = unitOfWork;

//        }

//        // GET: api/PromotionalProducts
//        public IEnumerable<PromotionalProductsModel> GetPromotionalProducts()
//        {
//            return Mapper.Map<IEnumerable<PromotionalProductsModel>>(
//                _promotionalProductRepository.GetWhere(pp => pp.Promotion.Supplier.UserName == CurrentUser.UserName)
//                );
//        }

//        // GET: api/PromotionalProducts/5
//        [ResponseType(typeof(PromotionalProductsModel))]
//        public IHttpActionResult GetPromotionalProduct(int id)
//        {
//            PromotionalProduct dbPromotionalProduct = _promotionalProductRepository.GetFirstOrDefault(pp => pp.Promotion.Supplier.UserName == CurrentUser.UserName && pp.PromotionalProductId == id);
//            if (dbPromotionalProduct == null)
//            {
//                return NotFound();
//            }

//            return Ok(Mapper.Map<PromotionalProductsModel>(dbPromotionalProduct));
//        }

//        // PUT: api/PromotionalProducts/5
//        [ResponseType(typeof(void))]
//        public IHttpActionResult PutPromotionalProduct(int id, PromotionalProductsModel promotionalProduct)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            PromotionalProduct dbPromotionalProduct = _promotionalProductRepository.GetFirstOrDefault(pp => pp.Promotion.Supplier.UserName == CurrentUser.UserName && pp.PromotionalProductId == id);

//            if (id != promotionalProduct.PromotionalProductId)
//            {
//                return BadRequest();
//            }

//            dbPromotionalProduct.Update(promotionalProduct);

//            _promotionalProductRepository.Update(dbPromotionalProduct);

//            try
//            {
//                _unitOfWork.Commit();
//            }
//            catch (Exception)
//            {
//                if (!PromotionalProductExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return StatusCode(HttpStatusCode.NoContent);
//        }

//        // POST: api/PromotionalProducts
//        [ResponseType(typeof(PromotionalProductsModel))]
//        public IHttpActionResult PostPromotionalProduct(PromotionalProductsModel promotionalProduct)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var dbPromotionalProduct = new PromotionalProduct();

//            dbPromotionalProduct.Update(promotionalProduct);

//            _promotionalProductRepository.Add(dbPromotionalProduct);
//            _unitOfWork.Commit();

//            promotionalProduct.PromotionalProductId = dbPromotionalProduct.PromotionalProductId;

//            return CreatedAtRoute("DefaultApi", new { id = promotionalProduct.PromotionalProductId }, promotionalProduct);
//        }

//        // DELETE: api/PromotionalProducts/5
//        [ResponseType(typeof(PromotionalProductsModel))]
//        public IHttpActionResult DeletePromotionalProduct(int id)
//        {
//            PromotionalProduct dbPromotionalProduct = _promotionalProductRepository.GetFirstOrDefault(pp => pp.Promotion.Supplier.UserName == CurrentUser.UserName && pp.PromotionalProductId == id);

//            if (dbPromotionalProduct == null)
//            {
//                return NotFound();
//            }

//            _promotionalProductRepository.Delete(dbPromotionalProduct);
//            _unitOfWork.Commit();

//            return Ok(Mapper.Map<PromotionalProductsModel>(dbPromotionalProduct));
//        }

//        private bool PromotionalProductExists(int id)
//        {
//            return _promotionalProductRepository.Any(e => e.PromotionalProductId == id);
//        }
//    }
//}

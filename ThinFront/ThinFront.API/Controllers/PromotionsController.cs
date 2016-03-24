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
    public class PromotionsController : BaseApiController
    {
        private IProductRepository _productRepository;
        private IPromotionRepository _promotionRepository;
        private IPromotionalProductRepository _promotionalProductRepository;
        private IUnitOfWork _unitOfWork;

        public PromotionsController(IProductRepository productRepository, IPromotionRepository promotionRepository, IPromotionalProductRepository promotionalProductRepository, IThinFrontUserRepository userRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _productRepository = productRepository;
            _promotionRepository = promotionRepository;
            _promotionalProductRepository = promotionalProductRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/supplier/promotions
        [Route("api/supplier/promotions")]
        [Authorize(Roles = "Supplier")]
        public IEnumerable<PromotionsModel> GetPromotionsForCurrentSupplier()
        {
            return Mapper.Map<IEnumerable<PromotionsModel>>(
                _promotionRepository.GetWhere(i => i.Supplier.SupplierId == CurrentUser.Id)
            );
        }

        //GET: api/Inventories/5
        [ResponseType(typeof(PromotionsModel))]
        [Authorize(Roles = "Supplier")]
        public IHttpActionResult GetPromotion(int id)
        {
            Promotion dbPromotion = _promotionRepository.GetFirstOrDefault(p => p.Supplier.SupplierId == CurrentUser.Id && p.PromotionId == id);
            if (dbPromotion == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PromotionsModel>(dbPromotion));
        }

        // GET: api/Promotions/5/PromotionalProducts
        [Route("api/promotions/{promotionId}/promotionalproducts")]
        public IEnumerable<PromotionalProductsModel> GetPromotionalProductsForPromotion(int id)
        {
            var promotionalProduct = _promotionalProductRepository.GetWhere(pc => pc.PromotionId == id && pc.Promotion.Supplier.Id == CurrentUser.Id);

            return Mapper.Map<IEnumerable<PromotionalProductsModel>>(promotionalProduct);
        }

        // Do not create controllers for your many-to-many relation tables. 
        // Instead, create additional actions on either side of the relationship that accesses the opposite side of the relationship.
        // EX: Odrder 1-> OrderItems <-1 Product​

        //GET: api/Promotions/5/Products
        [Route("api/promotions/{promotionId}/products")]
        public IEnumerable<ProductsModel> GetProductsForPromotion(int promotionId)
        {
            return Mapper.Map<IEnumerable<ProductsModel>>(_promotionRepository.GetWhere(p => p.PromotionalProducts.Any(oi => oi.PromotionId == promotionId)));
        }

        // PUT: api/Promotions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPromotion(int id, PromotionsModel promotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Promotion dbPromotion = _promotionRepository.GetFirstOrDefault(p => p.Supplier.UserName == CurrentUser.UserName && p.PromotionId == id);

            if (id != promotion.PromotionId)
            {
                return BadRequest();
            }

            dbPromotion.Update(promotion);

            _promotionRepository.Update(dbPromotion);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!PromotionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // returns a 204 (OK) that doesnt have any content
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Promotions
        [ResponseType(typeof(PromotionsModel))]
        public IHttpActionResult PostPromotion(PromotionsModel promotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbPromotion = new Promotion();

            dbPromotion.Supplier = _userRepository.GetFirstOrDefault(u => u.UserName == CurrentUser.UserName);

            dbPromotion.Update(promotion);

            foreach (var promotionalProduct in promotion.PromotionalProducts)
            {
                dbPromotion.PromotionalProducts.Add(new PromotionalProduct(promotionalProduct));
            }

            _unitOfWork.Commit();

            promotion.PromotionId = dbPromotion.PromotionId;

            return CreatedAtRoute("DefaultApi", new { id = promotion.PromotionId }, promotion);
        }

        // POST: 
        // [Route("api/promotions/{promotionId}/addProduct/{productId}")]
        // public IHttpActionResult AddProductToPromotion(int promotionId, int productId)
        // {
        //     var promotion = _promotionRepository.GetById(promotionId);
        //     var product = _productRepository.GetById(productId);
        // 
        //     // do some checks, make sure promotion & product aren't null!
        //     if (promotionId == null || productId == null)
        //     {
        //         return NotFound();
        //     }
        // 
        //     promotion.Add(new PromotionalProduct
        //     {
        //         Product = product
        //     });
        // 
        //     _promotionRepository.Update(promotion);
        // 
        //     _unitOfWork.Commit();
        // 
        //     return Ok(Mapper.Map<PromotionalProductsModel>(product));
        // }

        // DELETE: api/Promotions/5
        [ResponseType(typeof(PromotionsModel))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Promotion dbPromotion = _promotionRepository.GetFirstOrDefault(p => p.Supplier.UserName == CurrentUser.UserName && p.PromotionId == id);

            if (dbPromotion == null)
            {
                return NotFound();
            }

            _promotionRepository.Delete(dbPromotion);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<PromotionsModel>(dbPromotion));
        }

        private bool PromotionExists(int id)
        {
            return _promotionRepository.Any(e => e.PromotionId == id);
        }
    }
}

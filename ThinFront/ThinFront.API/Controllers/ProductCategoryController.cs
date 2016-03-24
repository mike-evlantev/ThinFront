using AutoMapper;
using Stripe;
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
    public class ProductCategoryController : BaseApiController
    {
        private IProductCategoryRepository _productCategoryRepository;
        private IProductSubcategoryRepository _productSubcategoryRepository;
        private IUnitOfWork _unitOfWork;

        public ProductCategoryController(IProductCategoryRepository productCategoryRepository, IProductSubcategoryRepository productSubcategoryRepository, IThinFrontUserRepository userRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _productCategoryRepository = productCategoryRepository;
            _productSubcategoryRepository = productSubcategoryRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/ProductCategories
        public IEnumerable<ProductCategoriesModel> GetProductCategories()
        {
            return Mapper.Map<IEnumerable<ProductCategoriesModel>>(
                _productCategoryRepository.GetWhere(pc => pc.Inventory.Supplier.UserName == CurrentUser.UserName)
            );
        }

        // GET: api/ProductCategories/5
        [ResponseType(typeof(ProductCategoriesModel))]
        public IHttpActionResult GetProductCategory(int id)
        {
            ProductCategory dbProductCategory = _productCategoryRepository.GetFirstOrDefault(pc => pc.Inventory.Supplier.UserName == CurrentUser.UserName && pc.ProductCategoryId == id);
            if (dbProductCategory == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ProductCategoriesModel>(dbProductCategory));
        }

        // GET: api/ProductCategories/5/ProductSubcategories
        [Route("api/productcategories/{productCategoryId}/productsubcategories")]
        public IEnumerable<ProductSubcategoriesModel> GetProductSubcategoriesForProductCategory(int id)
        {
            var productSubcategories = _productSubcategoryRepository.GetFirstOrDefault(psc => psc.ProductCategoryId == id && psc.ProductCategory.Inventory.Supplier.Id == CurrentUser.Id);

            return Mapper.Map<IEnumerable<ProductSubcategoriesModel>>(productSubcategories);
        }

        // PUT: api/ProductCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductCategory(int id, ProductCategoriesModel productCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProductCategory dbProductCategory = _productCategoryRepository.GetFirstOrDefault(pc => pc.Inventory.Supplier.UserName == CurrentUser.UserName && pc.ProductCategoryId == id);

            if (id != productCategory.ProductCategoryId)
            {
                return BadRequest();
            }

            dbProductCategory.Update(productCategory);

            _productCategoryRepository.Update(dbProductCategory);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!ProductCategoryExists(id))
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

        // POST: api/ProductCategory/5
        [ResponseType(typeof(ProductCategoriesModel))]
        public IHttpActionResult PostProductCategory(int id, ProductCategoriesModel productCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbProductCategory = new ProductCategory();

            dbProductCategory.Inventory.Supplier = _userRepository.GetFirstOrDefault(u => u.UserName == CurrentUser.UserName);

            _productCategoryRepository.Add(dbProductCategory);

            _unitOfWork.Commit();

            productCategory.ProductCategoryId = dbProductCategory.ProductCategoryId;

            return CreatedAtRoute("DefaultApi", new { id = productCategory.ProductCategoryId }, productCategory);
        }

        // DELETE: api/Boxes/5
        [ResponseType(typeof(ProductCategoriesModel))]
        public IHttpActionResult DeleteProductCategory(int id)
        {
            ProductCategory dbProductCategory = _productCategoryRepository.GetFirstOrDefault(pc => pc.Inventory.Supplier.UserName == CurrentUser.UserName && pc.ProductCategoryId == id);

            if (dbProductCategory == null)
            {
                return NotFound();
            }

            _productCategoryRepository.Delete(dbProductCategory);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<OrdersModel>(dbProductCategory));
        }

        private bool ProductCategoryExists(int id)
        {
            return _productCategoryRepository.Any(e => e.ProductCategoryId == id);
        }
    }
}

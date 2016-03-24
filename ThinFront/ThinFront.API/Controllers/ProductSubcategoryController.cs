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
    public class ProductSubcategoryController : BaseApiController
    {
        private IProductRepository _productRepository;
        private IProductCategoryRepository _productCategoryRepository;
        private IProductSubcategoryRepository _productSubcategoryRepository;
        private IUnitOfWork _unitOfWork;

        public ProductSubcategoryController(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IProductSubcategoryRepository productSubcategoryRepository, IThinFrontUserRepository userRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productSubcategoryRepository = productSubcategoryRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/ProductSubcategories
        public IEnumerable<ProductSubcategoriesModel> GetProductSubcategories()
        {
            return Mapper.Map<IEnumerable<ProductSubcategoriesModel>>(
                _productSubcategoryRepository.GetWhere(psc => psc.ProductCategory.Inventory.Supplier.UserName == CurrentUser.UserName)
            );
        }

        // GET: api/ProductCategories/5
        [ResponseType(typeof(ProductCategoriesModel))]
        public IHttpActionResult GetProductSubcategory(int id)
        {
            ProductSubcategory dbProductSubcategory = _productSubcategoryRepository.GetFirstOrDefault(psc => psc.ProductCategory.Inventory.Supplier.UserName == CurrentUser.UserName && psc.ProductSubcategoryId == id);
            if (dbProductSubcategory == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ProductCategoriesModel>(dbProductSubcategory));
        }

        // GET: api/ProductSubcategories/5/Products
        [Route("api/productsubcategories/{productSubcategoryId}/products")]
        public IEnumerable<ProductsModel> GetProductsForProductSubcategory(int id)
        {
            var products = _productRepository.GetFirstOrDefault(p => p.ProductSubcategoryId == id && p.ProductSubcategory.ProductCategory.Inventory.Supplier.Id == CurrentUser.Id);

            return Mapper.Map<IEnumerable<ProductsModel>>(products);
        }

        // PUT: api/ProductSubcategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductSubcategory(int id, ProductSubcategoriesModel productSubcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProductSubcategory dbProductSubcategory = _productSubcategoryRepository.GetFirstOrDefault(pc => pc.ProductCategory.Inventory.Supplier.UserName == CurrentUser.UserName && pc.ProductSubcategoryId == id);

            if (id != productSubcategory.ProductSubcategoryId)
            {
                return BadRequest();
            }

            dbProductSubcategory.Update(productSubcategory);

            _productSubcategoryRepository.Update(dbProductSubcategory);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!ProductSubcategoryExists(id))
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

        // POST: api/Orders
        [ResponseType(typeof(ProductCategoriesModel))]
        public IHttpActionResult PostProductSubcategory(int id, ProductSubcategoriesModel productSubcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbProductSubcategory = new ProductSubcategory();

            dbProductSubcategory.ProductCategory.Inventory.Supplier = _userRepository.GetFirstOrDefault(u => u.UserName == CurrentUser.UserName);

            _productSubcategoryRepository.Add(dbProductSubcategory);

            _unitOfWork.Commit();

            productSubcategory.ProductSubcategoryId = dbProductSubcategory.ProductSubcategoryId;

            return CreatedAtRoute("DefaultApi", new { id = productSubcategory.ProductSubcategoryId }, productSubcategory);
        }

        // DELETE: api/Boxes/5
        [ResponseType(typeof(ProductCategoriesModel))]
        public IHttpActionResult DeleteProductSubcategory(int id)
        {
            ProductSubcategory dbProductSubcategory = _productSubcategoryRepository.GetFirstOrDefault(psc => psc.ProductCategory.Inventory.Supplier.UserName == CurrentUser.UserName && psc.ProductCategoryId == id);

            if (dbProductSubcategory == null)
            {
                return NotFound();
            }

            _productSubcategoryRepository.Delete(dbProductSubcategory);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<OrdersModel>(dbProductSubcategory));
        }

        private bool ProductSubcategoryExists(int id)
        {
            return _productSubcategoryRepository.Any(e => e.ProductSubcategoryId == id);
        }
    }
}

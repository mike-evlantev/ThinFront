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
    public class ProductController : BaseApiController
    {
        private IOrderItemRepository _orderItemRepository;
        private IProductRepository _productRepository;
        private IPromotionalProductRepository _promotionalProductRepository;
        private IUnitOfWork _unitOfWork;

        public ProductController(IOrderItemRepository orderItemRepository, IProductRepository productRepository, IPromotionalProductRepository promotionalProductRepository, IThinFrontUserRepository userRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
            _promotionalProductRepository = promotionalProductRepository;
            _unitOfWork = unitOfWork;
        }

        // NEEDS to return products with respective subcategories
        // GET: api/Products
        public IEnumerable<ProductsModel> GetProducts()
        {
            return Mapper.Map<IEnumerable<ProductsModel>>(
                _productRepository.GetWhere(p => p.ProductSubcategory.ProductCategory.Inventory.Supplier.UserName == CurrentUser.UserName)
            );
        }

        // GET: api/Products/5
        [ResponseType(typeof(ProductsModel))]
        public IHttpActionResult GetProduct(int id)
        {
            Product dbProduct = _productRepository.GetFirstOrDefault(p => p.ProductSubcategory.ProductCategory.Inventory.Supplier.UserName == CurrentUser.UserName && p.ProductId == id);
            if (dbProduct == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ProductsModel>(dbProduct));
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, ProductsModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product dbProduct = _productRepository.GetFirstOrDefault(p => p.ProductSubcategory.ProductCategory.Inventory.Supplier.UserName == CurrentUser.UserName && p.ProductId == id);

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            dbProduct.Update(product);

            _productRepository.Update(dbProduct);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        [ResponseType(typeof(ProductsModel))]
        public IHttpActionResult PostProduct(int id, ProductsModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbProduct = new Product(product);

            dbProduct.ProductSubcategory.ProductCategory.Inventory.Supplier = _userRepository.GetFirstOrDefault(u => u.UserName == CurrentUser.UserName);

            _productRepository.Add(dbProduct);

            _unitOfWork.Commit();

            product.ProductId = dbProduct.ProductId;

            return CreatedAtRoute("DefaultApi", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(ProductsModel))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product dbProduct = _productRepository.GetFirstOrDefault(pc => pc.ProductSubcategory.ProductCategory.Inventory.Supplier.UserName == CurrentUser.UserName && pc.ProductId == id);

            if (dbProduct == null)
            {
                return NotFound();
            }

            _productRepository.Delete(dbProduct);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<OrdersModel>(dbProduct));
        }

        private bool ProductExists(int id)
        {
            return _productRepository.Any(e => e.ProductId == id);
        }
    }
}

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
    public class InventoriesController : BaseApiController
    {
        private IInventoryRepository _inventoryRepository;
        private IProductCategoryRepository _productCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public InventoriesController(IInventoryRepository inventoryRepository, IProductCategoryRepository productCategoryRepository, IThinFrontUserRepository userRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _inventoryRepository = inventoryRepository;
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/supplier/inventories
        [Route("api/supplier/inventories")]
        [Authorize(Roles = "Supplier")] 
        public IEnumerable<InventoriesModel> GetInventoriesForCurrentUser()
        {
            return Mapper.Map<IEnumerable<InventoriesModel>>(
                _inventoryRepository.GetWhere(i => i.Supplier.SupplierId == CurrentUser.Id)
            );
        }

        //GET: api/Inventories/5
        [ResponseType(typeof(InventoriesModel))]
        [Authorize(Roles = "Supplier")]
        public IHttpActionResult GetInventory(int id)
        {
            Inventory dbInventory = _inventoryRepository.GetFirstOrDefault(i => i.Supplier.SupplierId == CurrentUser.Id && i.InventoryId == id);
            if (dbInventory == null)
            {
                return NotFound();
            }
                
            return Ok(Mapper.Map<InventoriesModel>(dbInventory));
        }

        // GET: api/Inventories/5/ProductCategories
        [Route("api/inventories/{inventoryId}/productcategories")]
        public IEnumerable<ProductCategoriesModel> GetProductCategoriesForInventory(int id)
        {
            var productCategories = _productCategoryRepository.GetWhere(pc => pc.InventoryId == id);

            return Mapper.Map<IEnumerable<ProductCategoriesModel>>(productCategories);
        }

        // PUT: api/Inventory/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInventory(int id, InventoriesModel inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Inventory dbInventory = _inventoryRepository.GetFirstOrDefault(i => i.Supplier.UserName == CurrentUser.UserName && i.InventoryId == id);

            if (id != inventory.InventoryId)
            {
                return BadRequest();
            }

            dbInventory.Update(inventory);

            _inventoryRepository.Update(dbInventory);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!InventoryExists(id))
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

        // POST: api/Inventory
        [ResponseType(typeof(InventoriesModel))]
        [Authorize(Roles = "Supplier")]
        public IHttpActionResult PostInventory(InventoriesModel inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbInventory = new Inventory();

            dbInventory.Supplier = _userRepository.GetFirstOrDefault(u => u.SupplierId == CurrentUser.Id);

            _inventoryRepository.Add(dbInventory);

            _unitOfWork.Commit();

            inventory.InventoryId = dbInventory.InventoryId;

            return CreatedAtRoute("DefaultApi", new { id = inventory.InventoryId }, inventory);
        }

        // DELETE: api/Inventory/5
        [ResponseType(typeof(InventoriesModel))]
        [Authorize(Roles = "Supplier")]
        public IHttpActionResult DeleteInventory(int id)
        {
            Inventory dbInventory = _inventoryRepository.GetFirstOrDefault(i => i.Supplier.SupplierId == CurrentUser.Id && i.InventoryId == id);

            if (dbInventory == null)
            {
                return NotFound();
            }

            _inventoryRepository.Delete(dbInventory);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<InventoriesModel>(dbInventory));
        }

        private bool InventoryExists(int id)
        {
            return _inventoryRepository.Any(e => e.InventoryId == id);
        }
    }
}

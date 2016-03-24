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
    public class OrdersController : BaseApiController
    {
        private IOrderRepository _orderRepository;
        private IOrderItemRepository _orderItemRepository;
        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;

        public OrdersController(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IProductRepository productRepository, IThinFrontUserRepository userRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        // // GET: api/Orders
        // [Route("api/orders")]
        // public IEnumerable<OrdersModel> GetOrders()
        // {
        //     return Mapper.Map<IEnumerable<OrdersModel>>(
        //         _orderRepository.GetWhere(o => o.User.UserName == CurrentUser.UserName)    
        //     );
        // }

        // GET: api/reseller/orders
        [Route("api/reseller/orders")]
        [Authorize(Roles = "Reseller")]
        public IEnumerable<OrdersModel> GetOrdersForReseller()
        {
            return Mapper.Map<IEnumerable<OrdersModel>>(
                _orderRepository.GetWhere(o => o.Customer.ResellerId == CurrentUser.Id)
            );
        }

        // GET: api/customer/orders
        [Route("api/customer/orders")]
        [Authorize(Roles = "Customer")]
        public IEnumerable<OrdersModel> GetOrdersForCustomer()
        {
            return Mapper.Map<IEnumerable<OrdersModel>>(
                _orderRepository.GetWhere(o => o.Customer.Id == CurrentUser.Id)
            );
        }

        // GET: api/reseller/Orders/5
        [Route("api/reseller/orders/{id}")]
        [Authorize(Roles ="Reseller")]
        [ResponseType(typeof(OrdersModel))]
        public IHttpActionResult GetOrderForReseller(int id)
        {
            Order dbOrder = _orderRepository.GetFirstOrDefault(o => o.Customer.ResellerId == CurrentUser.Id && o.OrderId == id);
            if(dbOrder == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<OrdersModel>(dbOrder));
        }

        // GET: api/Orders/5
        [Route("api/customer/orders/{id}")]
        [Authorize(Roles = "Customer")]
        [ResponseType(typeof(OrdersModel))]
        public IHttpActionResult GetOrderForCustomer(int id)
        {
            Order dbOrder = _orderRepository.GetFirstOrDefault(o => o.Customer.Id == CurrentUser.Id && o.OrderId == id);
            if (dbOrder == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<OrdersModel>(dbOrder));
        }

        // GET: api/Orders/date
        [Route("api/reseller/orders/{startDate}/{endDate}")]
        [Authorize(Roles = "Reseller")]
        public IEnumerable<OrdersModel> GetOrdersForResellerByDate(DateTime startDate, DateTime endDate)
        {
            return Mapper.Map<IEnumerable<OrdersModel>>(
                _orderRepository.GetWhere(o => o.OrderDate >= startDate && o.OrderDate <= endDate && o.Customer.ResellerId == CurrentUser.Id)
                );
        }

        // GET: api/Orders/5/OrderItems
        [Route("api/orders/{orderId}/orderitems")]
        public IEnumerable<OrderItemsModel> GetOrderItemsForOrder(int id)
        {
            var orderItems = _orderItemRepository.GetWhere(oi => oi.OrderId == id && oi.Order.Customer.Id == CurrentUser.Id);

            return Mapper.Map<IEnumerable<OrderItemsModel>>(orderItems);
        }

        // Do not create controllers for your many-to-many relation tables. 
        // Instead, create additional actions on either side of the relationship that accesses the opposite side of the relationship.
        // EX: Odrder 1-> OrderItems <-1 Product​

        //GET: api/Orders/5/Products
        [Route("api/orders/{orderId}/products")]
        public IEnumerable<ProductsModel> GetProductsForOrder(int orderId)
        {
            return Mapper.Map<IEnumerable<ProductsModel>>(_productRepository.GetWhere(p => p.OrderItems.Any(oi => oi.OrderId == orderId)));
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, OrdersModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order dbOrder = _orderRepository.GetFirstOrDefault(o => o.Customer.UserName == CurrentUser.UserName && o.OrderId == id);

            if (id != order.OrderId)
            {
                return BadRequest();
            }

            dbOrder.Update(order);

            _orderRepository.Update(dbOrder);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!OrderExists(id))
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
        [ResponseType(typeof(OrdersModel))]
        public IHttpActionResult PostOrder(OrdersModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbOrder = new Order();

            dbOrder.Customer = _userRepository.GetFirstOrDefault(u => u.UserName == CurrentUser.UserName);

            dbOrder.Update(order);

            foreach (var orderItem in order.OrderItems)
            {
                dbOrder.OrderItems.Add(new OrderItem(orderItem));
            }

            _unitOfWork.Commit();

            order.OrderId = dbOrder.OrderId;

            return CreatedAtRoute("DefaultApi", new { id = order.OrderId }, order);
        }

        // DELETE: api/Boxes/5
        [ResponseType(typeof(OrdersModel))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order dbOrder = _orderRepository.GetFirstOrDefault(o => o.Customer.UserName == CurrentUser.UserName && o.OrderId == id);

            if (dbOrder == null)
            {
                return NotFound();
            }

            _orderRepository.Delete(dbOrder);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<OrdersModel>(dbOrder));
        }

        private bool OrderExists(int id)
        {
            return _orderRepository.Any(e => e.OrderId == id);
        }
    }
}

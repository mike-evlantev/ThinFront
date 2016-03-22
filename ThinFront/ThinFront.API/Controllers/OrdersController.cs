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
        private IUnitOfWork _unitOfWork;

        public OrdersController(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IThinFrontUserRepository userRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Orders
        [Route("api/orders")]
        public IEnumerable<OrdersModel> GetOrders()
        {
            return Mapper.Map<IEnumerable<OrdersModel>>(
                _orderRepository.GetWhere(o => o.User.UserName == CurrentUser.UserName)    
            );
        }

        // GET: api/Orders/5
        [ResponseType(typeof(OrdersModel))]
        public IHttpActionResult GetOrder(int id)
        {
            Order dbOrder = _orderRepository.GetFirstOrDefault(o => o.User.UserName == CurrentUser.UserName && o.OrderId == id);
            if(dbOrder == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<OrdersModel>(dbOrder));
        }

        // GET: api/Orders/user
        [Route("api/orders/user")]
        public OrdersModel GetOrderForCurrentUser()
        {
            // var currentUser = _userRepository.GetFirstOrDefault(u => u.User.UserName == CurrentUser.UserName);
            var dbOrder = _orderRepository.GetFirstOrDefault(o => o.User.UserName == CurrentUser.UserName);
            if(dbOrder == null)
            {
                return new OrdersModel
                {
                    OrderItems = new OrderItemsModel[]
                    {
                        // creates an order with a number of items
                    }
                };
            }
            else
            {
                return Mapper.Map<OrdersModel>(dbOrder);
            }
        }

        // GET: api/Orders/5/OrderItems

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, OrdersModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order dbOrder = _orderRepository.GetFirstOrDefault(o => o.User.UserName == CurrentUser.UserName && o.OrderId == id);

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
                if (!BoxExists(id))
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

        // POST: api/Boxes
        [ResponseType(typeof(OrdersModel))]
        public IHttpActionResult PostOrder(OrdersModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbOrder = new Order();

            dbOrder.User = _userRepository.GetFirstOrDefault(u => u.UserName == CurrentUser.UserName);

            dbOrder.Update(order);

            foreach (var boxItem in dbOrder.OrderItems)
            {
                boxItem.BoxItemPrice = _configRepository.GetAll().First().CurrentBoxItemPrice;
            }

            _orderRepository.Add(dbOrder);

            _unitOfWork.Commit();

            order.OrderId = dbOrder.OrderId;

            return CreatedAtRoute("DefaultApi", new { id = order.OrderId }, order);
        }

        // DELETE: api/Boxes/5
        [ResponseType(typeof(OrdersModel))]
        public IHttpActionResult DeleteOrder(int id)
        {
            // Box box = db.Boxes.Find(id);
            Order dbOrder = _orderRepository.GetFirstOrDefault(b => b.User.UserName == User.Identity.Name && b.OrderId == id);

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

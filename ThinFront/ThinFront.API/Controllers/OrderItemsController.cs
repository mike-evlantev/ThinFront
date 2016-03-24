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
//    public class OrderItemsController : BaseApiController
//    {
//        private IOrderRepository _orderRepository;
//        private IOrderItemRepository _orderItemRepository;
//        private IProductRepository _productRepository;
//        private IUnitOfWork _unitOfWork;

//        public OrderItemsController(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IProductRepository productRepository, IThinFrontUserRepository userRepository, IUnitOfWork unitOfWork) : base(userRepository)
//        {
//            _orderRepository = orderRepository;
//            _orderItemRepository = orderItemRepository;
//            _productRepository = productRepository;
//            _unitOfWork = unitOfWork;

//        }

//        // GET: api/OrderItems
//        public IEnumerable<OrderItemsModel> GetOrderItems()
//        {
//            return Mapper.Map<IEnumerable<OrderItemsModel>>(
//                _orderItemRepository.GetWhere(pp => pp.Order.Customer.UserName == CurrentUser.UserName)
//                );
//        }

//        // GET: api/OrderItems/5
//        [ResponseType(typeof(OrderItemsModel))]
//        public IHttpActionResult GetOrderItem(int id)
//        {
//            OrderItem dbOrderItem = _orderItemRepository.GetFirstOrDefault(pp => pp.Order.Customer.UserName == CurrentUser.UserName && pp.OrderItemId == id);
//            if (dbOrderItem == null)
//            {
//                return NotFound();
//            }

//            return Ok(Mapper.Map<OrderItemsModel>(dbOrderItem));
//        }

//        // PUT: api/OrderItems/5
//        [ResponseType(typeof(void))]
//        public IHttpActionResult PutOrderItem(int id, OrderItemsModel orderItem)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            OrderItem dbOrderItem = _orderItemRepository.GetFirstOrDefault(oi => oi.Order.Customer.UserName == CurrentUser.UserName && oi.OrderItemId == id);

//            if (id != orderItem.OrderItemId)
//            {
//                return BadRequest();
//            }

//            dbOrderItem.Update(orderItem);

//            _orderItemRepository.Update(dbOrderItem);

//            try
//            {
//                _unitOfWork.Commit();
//            }
//            catch (Exception)
//            {
//                if (!OrderItemExists(id))
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

//        // POST: api/OrderItems
//        [ResponseType(typeof(OrderItemsModel))]
//        public IHttpActionResult PostOrderItem(OrderItemsModel orderItem)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var dbOrderItem = new OrderItem();

//            dbOrderItem.Update(orderItem);

//            _orderItemRepository.Add(dbOrderItem);
//            _unitOfWork.Commit();

//            orderItem.OrderItemId = dbOrderItem.OrderItemId;

//            return CreatedAtRoute("DefaultApi", new { id = orderItem.OrderItemId }, orderItem);
//        }

//        // DELETE: api/OrderItems/5
//        [ResponseType(typeof(OrderItemsModel))]
//        public IHttpActionResult DeleteOrderItem(int id)
//        {
//            OrderItem dbOrderItem = _orderItemRepository.GetFirstOrDefault(pp => pp.Order.Customer.UserName == CurrentUser.UserName && pp.OrderItemId == id);

//            if (dbOrderItem == null)
//            {
//                return NotFound();
//            }

//            _orderItemRepository.Delete(dbOrderItem);
//            _unitOfWork.Commit();

//            return Ok(Mapper.Map<OrderItemsModel>(dbOrderItem));
//        }

//        private bool OrderItemExists(int id)
//        {
//            return _orderItemRepository.Any(e => e.OrderItemId == id);
//        }
//    }
//}

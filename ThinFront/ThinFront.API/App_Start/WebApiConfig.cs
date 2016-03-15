using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ThinFront.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application / xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            CreateMaps();
        }
        public static void CreateMaps()
        {
            Mapper.CreateMap<Address, AddressesModel>();
            Mapper.CreateMap<AddressType, AddressTypesMOdel>();
            Mapper.CreateMap<Customer, CustomersModel>();
            Mapper.CreateMap<Inventory, InventoriesModel>();
            Mapper.CreateMap<Order, OrdersModel>();
            Mapper.CreateMap<OrderItem, OrderItemsModel>();
            Mapper.CreateMap<Product, ProductsModel>();
            Mapper.CreateMap<ProductCategory, ProductCategoriesModel>();
            Mapper.CreateMap<ProductSubcategory, ProducySubcategoriesModel>();
            Mapper.CreateMap<Promotion, PromotionsModel>();
            Mapper.CreateMap<PromotionalProduct, PromotionalProductsModel>();
            Mapper.CreateMap<Reseller, ResellersModel>();
            Mapper.CreateMap<ResellerProductCategory, ResellerProductCategoriesModel>();
            Mapper.CreateMap<Supplier, SuppliersModel>();
        }
    }
}

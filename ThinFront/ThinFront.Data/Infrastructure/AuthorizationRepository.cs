using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Domain;
using ThinFront.Core.Infrastructure;
using ThinFront.Core.Models;

namespace ThinFront.Data.Infrastructure
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly IUserStore<ThinFrontUser, int> _userStore;
        private readonly IDatabaseFactory _databaseFactory;
        private readonly UserManager<ThinFrontUser, int> _userManager;

        private ThinFrontDataContext db;
        protected ThinFrontDataContext Db => db ?? (db = _databaseFactory.GetDataContext());

        public AuthorizationRepository(IDatabaseFactory databaseFactory, IUserStore<ThinFrontUser, int> userStore)
        {
            _userStore = userStore;
            _databaseFactory = databaseFactory;
            _userManager = new UserManager<ThinFrontUser, int>(userStore);
        }

        // Assign Customer Role
        public async Task<IdentityResult> RegisterCustomer(RegistrationsModel.Customer model)
        {
            var customer = new ThinFrontUser
            {
                Email = model.EmailAddress,
                Phone = model.Phone,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.EmailAddress,
                Addresses = new Collection<Address>
                {
                    new Address(model.BillingAddress, AddressTypes.Billing),
                    new Address(model.ShippingAddress, AddressTypes.Shipping)
                }
            };

            //// Save a customer
            //Db.Users.Add(customer);
            //Db.SaveChanges();

            // Save a user
            var result = await _userManager.CreateAsync(customer, model.Password);

            await _userManager.AddToRoleAsync(customer.Id, "Customer");
            return result;
        }
        // Assign a Reseller role
        public async Task<IdentityResult> RegisterReseller(RegistrationsModel.Reseller model)
        {
            var reseller = new ThinFrontUser
            {
                CompanyName = model.CompanyName,
                Email = model.EmailAddress,
                Phone = model.Phone,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.EmailAddress,
                Addresses = new Collection<Address>
                {
                    new Address(model.BillingAddress, AddressTypes.Billing),
                    new Address(model.ShippingAddress, AddressTypes.Shipping)
                }
            };

            // Save a user
            var result = await _userManager.CreateAsync(reseller, model.Password);
            await _userManager.AddToRoleAsync(reseller.Id, "Reseller");
            return result;
        }
        // Assign a Supplier role
        public async Task<IdentityResult> RegisterSupplier(RegistrationsModel.Supplier model)
        {
            var supplier = new ThinFrontUser
            {
                CompanyName = model.CompanyName,
                Email = model.EmailAddress,
                Phone = model.Phone,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.EmailAddress,
                Addresses = new Collection<Address>
                {
                    new Address(model.BillingAddress, AddressTypes.Billing),
                    new Address(model.ShippingAddress, AddressTypes.Shipping)
                }
            };

            // Save a user
            var result = await _userManager.CreateAsync(supplier, model.Password);
            await _userManager.AddToRoleAsync(supplier.Id, "Supplier");
            return result;
        }

        public async Task<ThinFrontUser> FindUser(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }
    }
}

using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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

        public AuthorizationRepository(IDatabaseFactory databaseFactory, IUserStore<ThinFrontUser> userStore)
        {
            _userStore = userStore;
            _databaseFactory = databaseFactory;
            _userManager = new UserManager<ThinFrontUser, int>(userStore);
        }

        // Assign Customer Role
        public async Task<IdentityResult> RegisterCustomer(RegistrationsModel.Customer model)
        {
            var customer = new Customer
            {
                Email = model.EmailAddress,
                FirstName = model.FirstName,
                LastName = model.LastName,
                
            };

            // Create a user
            var thinFrontUser = new ThinFrontUser
            {
                UserName = model.EmailAddress,
                Email = model.EmailAddress,
                FirstName = model.FirstName,
                LastName = model.LastName,
                // Whats the proper way to model the address per address model??
                
            };

            thinFrontUser.Addresses
            
                // Save a user
            var result = await _userManager.CreateAsync(thinFrontUser, model.Password);
            await _userManager.AddToRoleAsync(thinFrontUser.Id, "Customer");
            return result;
        }
        // Assign a Reseller role
        public async Task<IdentityResult> RegisterReseller(RegistrationsModel.Reseller model)
        {
            // Create a user
            var thinFrontUser = new ThinFrontUser
            {
                // ???What should this reflect???
                UserName = model.EmailAddress,
                Email = model.EmailAddress
            };

            // Save a user
            var result = await _userManager.CreateAsync(thinFrontUser, model.Password);
            await _userManager.AddToRoleAsync(thinFrontUser.Id, "Reseller");
            return result;
        }
        // Assign a Supplier role
        public async Task<IdentityResult> RegisterSupplier(RegistrationsModel.Supplier model)
        {
            // Create a user
            var thinFrontUser = new ThinFrontUser
            {
                // ???What should this reflect???
                UserName = model.EmailAddress,
                Email = model.EmailAddress
            };

            // Save a user
            var result = await _userManager.CreateAsync(thinFrontUser, model.Password);
            await _userManager.AddToRoleAsync(thinFrontUser.Id, "Supplier");
            return result;
        }

        public async Task<ThinFrontUser> FindUser(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }

        //Should the finduser method be divided by role?
        public async Task<Customer> FindCustomer(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }
        public async Task<ThinFrontUser> FindReseller(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }
        public async Task<Supplier> FindSupplier(username, password)
        {
            return await _userManager.FindAsync(userName, password);
        }
    }
}

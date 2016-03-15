using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Domain;
using ThinFront.Core.Models;

namespace ThinFront.Core.Infrastructure
{
    public interface IAuthorizationRepository
    {
        // Connector between OAuth (API) and EF (DB)
        // Specifies the details needed to find and register users
        // This method takes a username and password and finds the corresponding User(Customer, Reseller, or Supplier)
        // and returns the corresponding User (Customer, Reseller, or Supplier)
        Task<Customer> FindCustomer(string username, string password);
        Task<Supplier> FindSupplier(string username, string password);
        Task<Reseller> FindReseller(string username, string password);
        
        // takes in a registration model - input form that adds it to the database
        Task<IdentityResult> RegisterReseller(RegistrationsModel.Reseller model);
        Task<IdentityResult> RegisterCustomer(RegistrationsModel.Customer model);
        Task<IdentityResult> RegisterSupplier(RegistrationsModel.Supplier model);
    }
}

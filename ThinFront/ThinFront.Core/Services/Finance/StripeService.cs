using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Services.Finance
{
    public class StripeService
    {
        // This executes once the credit card info has been submitted on the front end

        // Set Secret key
        private const string StripeApiKey = "sk_test_IIPbTReVDSF5FlJHFNW5ZJIR";

        // Get the credit card details submitted by the form
        public void GetCardDetails()
        {
            // Declare a transaction token
            var token = new StripeTokenCreateOptions();

            var tokenService = new StripeTokenService();
            StripeToken stripeToken = tokenService.Create(token);

            var customerCharge = new StripeChargeCreateOptions();

            // Charge properties
            customerCharge.Amount = 0;
            customerCharge.Currency = "usd";
            customerCharge.Description = "Purchase at ResellerName";

            customerCharge.Source = new StripeSourceOptions()
            {
                TokenId = token
            };

            var chargeService = new StripeChargeService();
            StripeCharge stripeCharge = chargeService.Create(customerCharge);
        }
        
    }
}

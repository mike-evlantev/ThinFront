using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ThinFront.Core.Domain;
using ThinFront.Core.Repository;

namespace ThinFront.API.Infrastructure
{
    public class BaseApiController : ApiController
    {
        protected readonly IThinFrontUserRepository _userRepository;
        public BaseApiController(IThinFrontUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        protected ThinFrontUser CurrentUser
        {
            get
            {
                return _userRepository.GetFirstOrDefault(u => u.UserName == User.Identity.Name);
            }
        }
    }
}

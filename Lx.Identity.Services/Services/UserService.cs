using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lx.Identity.Persistence.Uow;
using Lx.Shared.All.Identity.DTOs;

namespace Lx.Identity.Services.Services
{
  public  class UserService:IUserService
  {
      protected readonly IUserUowFactory UserUowFactory;

      public UserService(IUserUowFactory userUowFactory)
      {
          UserUowFactory = userUowFactory;
      }

      public UserDto GetUser(string usernameOrEmailOrMobileNumber, string userProfileOriginator)
      {
          var userDto=UserUowFactory.;
      }

      public UserDto GetUser(Guid userKey, string userProfileOriginator)
      {
          throw new NotImplementedException();
      }

      public UserProfileDto GetUserProfile(Guid userKeystring, string userProfileGroupOriginator)
      {
          throw new NotImplementedException();
      }
    }
}

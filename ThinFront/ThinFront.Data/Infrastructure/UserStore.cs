using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Domain;
using ThinFront.Core.Infrastructure;

namespace ThinFront.Data.Infrastructure
{
    // 
    public class UserStore : Disposable,
                             IUserStore<ThinFrontUser, int>,
                             IUserPasswordStore<ThinFrontUser, int>,
                             IUserSecurityStampStore<ThinFrontUser, int>,
                             IUserRoleStore<ThinFrontUser, int>
    {
        private readonly IDatabaseFactory _databaseFactory;

        private ThinFrontDataContext _db;
        protected ThinFrontDataContext Db
        {
            get
            {
                return _db ?? (_db = _databaseFactory.GetDataContext());
            }
        }

        public UserStore(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        #region IUserStore
        public Task CreateAsync(ThinFrontUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() => {
                Db.Users.Add(user);
                Db.SaveChanges();
            });
        }

        public Task UpdateAsync(ThinFrontUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() =>
            {
                Db.Users.Attach(user);
                Db.Entry(user).State = EntityState.Modified;

                Db.SaveChanges();
            });
        }

        public Task DeleteAsync(ThinFrontUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() =>
            {
                Db.Users.Remove(user);
                Db.SaveChanges();
            });
        }

        public Task<ThinFrontUser> FindByIdAsync(int userId)
        {
            return Task.Factory.StartNew(() => Db.Users.Find(userId));
        }

        public Task<ThinFrontUser> FindByNameAsync(string userName)
        {
            return Task.Factory.StartNew(() => Db.Users.FirstOrDefault(u => u.UserName == userName));
        }
        #endregion

        #region IUserPasswordStore
        public Task SetPasswordHashAsync(ThinFrontUser user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(ThinFrontUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ThinFrontUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }
        #endregion

        #region IUserSecurityStampStore
        public Task SetSecurityStampAsync(ThinFrontUser user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(ThinFrontUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.SecurityStamp);
        }
        #endregion

        #region IUserRoleStore

        public Task AddToRoleAsync(ThinFrontUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }

            return Task.Factory.StartNew(() =>
            {
                if (!Db.Roles.Any(r => r.Name == roleName))
                {
                    Db.Roles.Add(new Role
                    {
                        Name = roleName
                    });
                    Db.SaveChanges();
                }

                user.Role = Db.Roles.FirstOrDefault(r => r.Name == roleName);

                Db.SaveChanges();
            });
        }

        public Task RemoveFromRoleAsync(ThinFrontUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }

            return Task.Factory.StartNew(() =>
            {
                // 1. Check if user has role
                if (user.Role.Name == roleName)
                {
                    // 2. if user has role remove user from that role
                    user.Role = null;
                }
                
                // 3. Update status of user
                Db.Entry(user).State = EntityState.Modified;
                
                // 4. save changes
                Db.SaveChanges();
            });
        }

        public Task<IList<string>> GetRolesAsync(ThinFrontUser user)
        {
            return Task.Factory.StartNew(() =>
            {
                return (IList<string>)Db.Roles.Select(r => r.Name).ToList();
            });
        }

        public Task<bool> IsInRoleAsync(ThinFrontUser user, string roleName)
        {
            return Task.Factory.StartNew(() =>
            {
                return user.Role.Name == roleName;
            });
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Infrastructure
{
    // represents a transaction when used in data layers
    // que up stuff to do or not do -- tapping into db.SaveChanges()
    // getting the current datacontext and calling db.SaveChanges()s
    public interface IUnitOfWork
    {
        void Commit();
    }
}

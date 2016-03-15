using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Infrastructure;

namespace ThinFront.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private readonly ThinFrontDataContext _dataContext;
        public ThinFrontDataContext GetDataContext()
        {
            return _dataContext ?? new ThinFrontDataContext();
        }

        public DatabaseFactory()
        {
            _dataContext = new ThinFrontDataContext();
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null) _dataContext.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Domain;
using ThinFront.Core.Repository;
using ThinFront.Data.Infrastructure;

namespace ThinFront.Data.Repository
{
    public class ProductSubcategoryRepository : Repository<ProductSubcategory>, IProductSubcategoryRepository
    {
        public ProductSubcategoryRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}

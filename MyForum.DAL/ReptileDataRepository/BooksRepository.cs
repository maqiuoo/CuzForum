using MyForum.DAL.ReptileDataInterface;
using MyForum.DAL.ReptileDataRepository;
using MyForum.Model.ReptlieModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL.ReptileData
{
    public class BooksRepository : ReptileDataRepository<Book>, IBooksRepository
    {
        public BooksRepository(ReptileDbContext dbctx) : base(dbctx)
        {
        }
    }
}

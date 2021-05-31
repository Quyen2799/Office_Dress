using DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIO
{
    public class OrderIO
    {
        MyShopHTQDB db = new MyShopHTQDB();

        public List<Order> GetListOrder()
        {
            return db.Orders.ToList();
        }
    }
}

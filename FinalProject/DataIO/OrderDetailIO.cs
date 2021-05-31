using DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIO
{
    public class OrderDetailIO
    {
        MyShopHTQDB db = new MyShopHTQDB();

        public List<OrderDetail> GetListOrderDetail()
        {
            return db.OrderDetails.ToList();
        }
        public List<OrderDetail> GetListByOrderID(int? id)
        {
            string query = "select * from dbo.OrderDetails where IDORDER = " + id;
            return db.Database.SqlQuery<OrderDetail>(query).ToList();
        }
    }
}

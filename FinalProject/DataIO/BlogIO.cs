using DataProvider;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIO
{
    public class BlogIO
    {
        MyShopHTQDB db = new MyShopHTQDB();
        //List category
        public List<Blog> GetListBlog()
        {
            return db.Blogs.ToList();
        }

        //Category Details
        public Blog GetDetails(string id)
        {
            string query = "select * from dbo.Blogs where id = '" + id + "'";
            return db.Database.SqlQuery<Blog>(query).FirstOrDefault();
        }

        //Category Create
        public void AddBlog(Blog blog)
        {
            db.Database.ExecuteSqlCommand(
                "insert into dbo.Blogs(ID, TITLE, CONTENT, DATEPOST, BLOGIMAGE) values (@A, @B, @C, @D, @E)",
                new SqlParameter("@A", blog.ID),
                new SqlParameter("@B", blog.TITLE),
                new SqlParameter("@C", blog.CONTENT),
                new SqlParameter("@D", blog.DATEPOST),
                new SqlParameter("@E", blog.BLOGIMAGE)
                );
        }

        //Category Edit
        public void UpdateBlog(Blog blog)
        {
            db.Entry(blog).State = EntityState.Modified;
            db.SaveChanges();
        }

        //Category Delete
        public void DeleteBlog(Blog blog)
        {
            db.Entry(blog).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public List<Blog> GetListSearch(string name)
        {
            string query = "select * from dbo.Blogs where TITLE like N'%" + name + "%' or CONTENT like N'%" + name + "%'";
            return db.Database.SqlQuery<Blog>(query).ToList();
        }
    }
}

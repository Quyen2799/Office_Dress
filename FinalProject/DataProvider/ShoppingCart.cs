using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DataProvider
{
    public class ShoppingCart
    {
        MyShopHTQDB db = new MyShopHTQDB();
        string ShoppingCartID { get; set; }
        public const string CartSessionKey = "CartID";
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartID = cart.GetCartId(context);
            return cart;
        }

        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Product Pro)
        {
            try
            {
                var cartItem = db.Carts.SingleOrDefault(c => c.CartID == ShoppingCartID && c.idProduct == Pro.ID);

                if (cartItem == null)
                {
                    // Create a new cart item if no cart item exists
                    cartItem = new Cart
                    {
                        idProduct = Pro.ID,
                        CartID = ShoppingCartID,
                        count = 1,
                        DateCreated = DateTime.Now
                    };
                    db.Carts.Add(cartItem);
                }
                else
                {
                    // If the item does exist in the cart, 
                    // then add one to the quantity
                    cartItem.count++;
                }
                // Save changes
                db.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            catch (System.Data.Entity.Core.EntityCommandCompilationException ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            catch (System.Data.Entity.Core.UpdateException ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            catch (System.Data.Entity.Infrastructure.DbUpdateException ex) //DbContext
            {
                Console.WriteLine(ex.InnerException);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = db.Carts.Single(cart => cart.CartID == ShoppingCartID && cart.RecordID == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.count > 1)
                {
                    cartItem.count--;
                    itemCount = cartItem.count;
                }
                else
                {
                    db.Carts.Remove(cartItem);
                }
                // Save changes
                db.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = db.Carts.Where(cart => cart.CartID == ShoppingCartID);

            foreach (var cartItem in cartItems)
            {
                db.Carts.Remove(cartItem);
            }
            // Save changes
            db.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return db.Carts.Where(cart => cart.CartID == ShoppingCartID).ToList();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in db.Carts
                          where cartItems.CartID == ShoppingCartID
                          select (int?)cartItems.count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }
        public int GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            int? total = (from cartItems in db.Carts
                          where cartItems.CartID == ShoppingCartID
                          select (int?)cartItems.count *
                          cartItems.Product.PRICE).Sum();

            return total ?? 0;
        }

        public int CreateOrder(Order order)
        {
            int orderTotal = 0;

            var cartItems = GetCartItems();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    IDPRODUCT = item.idProduct,
                    IDORDER = order.ID,
                    UNITPRICE = item.Product.PRICE,
                    COUNT = item.count
                };
                // Set the order total of the shopping cart
                orderTotal += (item.count * item.Product.PRICE);

                db.OrderDetails.Add(orderDetail);

            }
            // Set the order's total to the orderTotal count
            order.TOTALPRICE = orderTotal;

            // Save the order
            db.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.ID;
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        public void MigrateCart(string userName)
        {
            var shoppingCart = db.Carts.Where(c => c.CartID == ShoppingCartID);

            foreach (Cart item in shoppingCart)
            {
                item.CartID = userName;
            }
            db.SaveChanges();
        }
    }
}

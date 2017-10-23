using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Assignment_4
{
    public class DataService
    {
        private NorthwindContext db = new NorthwindContext("localhost", "northwind", "root", "root3010");

        /**
            Orders Requests
         */
        public Order GetOrder(int id)
        {
            var order = db.Orders.Include(x => x.OrderDetails).FirstOrDefault(y => y.Id == id);

            foreach (var od in order.OrderDetails)
            {
                od.Order = order;
                od.Product = db.Products.Include(x => x.Category).FirstOrDefault(y => y.Id == od.ProductId);
            }

            return order;
        }

        public List<dynamic> GetOrdersByShipName(string shipname)
        {
            var orders = db.Orders.Where(x => x.ShipName.Equals(shipname))
                        .Select(x => new { x.Id, x.OrderDate, x.ShipName, x.ShipCity }).ToList<dynamic>();
            return orders;
        }

        public List<dynamic> GetOrders()
        {
            var orders = db.Orders.Select(x => new {x.Id, x.OrderDate, x.ShipName, x.ShipCity}).ToList<dynamic>();
            return orders;
        }


        /**
            Order details Requests
         */
        public List<Orderdetail> GetOrderDetailsByOrderId(int orderid)
        {
            var order = db.Orderdetails.Where(x => x.Id == orderid)
                        .Include(x => x.Product)
                        .Include(x => x.Order).ToList<Orderdetail>(); 

            return order;
        }

        public List<Orderdetail> GetOrderDetailsByProductId(int productId)
        {
            var order = db.Orderdetails.Where(x => x.ProductId == productId)
                        .Include(x => x.Product)
                        .Include(x => x.Order).ToList<Orderdetail>(); 

            return order;
        }

        /**
            Products Requests
         */
        public Product GetProduct(int id)
        {
            var product = db.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);

            return product;
        }

        public List<Product> GetProductsByPartialName(string partial_name)
        {
            var products = db.Products.Include(x => x.Category)
                        .Where(x => x.ProductName.ToLower().Contains(partial_name.ToLower())).ToList<Product>();
            
            return products;
        }

        public List<Product> GetProductsByCategoryId(int category_id)
        {
            var products = db.Products.Include(x => x.Category).Where(x => x.Category.Id == category_id).ToList<Product>();

            return products;
        }

        /**
            Categories Requests
         */
        public Category GetCategory(int id)
        {
            var category = this.db.Categories.Find(id);

            return category;
        }

        public List<Category> GetCategories()
        {

            var categories = this.db.Categories.ToList<Category>();

            return categories;
        }

        public Category CreateCategory(string name, string description)
        {
            Category category = new Category { Name = name, Description = description };
            category = db.Categories.Add(category).Entity;
            db.SaveChanges();

            return category;
        }

        public bool UpdateCategory(int id, string new_name, string new_description)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
                return false;
            else
            {
                category.Name = new_name;
                category.Description = new_description;
                db.SaveChanges();
            }
            return true;
        }

        public bool DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null) return false;
            else
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
            return true;
        }
    }
}
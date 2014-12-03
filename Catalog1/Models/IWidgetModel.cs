using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog1.Models
{
    public interface IWidgetModel : IDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<CartItem> CartItems { get; set; }
        DbSet<ProductReview> ProductReviews { get; set; }
        DbSet<Article> Articles { get; set; }
        DbSet<Email> Emails { get; set; }
    }
}

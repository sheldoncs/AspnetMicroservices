
using Discount.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Data.Database
{
    public class DiscountContext : DbContext, IDiscountContext
    {
        public DbSet<Coupon> Coupons { get; set; }

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {

        }
        public async Task<TData> Entry<TData>(TData data, string operation)
        {
            if (operation.Equals("ADD")){
                this.Entry(data).State = EntityState.Added;
            } else if (operation.Equals("UPDATE")){
                this.Entry(data).State = EntityState.Modified;
            }
            base.SaveChanges();
            

            return await Task.FromResult(data);
        }

        public Task<int> DeleteDiscount(string productName)
        {
            var remove = from cp in Coupons
                         where cp.ProductName == productName
                         select cp;
            

            foreach (var detail in remove)
            {
                this.Remove(detail);
            }

            
            return Task.FromResult(base.SaveChanges());
            
        }

        public Task<Coupon> GetDiscount(string productName)
        {

            Coupon coupon = Coupons.FirstOrDefaultAsync<Coupon>(x => x.ProductName == productName).Result;
        
            return Task.FromResult(coupon);
        }
    }
}

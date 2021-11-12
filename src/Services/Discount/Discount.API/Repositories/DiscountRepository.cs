using Discount.API.Data.Database;
using Discount.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IDiscountContext _discountContext;

        private readonly ILogger<DiscountRepository> _logger;
        private const string LoggerScope = nameof(DiscountRepository);

        public DiscountRepository(ILogger<DiscountRepository> logger, IDiscountContext discountContext)
        {
            _logger = logger;
            _discountContext = discountContext;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            Boolean success = false;
            using (_logger.BeginScope("{Scope}: {Method}", LoggerScope, nameof(CreateDiscount)))
            {
                if (coupon == null)
                {
                    throw new ArgumentNullException(nameof(coupon));
                }
                try
                {
                    Coupon create = await _discountContext.Entry<Coupon>(coupon, "ADD");

                    success = true;

                }
                catch (DbUpdateException ex)
                {


                    _logger.LogError(

                        ex,
                        "Discount already exists in the database. {@coupon}.",
                        coupon);

                    throw new Exception("Discount already exists in the database.", ex);

                }

                return await Task.FromResult(success);

            }
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            bool success = false;
            using (_logger.BeginScope("{Scope}: {Method}", LoggerScope, nameof(DeleteDiscount)))
            {
                if (productName == null)
                {
                    throw new ArgumentNullException(nameof(productName));
                }

                try
                {
                    var deleted = await _discountContext.DeleteDiscount(productName);
                    success = true;

                } catch (DbUpdateException ex)
                {

                    _logger.LogError(

                        ex,
                        "Error Deleting Coupon with productName. {@productName}.",
                        productName);

                    throw new Exception("Error Deleting Coupon.", ex);
                }
                return success;
            }
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            
            if (productName == null)
            {
                return new Coupon { Amount = 0, Description = "No Discount Desc", ProductName = productName };
            }
            Coupon coupon = await _discountContext.GetDiscount(productName);

            return coupon;
            
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            Boolean success = false;
            using (_logger.BeginScope("{Scope}: {Method}", LoggerScope, nameof(CreateDiscount)))
            {
                if (coupon == null)
                {
                    throw new ArgumentNullException(nameof(coupon));
                }
                try
                {
                    Coupon create = await _discountContext.Entry<Coupon>(coupon, "UPDATE");

                    success = true;

                }
                catch (DbUpdateException ex)
                {


                    _logger.LogError(

                        ex,
                        "Error Updating Coupon: {@coupon}.",
                        coupon);

                    throw new Exception("Error Updating Coupon.", ex);

                }

                return await Task.FromResult(success);

            }
        }
    }
}

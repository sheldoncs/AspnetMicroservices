using Discount.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Discount.API.Data.Database
{
    public interface IDiscountContext : IDisposable
    {
        Task<TData> Entry<TData>(TData data, string operation);
        Task<int> DeleteDiscount(string productName);
        public Task<Coupon> GetDiscount(string productName);
    }
}

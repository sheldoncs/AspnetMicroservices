using Discount.Grpc.Entities;
using System;
using System.Threading.Tasks;

namespace Discount.Grpc.Data.Database
{
    public interface IDiscountContext : IDisposable
    {
        Task<TData> Entry<TData>(TData data, string operation);
        Task<int> DeleteDiscount(string productName);
        public Task<Coupon> GetDiscount(string productName);
    }
}

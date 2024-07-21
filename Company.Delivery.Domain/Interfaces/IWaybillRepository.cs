using Company.Delivery.Core;

namespace Company.Delivery.Domain.Interfaces
{
    public interface IWaybillRepository
    {
        Task<Waybill> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Waybill> CreateAsync(Waybill waybill, CancellationToken cancellationToken);
        Task<Waybill> UpdateByIdAsync(Guid id, Waybill waybill, CancellationToken cancellationToken);
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}

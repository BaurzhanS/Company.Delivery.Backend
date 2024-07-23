using Company.Delivery.Core;
using Company.Delivery.Domain;
using Company.Delivery.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Company.Delivery.Database.Repos
{
    public class WaybillRepository : IWaybillRepository
    {
        private readonly DeliveryDbContext _dbContext;
        public WaybillRepository(DeliveryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Waybill> CreateAsync(Waybill waybill, CancellationToken cancellationToken)
        {
            await _dbContext.Waybills.AddAsync(waybill, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return waybill;
        }
        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var waybill = await _dbContext.Waybills.FindAsync(id, cancellationToken);
            if (waybill is null)
            {
                throw new EntityNotFoundException();
            }

            _dbContext.Waybills.Remove(waybill);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<Waybill?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Waybills.FindAsync(id, cancellationToken);
        }
        public async Task<Waybill> UpdateByIdAsync(Guid id, Waybill waybill, CancellationToken cancellationToken)
        {
            _dbContext.Entry(waybill).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return waybill;
        }
    }
}

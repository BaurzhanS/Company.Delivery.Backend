using Company.Delivery.Core;
using Company.Delivery.Domain;
using Company.Delivery.Domain.Dto;
using Company.Delivery.Domain.Interfaces;

namespace Company.Delivery.Infrastructure;

public class WaybillService : IWaybillService
{
    private readonly IWaybillRepository _waybillRepository;
    public WaybillService(IWaybillRepository waybillRepository)
    {
        _waybillRepository = waybillRepository;
    }
    public async Task<WaybillDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: Если сущность не найдена по идентификатору, кинуть исключение типа EntityNotFoundException
        var response = await _waybillRepository.GetByIdAsync(id, cancellationToken);

        if (response is null)
        {
            throw new EntityNotFoundException();
        }

        var cargoItems = response.Items?.Select(i => new CargoItemDto
        {
            Id = i.Id,
            Name = i.Name,
            Number = i.Number,
            WaybillId = i.WaybillId
        });

        var waybill = new WaybillDto
        {
            Date = response.Date,
            Id = response.Id,
            Number = response.Number,
            Items = cargoItems
        };

        return waybill;
    }

    public async Task<WaybillDto> CreateAsync(WaybillCreateDto data, CancellationToken cancellationToken)
    {
        var cargoItems = data.Items?.Select(i => new CargoItem
        {
            Name = i.Name,
            Number = i.Number
        }).ToArray();

        var waybill = new Waybill
        {
            Date = data.Date,
            Items = cargoItems,
            Number = data.Number
        };

        var response = await _waybillRepository.CreateAsync(waybill, cancellationToken);

        var cargoItemsDto = response.Items?.Select(i => new CargoItemDto
        {
            Name = i.Name,
            Number = i.Number,
            Id = i.Id,
            WaybillId = i.WaybillId
        });

        var waybillDto = new WaybillDto
        {
            Id = response.Id,
            Number = response.Number,
            Date = response.Date,
            Items = cargoItemsDto
        };

        return waybillDto;
    }

    public async Task<WaybillDto> UpdateByIdAsync(Guid id, WaybillUpdateDto data, CancellationToken cancellationToken)
    {
        // TODO: Если сущность не найдена по идентификатору, кинуть исключение типа EntityNotFoundException
        var cargoItems = data.Items?.Select(i => new CargoItem
        {
            Name = i.Name,
            Number = i.Number
        }).ToArray();

        var waybill = new Waybill
        {
            Date = data.Date,
            Items = cargoItems,
            Number = data.Number
        };
        var response = await _waybillRepository.UpdateByIdAsync(id, waybill, cancellationToken);

        if (response is null)
        {
            throw new EntityNotFoundException();
        }

        var cargoItemsDto = response.Items?.Select(i => new CargoItemDto
        {
            Name = i.Name,
            Number = i.Number,
            Id = i.Id,
            WaybillId = i.WaybillId
        });

        var waybillDto = new WaybillDto
        {
            Id = response.Id,
            Number = response.Number,
            Date = response.Date,
            Items = cargoItemsDto
        };

        return waybillDto;
    }

    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: Если сущность не найдена по идентификатору, кинуть исключение типа EntityNotFoundException

        throw new NotImplementedException();
    }
}
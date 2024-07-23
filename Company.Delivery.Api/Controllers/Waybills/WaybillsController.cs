using Company.Delivery.Api.Controllers.Waybills.Request;
using Company.Delivery.Api.Controllers.Waybills.Response;
using Company.Delivery.Domain;
using Company.Delivery.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Company.Delivery.Api.Controllers.Waybills;

/// <summary>
/// Waybills management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WaybillsController : ControllerBase
{
    private readonly IWaybillService _waybillService;

    /// <summary>
    /// Waybills management
    /// </summary>
    public WaybillsController(IWaybillService waybillService) => _waybillService = waybillService;

    /// <summary>
    /// Получение Waybill
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(WaybillResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если найдено или кодом 404 если не найдено
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        try
        {
            var response = await _waybillService.GetByIdAsync(id, cancellationToken);

            var cargoItems = response.Items?.Select(i => new CargoItemResponse
            {
                Id = i.Id,
                Name = i.Name,
                Number = i.Number,
                WaybillId = i.WaybillId
            });

            var waybill = new WaybillResponse
            {
                Id = response.Id,
                Date = response.Date,
                Number = response.Number,
                Items = cargoItems
            };

            return Ok(waybill);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Создание Waybill
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(WaybillResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] WaybillCreateRequest request, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если успешно создано
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        var cargoItems = request.Items?.Select(i => new CargoItemCreateDto
        {
            Name = i.Name,
            Number = i.Number
        });

        var waybill = new WaybillCreateDto
        {
            Date = request.Date,
            Number = request.Number,
            Items = cargoItems
        };

        var response = await _waybillService.CreateAsync(waybill, cancellationToken);

        var cargoItemsResponse = response.Items?.Select(i => new CargoItemResponse
        {
            Name = i.Name,
            Number = i.Number,
            Id = i.Id,
            WaybillId = i.WaybillId
        });

        var waybillResponse = new WaybillResponse
        {
            Id = response.Id,
            Date = response.Date,
            Items = cargoItemsResponse,
            Number = response.Number
        };

        if (waybillResponse != null)
        {
            return Ok(waybillResponse);
        }
        else
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Редактирование Waybill
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(WaybillResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateByIdAsync(Guid id, [FromBody] WaybillUpdateRequest request, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если найдено и изменено, или 404 если не найдено
        // TODO: WaybillsControllerTests должен выполняться без ошибок

        var cargoItems = request.Items?.Select(i => new CargoItemUpdateDto
        {
            Name = i.Name,
            Number = i.Number
        });

        var waybill = new WaybillUpdateDto
        {
            Date = request.Date,
            Number = request.Number,
            Items = cargoItems
        };
        try
        {
            var response = await _waybillService.UpdateByIdAsync(id, waybill, cancellationToken);
            var cargoItemsResponse = response.Items?.Select(i => new CargoItemResponse
            {
                Name = i.Name,
                Number = i.Number,
                Id = i.Id,
                WaybillId = i.WaybillId
            });

            var waybillResponse = new WaybillResponse
            {
                Id = response.Id,
                Date = response.Date,
                Items = cargoItemsResponse,
                Number = response.Number
            };
            return Ok(waybillResponse);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        };
    }

    /// <summary>
    /// Удаление Waybill
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если найдено и удалено, или 404 если не найдено
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        try
        {
            await _waybillService.DeleteByIdAsync(id, cancellationToken);

            return Ok();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    }
}
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class DetallePedidoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DetallePedidoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DetallePedidoDto>>> Get()
    {
        var entities = await _unitOfWork.DetallePedidos.GetAllAsync();
        return _mapper.Map<List<DetallePedidoDto>>(entities);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetallePedidoDto>> Post(DetallePedidoDto modelDto)
    {
        var entity = _mapper.Map<DetallePedido>(modelDto);
        _unitOfWork.DetallePedidos.Add(entity);
        await _unitOfWork.SaveAsync();
        if (entity == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), modelDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetallePedidoDto>> Put(int id, [FromBody] DetallePedidoDto modelDto)
    {
        if (modelDto == null) return NotFound();
        var entity = _mapper.Map<DetallePedido>(modelDto);
        _unitOfWork.DetallePedidos.Update(entity);
        await _unitOfWork.SaveAsync();
        return modelDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _unitOfWork.DetallePedidos.GetByIdAsync(id);
        if (entity == null) return NotFound();
        _unitOfWork.DetallePedidos.Remove(entity);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
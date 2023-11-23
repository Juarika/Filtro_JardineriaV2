using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class PedidoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PedidoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PedidoDto>>> Get()
    {
        var entities = await _unitOfWork.Pedidos.GetAllAsync();
        return _mapper.Map<List<PedidoDto>>(entities);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PedidoDto>> Post(PedidoDto modelDto)
    {
        var entity = _mapper.Map<Pedido>(modelDto);
        _unitOfWork.Pedidos.Add(entity);
        await _unitOfWork.SaveAsync();
        if (entity == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post),new {id = entity.CodigoPedido}, modelDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PedidoDto>> Put(int id, [FromBody] PedidoDto modelDto)
    {
        if (modelDto == null) return NotFound();
        var entity = _mapper.Map<Pedido>(modelDto);
        _unitOfWork.Pedidos.Update(entity);
        await _unitOfWork.SaveAsync();
        return modelDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _unitOfWork.Pedidos.GetByIdAsync(id);
        if (entity == null) return NotFound();
        _unitOfWork.Pedidos.Remove(entity);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet("GetPedidosEstado")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetPedidosEstado()
    {
        var entities = await _unitOfWork.Pedidos.GetPedidosEstado();
        return Ok(entities);
    }
}
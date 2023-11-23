using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class ProductoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
    {
        var entities = await _unitOfWork.Productos.GetAllAsync();
        return _mapper.Map<List<ProductoDto>>(entities);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductoDto>> Post(ProductoDto modelDto)
    {
        var entity = _mapper.Map<Producto>(modelDto);
        _unitOfWork.Productos.Add(entity);
        await _unitOfWork.SaveAsync();
        if (entity == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post),new {id = entity.CodigoProducto}, modelDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductoDto>> Put(int id, [FromBody] ProductoDto modelDto)
    {
        if (modelDto == null) return NotFound();
        var entity = _mapper.Map<Producto>(modelDto);
        _unitOfWork.Productos.Update(entity);
        await _unitOfWork.SaveAsync();
        return modelDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _unitOfWork.Productos.GetByIdAsync(id);
        if (entity == null) return NotFound();
        _unitOfWork.Productos.Remove(entity);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet("GetSinPedido")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProdNomDesDto>>> GetSinPedido()
    {
        var entities = await _unitOfWork.Productos.GetSinPedido();
        return _mapper.Map<List<ProdNomDesDto>>(entities);
    }
}
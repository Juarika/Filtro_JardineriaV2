using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class PagoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PagoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PagoDto>>> Get()
    {
        var entities = await _unitOfWork.Pagos.GetAllAsync();
        return _mapper.Map<List<PagoDto>>(entities);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagoDto>> Post(PagoDto modelDto)
    {
        var entity = _mapper.Map<Pago>(modelDto);
        _unitOfWork.Pagos.Add(entity);
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
    public async Task<ActionResult<PagoDto>> Put(int id, [FromBody] PagoDto modelDto)
    {
        if (modelDto == null) return NotFound();
        var entity = _mapper.Map<Pago>(modelDto);
        _unitOfWork.Pagos.Update(entity);
        await _unitOfWork.SaveAsync();
        return modelDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _unitOfWork.Pagos.GetByIdAsync(id);
        if (entity == null) return NotFound();
        _unitOfWork.Pagos.Remove(entity);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet("GetPorAnio")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PagoDto>>> GetPorAño(int anio, string formaPago)
    {
        var entities = await _unitOfWork.Pagos.GetPorAño(anio, formaPago);
        return _mapper.Map<List<PagoDto>>(entities);
    }
}
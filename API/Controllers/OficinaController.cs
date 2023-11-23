using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class OficinaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public OficinaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<OficinaDto>>> Get()
    {
        var entities = await _unitOfWork.Oficinas.GetAllAsync();
        return _mapper.Map<List<OficinaDto>>(entities);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OficinaDto>> Post(OficinaDto modelDto)
    {
        var entity = _mapper.Map<Oficina>(modelDto);
        _unitOfWork.Oficinas.Add(entity);
        await _unitOfWork.SaveAsync();
        if (entity == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post),new {id = entity.CodigoOficina}, modelDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OficinaDto>> Put(int id, [FromBody] OficinaDto modelDto)
    {
        if (modelDto == null) return NotFound();
        var entity = _mapper.Map<Oficina>(modelDto);
        _unitOfWork.Oficinas.Update(entity);
        await _unitOfWork.SaveAsync();
        return modelDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _unitOfWork.Oficinas.GetByIdAsync(id);
        if (entity == null) return NotFound();
        _unitOfWork.Oficinas.Remove(entity);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class EmpleadoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public EmpleadoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmpleadoDto>>> Get()
    {
        var entities = await _unitOfWork.Empleados.GetAllAsync();
        return _mapper.Map<List<EmpleadoDto>>(entities);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmpleadoDto>> Post(EmpleadoDto modelDto)
    {
        var entity = _mapper.Map<Empleado>(modelDto);
        _unitOfWork.Empleados.Add(entity);
        await _unitOfWork.SaveAsync();
        if (entity == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post),new {id = entity.CodigoEmpleado}, modelDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmpleadoDto>> Put(int id, [FromBody] EmpleadoDto modelDto)
    {
        if (modelDto == null) return NotFound();
        var entity = _mapper.Map<Empleado>(modelDto);
        _unitOfWork.Empleados.Update(entity);
        await _unitOfWork.SaveAsync();
        return modelDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _unitOfWork.Empleados.GetByIdAsync(id);
        if (entity == null) return NotFound();
        _unitOfWork.Empleados.Remove(entity);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet("GetJefe")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<JefeDto>>> GetJefe()
    {
        var entities = await _unitOfWork.Empleados.GetJefe();
        return _mapper.Map<List<JefeDto>>(entities);
    }
}
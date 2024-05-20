using AutoMapper;
using Azure;
using CervejaAPITeste.Data;
using CervejaAPITeste.Data.Dtos;
using CervejaAPITeste.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CervejaAPITeste.Controllers;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("[controller]")]
public class CervejaController : Controller
{
    private CervejaContext _context;
    private IMapper _mapper;

    public CervejaController(CervejaContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Cadastra uma cerveja ao banco de dados
    /// </summary>
    /// <param name="cervejaDto">Objeto com os nome necessários para cadastro de uma cerveja</param>
    /// <returns code="201">Caso inserção seja feita com sucesso</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaFilme([FromBody] CreatCervejaDto cervejaDto)
    {
        Cerveja cerva = _mapper.Map< Cerveja > (cervejaDto);
        _context.Cervejas.Add(cerva);
        _context.SaveChanges();
        return CreatedAtAction(nameof(ConsultarCervejaPorId), new { id = cerva.Id }, cerva);
    }
    /// <summary>
    /// Consulta o banco de dados e retorna cervejas cadastradas.
    /// </summary>
    /// <param name="skip">Número de itens a pular</param>
    /// <param name="take">Quantidade que retornará na consulta</param>
    /// <returns code="200">Quando a consulta é realizada com sucesso</returns>
    /// <returns code="404">Quando a consulta não é encontrada</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadCervejaDto> ConsultarCerveja([FromQuery] int skip = 0, int take = 50)
    {
        return _mapper.Map<List<ReadCervejaDto>>(_context.Cervejas.Skip(skip).Take(take));
    }
    /// <summary>
    /// Consulta filtrando pela propriedade {Id}.
    /// </summary>
    /// <param name="id">Propriedade utilizada na consulta</param>
    /// <returns code="200">Quando a consulta é realizada com sucesso.</returns>
    /// <returns code="404">Quando a consulta não é encontrada</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ConsultarCervejaPorId(int id)
    {
        var cerveja = _context.Cervejas.FirstOrDefault(cerv => cerv.Id == id);

        if (cerveja == null) return NotFound();

        var cervejaDto = _mapper.Map<ReadCervejaDto>(cerveja);

        return cerveja == null ? NotFound() : Ok(cervejaDto);
    }
    /// <summary>
    /// Atualiza todas as propriedades da cerveja.
    /// </summary>
    /// <param name="id">Propriedade utilizada na consulta</param>
    /// <param name="cervejaDto">Objeto necessário para atualização de todas as propriedades da cerveja</param>
    /// <returns code="204">Retorno quando a atualização é realizada com sucesso</returns>
    /// <returns code="404">Retorno quando o item não é encontrado</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AtualizaCerveja(int id, [FromBody] UpdateCervejaDto cervejaDto)
    {
        var cerveja = _context.Cervejas.FirstOrDefault(cerveja => cerveja.Id == id);
        if (cerveja == null) return NotFound();

        _mapper.Map(cervejaDto, cerveja);
        _context.SaveChanges();

        return NoContent();
    }
    /// <summary>
    /// Atualiza as propriedades da cerveja, sendo necessário uma ou mais propriedades
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patch"></param>
    /// <returns code="204">Retorno quando a atualização é realizada com sucesso</returns>
    /// <returns code="404">Retorno quando o item não é encontrado</returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AtualizaCervejaParcial(int id, JsonPatchDocument<UpdateCervejaDto> patch)
    {
        var cerveja = _context.Cervejas.FirstOrDefault(cerveja => cerveja.Id == id);
        if (cerveja == null) return NotFound();

        var cervejaParaAtualizar = _mapper.Map<UpdateCervejaDto>(cerveja);

        patch.ApplyTo(cervejaParaAtualizar, ModelState);

        if (!TryValidateModel(cervejaParaAtualizar)) return ValidationProblem(ModelState);

        _mapper.Map(cervejaParaAtualizar, cerveja);
        _context.SaveChanges();

        return NoContent();
    }
    /// <summary>
    /// Remove uma cerveja existente no banco de dados
    /// </summary>
    /// <param name="id">Propriedade obrigatória para remoção</param>
    /// <param name="patch"></param>
    /// <returns code="204">Retorno quando a remoção é realizada com sucesso</returns>
    /// <returns code="404">Retorno quando o item não é encontrado</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeletaCerveja(int id)
    {
        var cerveja = _context.Cervejas.FirstOrDefault(cerveja => cerveja.Id == id);
        if (cerveja == null) return NotFound();

        _context.Remove(cerveja);
        _context.SaveChanges();

        return NoContent();
    }
}

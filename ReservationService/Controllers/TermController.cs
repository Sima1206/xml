using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReservationService.Configuration;
using ReservationService.Model;
using ReservationService.Model.DTO;
using ReservationService.Services;

namespace ReservationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TermController : BaseController<Term>
{
    private readonly IMapper _mapper;

    
    public TermController(ProjectConfiguration configuration, IMapper mapper) : base(configuration)
    {
        _mapper = mapper;

    }
    
    [Route("createTerm")]
    [HttpPost]
    public IActionResult CreateTerm(TermDTO dto)
    {
        TermService.Services.TermService termService = new TermService.Services.TermService();
            
        if (dto == null)
        {
            return BadRequest(new {message = "Wrong term, please check your dates"});

        }

        Term newTerm = termService.CreateTerm(dto);

        if (newTerm == null)
        {
            return BadRequest(new {message = "Wrong term, please check your dates"});
        }
            
        return Ok(newTerm);
    }


    [Route("updateTerm")]
    [HttpPut]
    public IActionResult UpdateTerm(long id, TermDTO dto)
    {
        return Ok();
    }
}
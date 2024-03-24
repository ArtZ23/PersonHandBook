using Microsoft.AspNetCore.Mvc;
using PersonHandBook.Dtos;
using PersonHandBook.Models;
using PersonHandBook.Services;

namespace PersonHandBook.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PersonController : Controller
	{
		private readonly IPersonService _personService;

		public PersonController(IPersonService personService)
		{
			_personService = personService;
		}

		[HttpGet("person/{id}")]
		public async Task<IActionResult> GetPerson(int id)
		{
			try
			{
				var person = await _personService.GetPerson(id);
				return Ok(person);
			}
			catch (Exception e)
			{
				return BadRequest(new { Message = e.Message, StackTrace = e.StackTrace });
			}
		}

		[HttpGet("filteredperson")]
		public async Task<IActionResult> FilterPersons([FromQuery] PersonFilterModel person)
		{
			try
			{
				var filteredPersons = await _personService.FilterPersonsAsync(person);
				return Ok(filteredPersons);
			}
			catch (Exception e)
			{
				return BadRequest(new { Message = e.Message, StackTrace = e.StackTrace });
			}
		}


		[HttpGet("relatedpersonsreport")]
		public async Task<IActionResult> GetRelatedPersonReport(int id)
		{
			try
			{
				var filteredPersons = await _personService.GetRelatedPersonReport(id);
				return Ok(filteredPersons);
			}
			catch (Exception e)
			{
				return BadRequest(new { Message = e.Message, StackTrace = e.StackTrace });
			}
		}

		[HttpPost("person")]
		public async Task<IActionResult> AddPerson([FromForm] PersonDto person)
		{
			try
			{
				await _personService.AddPerson(person);
				return Ok("Person was created succesfully");
			}
			catch (Exception e)
			{
				return BadRequest(new { message = e.Message });
			}
		}

		[HttpPut("person/{id}")]
		public async Task<IActionResult> UpdatePerson(int id, [FromForm] PersonUpdateModel person)
		{
			try
			{
				await _personService.UpdatePerson(id, person);
				return Ok("Person was upadted succesfully");
			}
			catch (Exception e)
			{
				return BadRequest(new { Message = e.Message, StackTrace = e.StackTrace });
			}
		}

		[HttpDelete("person/{id}")]
		public async Task<IActionResult> DeletePerson(int id)
		{
			try
			{
				await _personService.DeletePerson(id);
				return Ok("Person was deleted succesfully");
			}
			catch (Exception e)
			{
				return BadRequest(new { Message = e.Message, StackTrace = e.StackTrace });
			}
		}

		[HttpDelete("phone/{id}")]
		public async Task<IActionResult> DeletePhone(int id)
		{
			try
			{
				await _personService.DeletePhone(id);
				return Ok("Phone was deleted succesfully");
			}
			catch (Exception e)
			{
				return BadRequest(new { Message = e.Message, StackTrace = e.StackTrace });
			}
		}

		[HttpDelete("relatedperson/{id}")]
		public async Task<IActionResult> DeleteRelatedPerson(int id)
		{
			try
			{
				await _personService.DeleteRelatedPerson(id);
				return Ok("Related Person was deleted succesfully");
			}
			catch (Exception e)
			{
				return BadRequest(new { Message = e.Message, StackTrace = e.StackTrace });
			}
		}
	}
}

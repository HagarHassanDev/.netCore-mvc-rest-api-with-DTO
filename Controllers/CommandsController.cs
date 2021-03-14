using System.Collections.Generic;
using AutoMapper;
using commandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace commandAPI.Controllers
{
    //api/commands
    // [Route("api/[Controller]")]
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly Data.ICommandRepo _repository;
        private readonly IMapper _mapper;
        public CommandsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Get /api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        //Get api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound();
        }

        // POST /api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandUpdateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();
            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
            // to create route for the record to get it 
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
            // return Ok(commandReadDto);
        }

        // PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromrepo = _repository.GetCommandById(id);
            if (commandModelFromrepo == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandModelFromrepo);
            _repository.UpdateCommand(commandModelFromrepo);
            _repository.SaveChanges();
            return NoContent();
        }

        //PATCH  api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult partialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDocumnet)
        {
            var commandModelFromrepo = _repository.GetCommandById(id);
            if (commandModelFromrepo == null)
            {
                return NotFound();
            }
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromrepo);
            patchDocumnet.ApplyTo(commandToPatch, ModelState);
            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(commandToPatch, commandModelFromrepo);
            _repository.UpdateCommand(commandModelFromrepo);
            _repository.SaveChanges();
            return NoContent();
        }


        //DELETE /api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id )
        {
            var commandModelFromrepo = _repository.GetCommandById(id);
            if (commandModelFromrepo == null)
            {
                return NotFound();
            }
            _repository.DeleteCommand(commandModelFromrepo);
            _repository.SaveChanges();
            return NoContent();

        }

    }
}
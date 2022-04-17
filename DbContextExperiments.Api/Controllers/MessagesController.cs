using System.Linq;
using System.Threading.Tasks;
using DbContextExperiments.Api.Models.Foundations.Messages;
using DbContextExperiments.Api.Models.Foundations.Messages.Exceptions;
using DbContextExperiments.Api.Services.Orchestrations.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RESTFulSense.Controllers;

namespace DbContextExperiments.Api.Controllers;

[ApiController]
[Route(template: "api/[controller]")]
public class MessagesController : RESTFulController
{
    private readonly IMessageOrchestrationService messageService;

    public MessagesController(IMessageOrchestrationService messageService)
        => this.messageService = messageService;

    [HttpGet]
    [EnableQuery]
    public ActionResult<IQueryable<Message>> Get()
    {
        try
        {
            IQueryable<Message> profiles = this.messageService.RetrieveAll();

            return Ok(profiles);
        }
        catch (MessageOrchestrationServiceDependencyException messageOrchestrationServiceDependencyException)
        {
            return InternalServerError(messageOrchestrationServiceDependencyException);
        }
        catch (MessageOrchestrationServiceException messageOrchestrationServiceException)
        {
            return InternalServerError(messageOrchestrationServiceException);
        }
    }

    [HttpPost]
    public async ValueTask<ActionResult<Message>> PostMessageAsync(Message message)
    {
        try
        {
            Message persistedMessage =
                await this.messageService.InsertMessageAsync(message);

            return Created(persistedMessage);
        }
        catch (MessageValidationException messageValidationException)
        {
            return BadRequest(messageValidationException.InnerException);
        }
        catch (MessageOrchestrationServiceDependencyValidationException messageOrchestrationServiceDependencyValidationException)
           when (messageOrchestrationServiceDependencyValidationException.InnerException is AlreadyExistsMessageException)
        {
            return Conflict(messageOrchestrationServiceDependencyValidationException.InnerException);
        }
        catch (MessageOrchestrationServiceDependencyException messageOrchestrationServiceDependencyException)
        {
            return InternalServerError(messageOrchestrationServiceDependencyException);
        }
        catch (MessageOrchestrationServiceException messageOrchestrationServiceException)
        {
            return InternalServerError(messageOrchestrationServiceException);
        }
    }

    //[HttpGet("{id}")]
    //public async Task<ActionResult<Message>> GetMessage(Guid id)
    //{
    //    var message = await messageService.FindMessageAsync(id);

    //    if (message is null)
    //    {
    //        return NotFound();
    //    }

    //    return message;
    //}

    //[HttpPut("{id}")]
    //public async Task<IActionResult> PutMessage(Guid id, Message message)
    //{
    //    await messageService.UpdateMessageAsync(id, message);
    //    return NoContent();
    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteMessage(Guid id)
    //{
    //    await messageService.DeleteMessageByIdAsync(id);
    //    return NoContent();
    //}
}
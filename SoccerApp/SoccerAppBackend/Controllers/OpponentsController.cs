using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoccerAppBackend.Data;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpponentsController : Controller
    {
        private readonly IOpponentsService opponentsService;

        public OpponentsController(IOpponentsService opponentsService)
        {
            this.opponentsService = opponentsService;
        }

        public async Task<IActionResult> GetActiveOpponents()
        {
            List<OpponentDto> activeOpponents = await opponentsService.GetActiveOpponents();

            if (activeOpponents.Count > 0)
            {
                return Ok(activeOpponents);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{opponentId}")]
        public async Task<IActionResult> GetOpponentById(int opponentId)
        {
            OpponentDto opponent = await opponentsService.GetOpponentById(opponentId);

            if (opponent.OpponentId != 0)
            {
                return Ok(opponent);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOpponent(OpponentDto opponentToCreate)
        {
            OpponentDto opponent = await opponentsService.CreateOpponent(opponentToCreate);

            if (opponent.OpponentId != 0)
            {
                return Ok(opponent);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{opponentId}")]
        public async Task<IActionResult> UpdateOpponent(OpponentDto opponentToUpdate)
        {
            opponentToUpdate = await opponentsService.UpdateOpponent(opponentToUpdate);

            return Ok(opponentToUpdate);
        }

        [HttpDelete("{opponentId}")]
        public async Task<IActionResult> DeactivateOpponent(int opponentId)
        {
            OpponentDto opponentToDeactivate = await opponentsService.DeactivateOpponent(opponentId);

            return Ok(opponentToDeactivate);
        }
    }
}

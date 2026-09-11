using Microsoft.AspNetCore.Mvc;
using SoccerAppBackend.Data;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerGameStatsController : Controller
    {
        private readonly IPlayerGameStatsService playerGameStatsService;

        public PlayerGameStatsController(IPlayerGameStatsService playerGameStatsService)
        {
            this.playerGameStatsService = playerGameStatsService;
        }

        public async Task<IActionResult> GetAllPlayerGameStats()
        {
            List<PlayerGameStatDto> allPlayerGameStats = await playerGameStatsService.GetAllPlayerGameStats();

            return Ok(allPlayerGameStats);
        }

        [HttpGet("{playerGameStatId}")]
        public async Task<IActionResult> GetPlayerGameStatById(int playerGameStatId)
        {
            PlayerGameStatDto playerGameStat = await playerGameStatsService.GetPlayerGameStatById(playerGameStatId);

            if (playerGameStat.PlayerGameStatId != 0)
            {
                return Ok(playerGameStat);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayerGameStat(PlayerGameStatDto playerGameStatToCreate)
        {
            PlayerGameStatDto playerGameStat = await playerGameStatsService.CreatePlayerGameStat(playerGameStatToCreate);

            if (playerGameStat.PlayerGameStatId != 0)
            {
                return Ok(playerGameStat);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{playerGameStatId}")]
        public async Task<IActionResult> UpdatePlayerGameStat(PlayerGameStatDto playerGameStatToUpdate)
        {
            playerGameStatToUpdate = await playerGameStatsService.UpdatePlayerGameStat(playerGameStatToUpdate);

            return Ok(playerGameStatToUpdate);
        }
    }
}

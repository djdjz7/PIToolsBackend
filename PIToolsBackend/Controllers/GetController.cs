using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIToolsBackend.Models;

namespace PIToolsBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GetController : ControllerBase
    {
        private PostgresContext _context;

        public GetController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [EnableCors("AllowAll")]
        public CommonResponse<PlayerScore> PlayerScore([FromQuery(Name = "qq")] string playerQQID)
        {
            try
            {
                var player = _context.Players.FirstOrDefault(x =>
                    x.Qqnumber != null && x.Qqnumber.Trim() == playerQQID.Trim()
                );
                if (player is null)
                    return new CommonResponse<PlayerScore>
                    {
                        Error = "PLAYER_NOT_FOUND",
                        IsSuccess = false
                    };
                if (player.Banned == true)
                    return new CommonResponse<PlayerScore>
                    {
                        Error = "PLAYER_BANNED",
                        IsSuccess = false
                    };

                var scores = _context
                    .Scores.Where(x => x.Userid == player.Userid)
                    .Select(
                        (x) =>
                            new PlayerScenarioScore
                            {
                                ScenarioID = x.Scenarioid,
                                Score = x.Score1 ?? 0,
                                Rating = x.Rating ?? 0
                            }
                    );

                var tempArray = scores.ToArray();
                foreach (var score in tempArray)
                {
                    score.ScenarioName = GetScenarioName(score.ScenarioID);
                }

                return new CommonResponse<PlayerScore>
                {
                    Data = new PlayerScore
                    {
                        Player = new PlayerCondensed
                        {
                            QQID = player.Qqnumber,
                            UserID = player.Userid,
                            Nickname = player.Nickname,
                            Banned = player.Banned ?? false,
                            Potential = player.Potential ?? 0,
                            Rank = player.Rank ?? 0,
                            ScoreSum = player.Scoresum ?? 0,
                        },
                        ScenarioScores = tempArray,
                    },
                    IsSuccess = true
                };
            }
            catch
            {
                return new CommonResponse<PlayerScore>
                {
                    Error = "INTERNAL_SERVER_ERROR",
                    IsSuccess = false
                };
            }
        }

        private string GetScenarioName(int scenarioID)
        {
            var scenario = _context.Scenarios.FirstOrDefault(x => x.Scenarioid == scenarioID);
            if (scenario is null)
                return "Unknown";
            return scenario.Scenarioname ?? "Unknown";
        }

        [HttpGet]
        [EnableCors("AllowAll")]
        public CommonResponse<ScenarioResponse[]> Scenarios()
        {
            try
            {
                var data = _context.Scenarios.Select(x => new ScenarioResponse
                {
                    Author = x.Author,
                    Constant = x.Constant ?? 0,
                    Feature = x.Feature ?? 0,
                    ID = x.Scenarioid,
                    Name = x.Scenarioname,
                    Package = x.Packid,
                    ScoreMultiplier = x.Multiplier ?? 0,
                });
                return new CommonResponse<ScenarioResponse[]>
                {
                    Data = data.ToArray(),
                    IsSuccess = true
                };
            }
            catch
            {
                return new CommonResponse<ScenarioResponse[]>
                {
                    Error = "INTERNAL_SERVER_ERROR",
                    IsSuccess = false
                };
            }
        }
    }
}

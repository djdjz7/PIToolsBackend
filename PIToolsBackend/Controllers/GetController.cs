using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIToolsBackend.Models;

namespace PIToolsBackend.Controllers
{
    [Route("api/[controller]")]
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
        [Route("playerscore")]
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
            catch (Exception e)
            {
                return new CommonResponse<PlayerScore>
                {
                    Error = "INTERNAL_SERVER_ERROR" + Environment.NewLine + e.Message,
                    IsSuccess = false
                };
            }
        }

        private ScenarioResponse? GetScenarioResponseById(int scenarioId)
        {
            var scenario = _context.Scenarios.FirstOrDefault(x => x.Scenarioid == scenarioId);
            if (scenario is null)
                return null;
            return new ScenarioResponse
            {
                Author = scenario.Author,
                Constant = scenario.Constant ?? 0,
                Feature = scenario.Feature ?? 0,
                ID = scenario.Scenarioid,
                Name = scenario.Scenarioname,
                Package = GetPackageName(scenario.Packid),
                ScoreMultiplier = scenario.Multiplier ?? 0,
            };
        }

        private string GetScenarioName(int scenarioID)
        {
            var scenario = _context.Scenarios.FirstOrDefault(x => x.Scenarioid == scenarioID);
            if (scenario is null)
                return "Unknown";
            return scenario.Scenarioname ?? "Unknown";
        }
        private string GetPackageName(string? packageID)
        {
            var package = _context.Packagenames.FirstOrDefault(x => x.Packageid == packageID);
            if (package is null)
                return "Single";
            return package.Packagename1;
        }

        [HttpGet]
        [EnableCors("AllowAll")]
        [Route("scenarios/all")]
        public CommonResponse<ScenarioResponse[]> ScenariosAll()
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
                var tempArray = data.ToArray();
                foreach (var scenario in tempArray)
                {
                    scenario.Package = GetPackageName(scenario.Package);
                }
                return new CommonResponse<ScenarioResponse[]>
                {
                    Data = tempArray,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new CommonResponse<ScenarioResponse[]>
                {
                    Error = "INTERNAL_SERVER_ERROR" + Environment.NewLine + e.Message,
                    IsSuccess = false
                };
            }
        }

        [HttpGet]
        [EnableCors("AllowAll")]
        [Route("scenarios/random")]
        public CommonResponse<int> ScenariosRandomId()
        {
            try
            {
                var max = _context.Scenarios.Count();
                var index = Random.Shared.Next(0, max);
                var selectedId = _context.Scenarios.Skip(index).First().Scenarioid;
                return new CommonResponse<int>
                {
                    Data = selectedId,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new CommonResponse<int>
                {
                    Error = "INTERNAL_SERVER_ERROR" + Environment.NewLine + e.Message,
                    IsSuccess = false
                };
            }
        }

        [HttpGet]
        [EnableCors("AllowAll")]
        [Route("scenarios/scores")]
        public CommonResponse<ScenarioScoreListResponse> ScenarioScoresById([FromQuery(Name = "id")] int scenarioId)
        {
            var scenario = GetScenarioResponseById(scenarioId);
            if (scenario is null)
                return new CommonResponse<ScenarioScoreListResponse>
                {
                    Error = "SCENARIO_NOT_FOUND",
                    IsSuccess = false
                };
            var playerScores = _context.Scores.Where(x => x.Scenarioid == scenarioId).Select(x => new ScenarioPlayerScore
            {
                UserId = x.Userid,
                Potential = x.User.Potential ?? 0,
                Nickname = x.User.Nickname ?? "[UNKNOWN]",
                Score = x.Score1 ?? 0,
                QqId = x.User.Qqnumber ?? "[UNKNOWN]",
            });
            return new CommonResponse<ScenarioScoreListResponse>
            {
                Data = new ScenarioScoreListResponse
                {
                    Scenario = scenario,
                    Players = playerScores.ToArray(),
                },
                IsSuccess = true
            };
        }

        [HttpGet]
        [EnableCors("AllowAll")]
        [Route("packages/all")]
        public Packagename[] PackagesAll()
        {
            return _context.Packagenames.ToArray();
        }
    }
}

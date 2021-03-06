﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Priyarank.Data;
using Priyarank.Models;

namespace Priyarank.Controllers
{
    public class HomeController : Controller
    {
        private readonly RankContext _context;
        private Random _random = new Random();

        public HomeController(RankContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Standings()
        {
            return View(await _context.Team.OrderByDescending(t => t.Elo).ThenBy(t => t.Wins).ThenBy(q => q.Name).ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Rank()
        {
            return View(await ResolveRankView());            
            
        }

        public async Task<IActionResult> Resolve(Guid id, Guid winner, Guid loser, int draw)
        {
            Match m =  await _context.Match.FindAsync(id);
            if (m._played)
            {
                return RedirectToAction("Rank");
                //return View("Rank", await ResolveRankView());
            }
            Team alpha =  await _context.Team.FindAsync(winner);
            Team beta =  await _context.Team.FindAsync(loser);
            double aELO = alpha.Elo;
            double bELO = beta.Elo;
            double eA = 1.0 / (1.0 + Math.Pow(10.0, ((bELO - aELO) / 400)));
            double eB = 1.0 / (1.0 + Math.Pow(10.0, ((aELO - bELO) / 400)));
            if (draw == 1)
            {
                alpha.Draws++;
                beta.Draws++;
                alpha.Elo += Convert.ToInt16((32.0 * (0.5 - eA)));
                beta.Elo += Convert.ToInt16((32.0 * (0.5 - eB)));
                m.draw = true;
            }
            else
            {
                alpha.Wins++;
                beta.Losses++;
                alpha.Elo += Convert.ToInt16((32.0 * (1.0 - eA)));
                beta.Elo += Convert.ToInt16((32.0 * (0.0 - eB)));
            }
            m._played = true;
            m._playedOn = DateTime.UtcNow;
            m.winner = alpha;
            m.loser = beta;
            _context.Entry(alpha).State = EntityState.Modified;
            _context.Entry(beta).State = EntityState.Modified;
            _context.Entry(m).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction("Rank");
            //return View("Rank", await ResolveRankView());
        }

        public async Task<IActionResult> Team(string Name)
        {
            Team team = await _context.Team.Where(t => t.Name == Name).FirstOrDefaultAsync();
            var matches = await _context.Match.Where(m => (m._played == true) && (m.winner != null) && (m.loser != null) && (m.Team1.Id == team.Id || m.Team2.Id == team.Id)).OrderBy(m => m._playedOn).Select(m => new MatchDTO
            {
                //ID = m.Id,
                Team1 = m.winner.Name,
                //ID1 = m.winner.Id,
                Team2 = m.loser.Name,
                //ID2 = m.loser.Id,
                Timestamp = m._playedOn,
                draw = m.draw
            }).ToListAsync();

            Dictionary<string, Dictionary<string, int>> d = new Dictionary<string, Dictionary<string, int>>();

            foreach (var mm in matches)
            {
                var opp = mm.Team1 == Name ? mm.Team2 : mm.Team1;
                if (!d.ContainsKey(opp))
                {
                    d.TryAdd(opp, new Dictionary<string, int>());
                    d[opp].TryAdd("wins", 0);
                    d[opp].TryAdd("losses", 0);
                    d[opp].TryAdd("draws", 0);
                }
                d[opp][mm.draw ? "draws" : opp == mm.Team1 ? "losses" : "wins"]++;
            }
            ViewData["Title"] = Name;
            return View(d);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<MatchDTO> ResolveRankView()
        {
            if (_random.Next() % 4 == 0)
            {
                var mtc = _context.Match.Where(n => n._played == false);
                if (mtc.Count() > 0)
                {
                    var match = mtc.OrderBy(n => _random.Next()).Take(1).Select(n => new MatchDTO
                        {
                            ID = n.Id,
                            Team1 = n.Team1.Name,
                            ID1 = n.Team1.Id,
                            Team2 = n.Team2.Name,
                            ID2 = n.Team2.Id
                        });
                    return await match.FirstAsync();
                }
                    
            }
            var teams = _context.Team.OrderBy(t => _random.Next()).Take(2).ToArray();
            var m = new Match(teams[0], teams[1]);
            _context.Match.Add(m);
            await _context.SaveChangesAsync();
            return new MatchDTO
            {
                ID = m.Id,
                Team1 = m.Team1.Name,
                ID1 = m.Team1.Id,
                Team2 = m.Team2.Name,
                ID2 = m.Team2.Id
            };
        }
    }
}

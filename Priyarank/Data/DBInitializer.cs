using Microsoft.EntityFrameworkCore;
using Priyarank.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Priyarank.Data
{
    public class DBInitializer
    {
        public static void Initialize(RankContext context)
        {
            context.Database.EnsureCreated();

            if (context.Team.Any())
            {
                return;
            }

            string[] teams = new string[] { "Amherst", "Auburn", "Chicago A", "Chicago B", "Chicago C", "Colgate", "Colorado", "Columbia A", "Cornell", "Delaware", "Florida A", "Harvard A", "Illinois A", "Kentucky", "Maryland A", "McGill", "Michigan", "Michigan State", "Minnesota A", "Minnesota B", "Missouri", "Northwestern A", "Ohio State", "Penn A", "Queen's", "Rutgers", "Stanford", "Texas", "Toronto A", "Toronto B", "UC Berkeley A", "UC Berkeley B", "UC San Diego", "Virginia", "WUSTL", "Yale A" };

            //var dataFile = Path.Combine()

            //string[] teams = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "ICTDI.txt"));

            //int i = 0;
            foreach (var team in teams)
            {
                var t = new Team { Name = team, Elo = 1200, Id = Guid.NewGuid() };
                context.Team.Add(t);
                //i++;
            }
            context.SaveChanges();
        }

    }
}

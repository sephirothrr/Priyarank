using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace Priyarank.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Elo { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        //public List<Player> Roster { get; set; }

        //public Team() { }

        //public Team(string name)
        //{
        //    this.Id = new Guid();
        //    this.Name = name;
        //    this.Elo = 1200;
        //}

        public static void AdjustElo(Team winner, Team loser)
        {
            int win, lose;
            (win, lose) = Models.Elo.NewScores(winner.Elo, loser.Elo);
            winner.Elo = win;
            loser.Elo = lose;
        }

    }
    
    
    
}
using System;
using System.ComponentModel.DataAnnotations;


namespace Priyarank.Models
{
    public class Match
    {   
        public Guid Id { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public Team winner { get; set; }
        public Team loser { get; set; }
        public bool _played { get; set; }
        public DateTime created { get; set; }
        public DateTime _playedOn { get; set; }

        public Match()
        {

        }

        public Match(Team t1, Team t2)
        {
            this.Id = new Guid();
            this.Team1 = t1;
            this.Team2 = t2;
            this._played = false;
        }

        public bool Resolve(Guid id, Team w, Team l)
        {
            if (this._played)
                return false;
            this.winner = w;
            this.loser = l;
            Team.AdjustElo(this.winner, this.loser);
            this._played = true;
            this._playedOn = DateTime.Now;
            return true;
        }
    }
}
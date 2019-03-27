using System;
using System.Collections.Generic;
using System.Linq;

namespace Priyarank.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Team> Field { get; set; }


        //public Tournament(string name)
        //{
        //    this.ID = new Guid();
        //    this.Name = name;
        //}

        public bool AddTeam(Team team)
        {
            try
            {
                this.Field.Append(team);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    
}
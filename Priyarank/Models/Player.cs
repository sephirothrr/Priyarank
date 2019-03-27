using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Priyarank.Models
{
    public class Player
    {
        public int Id { get; set; }
        public String Name { get; set; }


        //public Player(String name)
        //{
        //    Id = new Guid();
        //    this.Name = name;
        //}
    }
}

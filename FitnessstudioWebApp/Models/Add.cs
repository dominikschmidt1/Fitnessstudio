using FitnessstudioLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FitnessstudioWebApp.Models
{
    public class Add
    {
        [Key]
        [Column(Order = 0)]
        public int PersonId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int LeistungId { get; set; }
        public Person Person { get; set; }
        public Leistung Leistung { get; set; }
    }
}
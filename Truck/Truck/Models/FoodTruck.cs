using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Truck.Models
{
    public class FoodTruck
    {
        public int ID { get; set; }
        

        [Display(Name = "Truck Name")]
        public string TruckName { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Map { get; set; }
    }



    public class FoodTruckContext : DbContext
    {
        public DbSet<FoodTruck> Trucks { get; set; }
    }

}
﻿namespace CarSearchApp.Models
{
    public class UsedVehicle
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;  
        public string Model { get; set; } = string.Empty; 
        public int Year { get; set; }
    }
}
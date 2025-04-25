using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CarSearchApp.Models; 

namespace CarSearchApp.Controllers
{
    public class VehicleController : Controller
    {

        private readonly List<UsedVehicle> vehicles = new List<UsedVehicle>
        {
            new UsedVehicle { Id = 1, Make = "Toyota", Model = "Corolla", Year = 2019 },
            new UsedVehicle { Id = 2, Make = "Honda", Model = "Civic", Year = 2020 },
            new UsedVehicle { Id = 3, Make = "Ford", Model = "Mustang", Year = 2018 },
            new UsedVehicle { Id = 4, Make = "Toyota", Model = "Camry", Year = 2018 }
        };


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string? searchMake, string? searchModel, int? searchYear, string? useManualBinding)
        {

            bool manualBinding = !string.IsNullOrEmpty(useManualBinding);

            if (manualBinding)
            {

                searchMake = Request.Form["searchMake"].FirstOrDefault();
                searchModel = Request.Form["searchModel"].FirstOrDefault();

                string? yearString = Request.Form["searchYear"].FirstOrDefault();
                if (int.TryParse(yearString, out int parsedYear))
                {
                    searchYear = parsedYear;
                }
                else
                {
                    searchYear = null;
                }
            }


            var filteredVehicles = vehicles.Where(v =>
                (string.IsNullOrEmpty(searchMake) || v.Make.Contains(searchMake, System.StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(searchModel) || v.Model.Contains(searchModel, System.StringComparison.OrdinalIgnoreCase)) &&
                (!searchYear.HasValue || v.Year == searchYear.Value)
            ).ToList();

            return View("Results", filteredVehicles);
        }
    }
}
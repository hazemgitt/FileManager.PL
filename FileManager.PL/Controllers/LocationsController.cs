using FileManager.PL.Models;
using FileManager.PL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FileManager.PL.Controllers
{
   
    public class LocationsController : Controller
    {
        private readonly ILocationService _locationService;
        private readonly ICityService _cityService;

        public LocationsController(ILocationService locationService, ICityService cityService)
        {
            _locationService = locationService;
            _cityService = cityService;
        }

        // GET: Locations
        public async Task<IActionResult> Index(int? cityId)
        {
            if (cityId.HasValue)
            {
                var city = await _cityService.GetCityByIdAsync(cityId.Value);
                if (city == null)
                {
                    return NotFound();
                }

                ViewBag.City = city;
                var locations = await _locationService.GetLocationsByCityIdAsync(cityId.Value);
                return View(locations);
            }
            else
            {
                var locations = await _locationService.GetAllLocationsAsync();
                return View(locations);
            }
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Locations/Create
        public async Task<IActionResult> Create(int? cityId)
        {
            if (cityId.HasValue)
            {
                var city = await _cityService.GetCityByIdAsync(cityId.Value);
                if (city == null)
                {
                    return NotFound();
                }

                ViewBag.City = city;
                var location = new Location { CityId = cityId.Value };
                return View(location);
            }
            else
            {
                var cities = await _cityService.GetAllCitiesAsync();
                ViewBag.Cities = new SelectList(cities, "Id", "Name");
                return View();
            }
        }

        // POST: Locations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Address,CityId")] Location location)
        {
            if (ModelState.IsValid)
            {
                var createdLocation = await _locationService.CreateLocationAsync(location);

                if (createdLocation.CityId > 0)
                {
                    return RedirectToAction(nameof(Index), new { cityId = createdLocation.CityId });
                }

                return RedirectToAction(nameof(Index));
            }

            if (location.CityId > 0)
            {
                var city = await _cityService.GetCityByIdAsync(location.CityId);
                ViewBag.City = city;
            }
            else
            {
                var cities = await _cityService.GetAllCitiesAsync();
                ViewBag.Cities = new SelectList(cities, "Id", "Name", location.CityId);
            }

            return View(location);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            var cities = await _cityService.GetAllCitiesAsync();
            ViewBag.Cities = new SelectList(cities, "Id", "Name", location.CityId);

            return View(location);
        }

        // POST: Locations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Address,CityId")] Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _locationService.UpdateLocationAsync(location);
                return RedirectToAction(nameof(Index), new { cityId = location.CityId });
            }

            var cities = await _cityService.GetAllCitiesAsync();
            ViewBag.Cities = new SelectList(cities, "Id", "Name", location.CityId);

            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            int cityId = location.CityId;
            await _locationService.DeleteLocationAsync(id);

            return RedirectToAction(nameof(Index), new { cityId });
        }
    }
}

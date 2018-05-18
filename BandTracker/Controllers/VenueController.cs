using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using BandTrackerApp;

namespace BandTrackerApp.Controllers
{
    public class VenueController : Controller
    {
        [HttpGet("/venues/list")]
        public ActionResult VenuesList()
        {
            List<Venue> allVenues = Venue.GetAll();
            return View(allVenues);
        }

        [HttpGet("/venue/{id}/details")]
        public ActionResult VenueDetails(int id)
        {
            Venue newVenue = Venue.Find(id);
            List<Band> bands = Band.GetAll();
            Dictionary<string,object> model = new Dictionary<string,object> {};
            model.Add("venue", newVenue);
            model.Add("bands", bands);
            return View(model);
        }

        [HttpPost("/venue/{id}/newband")]
        public ActionResult AddBandToVenue(int id)
        {
            Venue newVenue = Venue.Find(id);
            Band newBand = Band.Find(int.Parse(Request.Form["band"]));
            newVenue.AddBand(newBand);
            List<Band> bands = Band.GetAll();
            Dictionary<string,object> model = new Dictionary<string,object> {};
            model.Add("venue", newVenue);
            model.Add("bands", bands);
            return View("VenueDetails", model);
        }

        [HttpPost("/venue/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Venue newVenue = Venue.Find(id);
            newVenue.Delete();
            List<Venue> allVenues = Venue.GetAll();
            return View("VenuesList", allVenues);
        }

        [HttpGet("/new/venue")]
        public ActionResult VenueForm()
        {
            return View();
        }

        [HttpPost("/new/venue")]
        public ActionResult AddVenue()
        {
            Venue newVenue = new Venue(Request.Form["name"], Request.Form["eventDate"],Request.Form["address"],Request.Form["contact"]);
            newVenue.Save();
            List<Venue> allVenues = Venue.GetAll();
            return View("VenuesList", allVenues);
        }
    }
}

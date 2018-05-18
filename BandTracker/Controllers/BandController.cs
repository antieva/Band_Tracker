using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using BandTrackerApp;

namespace BandTrackerApp.Controllers
{
    public class BandController : Controller
    {
        [HttpGet("/bands/list")]
        public ActionResult BandsList()
        {
            List<Band> allBands = Band.GetAll();
            return View(allBands);
        }

        [HttpGet("/band/{id}/details")]
        public ActionResult BandDetails(int id)
        {
            Band newBand = Band.Find(id);
            List<Venue> venues = Venue.GetAll();
            Dictionary<string,object> model = new Dictionary<string,object> {};
            model.Add("band", newBand);
            model.Add("venues", venues);
            return View(model);
        }

        [HttpPost("/band/{id}/newVenue")]
        public ActionResult AddVenueToBand(int id)
        {
            Band newBand = Band.Find(id);
            Console.WriteLine(Request.Form["venue"]);
            Venue newVenue = Venue.Find(int.Parse(Request.Form["venue"]));
            newBand.AddVenue(newVenue);
            List<Venue> venues = Venue.GetAll();
            Dictionary<string,object> model = new Dictionary<string,object> {};
            model.Add("band", newBand);
            model.Add("venues", venues);
            return View("BandDetails", model);
        }

        [HttpPost("/band/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Band newBand = Band.Find(id);
            newBand.Delete();
            List<Band> allBands = Band.GetAll();
            return View("BandsList", allBands);
        }

        [HttpGet("/new/band")]
        public ActionResult BandForm()
        {
            return View();
        }

        [HttpPost("/new/band")]
        public ActionResult AddBand()
        {
            Band newBand = new Band(Request.Form["name"], Request.Form["genre"],Request.Form["leader"],Request.Form["members"],Request.Form["originPlace"], Request.Form["originYear"], Request.Form["agent"], Request.Form["agentContact"]);
            newBand.Save();
            List<Band> allBands = Band.GetAll();
            return View("BandsList", allBands);
        }
    }
}

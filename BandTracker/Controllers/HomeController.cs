using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using BandTrackerApp;

namespace BandTrackerApp.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/home")]
    public ActionResult Index()
    {
      return View(null);
    }

    [HttpPost("/search/bands/name")]
    public ActionResult SearchBandsByName()
    {
      List<Band> searchedBands = Band.FindByName(Request.Form["name"]);
      List<Band> emptyList1 = new List<Band>{};
      List<Venue> emptyList2 = new List<Venue> {};
      Dictionary<string,object> model = new Dictionary<string,object>{};
      model.Add("searchedBandsByName", searchedBands);
      model.Add("searchedBandsByGenre", emptyList1);
      model.Add("searchedVenuesByDate", emptyList2);
      return View("Index", model);
    }

    [HttpPost("/search/bands/genre")]
    public ActionResult SearchBandsByGenre()
    {
      List<Band> searchedBands = Band.FindByGenre(Request.Form["genre"]);
      List<Band> emptyList1 = new List<Band>{};
      List<Venue> emptyList2 = new List<Venue> {};
      Dictionary<string,object> model = new Dictionary<string,object>{};
      model.Add("searchedBandsByName", emptyList1);
      model.Add("searchedBandsByGenre", searchedBands);
      model.Add("searchedVenuesByDate", emptyList2);
      return View("Index", model);
    }

    [HttpPost("/search/venues/date")]
    public ActionResult SearchVenuessByDate()
    {
      List<Venue> searchedVenues = Venue.FindByDate(Request.Form["date"]);
      List<Band> emptyList = new List<Band>{};
      Dictionary<string,object> model = new Dictionary<string,object>{};
      model.Add("searchedBandsByName", emptyList);
      model.Add("searchedBandsByGenre", emptyList);
      model.Add("searchedVenuesByDate", searchedVenues);
      return View("Index", model);
    }
  }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using BandTrackerApp;
using System;

namespace BandTrackerApp.Tests
{

    [TestClass]
    public class VenueTest : IDisposable
     {
          public VenueTest()
          {
              DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
          }
          public void Dispose()
          {
              Band.DeleteAll();
              Venue.DeleteAll();
          }

          [TestMethod]
          public void GetAll_DatabaseEmptyAtFirst_0()
          {
              //Arrange, Act
              int result = Venue.GetAll().Count;

              //Assert
              Assert.AreEqual(0, result);
          }

          [TestMethod]
          public void Equals_TrueForSameFields_Venue()
          {
              //Arrange, Act
              Venue firstVenue = new Venue("Tacoma Dome", "08/23/18", "Tacoma, WA",  "206-880-0000");
              Venue secondVenue = new Venue("Tacoma Dome", "08/23/18", "Tacoma, WA",  "206-880-0000");

              //Assert
              Assert.AreEqual(firstVenue, secondVenue);
          }

          [TestMethod]
          public void Save_VenueSavesToDatabase_VenueList()
          {
              //Arrange
              Venue testVenue = new Venue("Tacoma Dome", "08/23/18", "Tacoma, WA",  "206-880-0000");
              testVenue.Save();

              //Act
              List<Venue> result = Venue.GetAll();
              Console.WriteLine(result.Count);
              List<Venue> testList = new List<Venue>{testVenue};

              //Assert
              CollectionAssert.AreEqual(testList, result);
          }

          [TestMethod]
          public void Save_AssignsIdToObject_id()
          {
              //Arrange
              Venue testVenue = new Venue("Tacoma Dome", "08/23/18", "Tacoma, WA",  "206-880-0000");
              testVenue.Save();

              //Act
              Venue savedVenue = Venue.GetAll()[0];

              int result = savedVenue.GetId();
              int testId = testVenue.GetId();

              //Assert
              Assert.AreEqual(testId, result);
          }

          [TestMethod]
          public void AddBand_AddsBandToVenue_BandsList()
          {
              //Arrange
              Band testBand = new Band("Black Cats", "blues", "Jack Goover", "Jill, John, Kate, Delan", "Tacoma, WA", "2015", "Karla Smith", "206-880-0000");
              testBand.Save();

              Venue testVenue = new Venue("Tacoma Dome", "08/23/18", "Tacoma, WA",  "206-880-0000");
              testVenue.Save();

              //Act
              testVenue.AddBand(testBand);

              List<Band> result = testVenue.GetBands();
              List<Band> testList = new List<Band>{testBand};

              //Assert
              CollectionAssert.AreEqual(testList, result);
          }

          [TestMethod]
          public void Delete_DeletesVenueAssociationsFromDatabase_VanuesList()
          {
              //Arrange
              Venue testVenue = new Venue("Tacoma Dome", "08/23/18", "Tacoma, WA",  "206-880-0000");
              testVenue.Save();

              Band testBand = new Band("Black Cats", "blues", "Jack Goover", "Jill, John, Kate, Delan", "Tacoma, WA", "2015", "Karla Smith", "206-880-0000");
              testBand.Save();

              //Act
              testBand.AddVenue(testVenue);
              testVenue.Delete();

              List<Venue> result = testBand.GetVenues();
              List<Venue> test = new List<Venue> {};

              //Assert
              CollectionAssert.AreEqual(test, result);
          }

          [TestMethod]
          public void Edit_EditsVenueAssociationsInDatabase_Void()
          {
              //Arrange
              Venue newVenue = new Venue("Tacoma Dome", "08/23/18", "Tacoma, WA",  "206-880-0000");
              newVenue.Save();

              Venue testVenue = new Venue("Georgetown Ballroom", "07/07/19", "5623 Airport Way S, Seattle, WA 98108",  "(206) 763-4999", newVenue.GetId());

              //Act
              newVenue.Edit("Georgetown Ballroom", "07/07/19", "5623 Airport Way S, Seattle, WA 98108",  "(206) 763-4999");

              //Assert
              Assert.AreEqual(testVenue, newVenue);
          }

          [TestMethod]
          public void DeleteBandFromVenue_DeleteBandFromJoinTable_Void()
          {
            Venue newVenue = new Venue("Tacoma Dome", "08/23/18", "Tacoma, WA",  "206-880-0000");
            newVenue.Save();

            Band firstBand = new Band("Black Cats", "blues", "Jack Goover", "Jill, John, Kate, Delan", "Tacoma, WA", "2015", "Karla Smith", "206-880-0000");
            firstBand.Save();

            Band secondBand = new Band("Kittens", "Grunge", "Tim Pitt", "John, Kate, Jane", "NY", "2010", "Cris Tilman", "306-880-0000");
            secondBand.Save();

            newVenue.AddBand(firstBand);
            newVenue.AddBand(secondBand);
            newVenue.DeleteBandFromVenue(firstBand.GetId());

            List<Band> result = newVenue.GetBands();
            List<Band> testList = new List<Band>{secondBand};

            //Assert
            CollectionAssert.AreEqual(testList, result);
          }
      }
}

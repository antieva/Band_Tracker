using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using BandTrackerApp;
using System;

namespace BandTrackerApp.Tests
{

    [TestClass]
    public class BandTest : IDisposable
     {
          public BandTest()
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
              int result = Band.GetAll().Count;

              //Assert
              Assert.AreEqual(0, result);
          }

          [TestMethod]
          public void Equals_TrueForSameFields_Band()
          {
              //Arrange, Act
              Band firstBand = new Band("Black Cats", "blues", "Jack Goover", "Jill, John, Kate, Delan", "Tacoma, WA", "2015", "Karla Smith", "206-880-0000");
              Band secondBand = new Band("Black Cats", "blues", "Jack Goover", "Jill, John, Kate, Delan", "Tacoma, WA", "2015", "Karla Smith", "206-880-0000");

              //Assert
              Assert.AreEqual(firstBand, secondBand);
          }

          [TestMethod]
          public void Save_BandSavesToDatabase_BandList()
          {
              //Arrange
              Band testBand = new Band("Black Cats", "blues", "Jack Goover", "Jill, John, Kate, Delan", "Tacoma, WA", "2015", "Karla Smith", "206-880-0000");
              testBand.Save();

              //Act
              List<Band> result = Band.GetAll();
              List<Band> testList = new List<Band>{testBand};

              //Assert
              CollectionAssert.AreEqual(testList, result);
          }

          [TestMethod]
          public void Save_AssignsIdToObject_id()
          {
              //Arrange
              Band testBand = new Band("Black Cats", "blues", "Jack Goover", "Jill, John, Kate, Delan", "Tacoma, WA", "2015", "Karla Smith", "206-880-0000");
              testBand.Save();

              //Act
              Band savedBand = Band.GetAll()[0];

              int result = savedBand.GetId();
              int testId = testBand.GetId();

              //Assert
              Assert.AreEqual(testId, result);
          }

          [TestMethod]
          public void AddVanue_AddsVenueToBand_VenuesList()
          {
              //Arrange
              Band testBand = new Band("Black Cats", "blues", "Jack Goover", "Jill, John, Kate, Delan", "Tacoma, WA", "2015", "Karla Smith", "206-880-0000");
              testBand.Save();

              Venue testVenue = new Venue("Tacoma Dome", "08/23/18", "Tacoma, WA",  "206-880-0000");
              testVenue.Save();

              //Act
              testBand.AddVenue(testVenue);

              List<Venue> result = testBand.GetVenues();
              List<Venue> testList = new List<Venue>{testVenue};

              //Assert
              CollectionAssert.AreEqual(testList, result);
          }

          [TestMethod]
          public void Delete_DeletesBandAssociationsFromDatabase_BandList()
          {
              //Arrange
              Venue testVenue = new Venue("Tacoma Dome", "08/23/18", "Tacoma, WA",  "206-880-0000");
              testVenue.Save();

              Band testBand = new Band("Black Cats", "blues", "Jack Goover", "Jill, John, Kate, Delan", "Tacoma, WA", "2015", "Karla Smith", "206-880-0000");
              testBand.Save();

              //Act
              testBand.AddVenue(testVenue);
              testBand.Delete();

              List<Band> result = testVenue.GetBands();
              List<Band> test = new List<Band> {};

              //Assert
              CollectionAssert.AreEqual(test, result);
          }

          [TestMethod]
          public void Edit_EditsBandAssociationsInDatabase_Void()
          {
              //Arrange
              Band newBand = new Band("Black Cats", "blues", "Jack Goover", "Jill, John, Kate, Delan", "Tacoma, WA", "2015", "Karla Smith", "206-880-0000");
              newBand.Save();

              Band testBand = new Band("Kittens", "Grunge", "Tim Pitt", "John, Kate, Jane", "NY", "2010", "Cris Tilman", "306-880-0000", newBand.GetId());

              //Act
              newBand.Edit("Kittens", "Grunge", "Tim Pitt", "John, Kate, Jane", "NY", "2010", "Cris Tilman", "306-880-0000");

              //Assert
              Assert.AreEqual(testBand, newBand);
          }

          [TestMethod]
          public void DeleteVenueFromBand_DeleteVenueFromJoinTable_Void()
          {
              Band newBand = new Band("Black Cats", "blues", "Jack Goover", "Jill, John, Kate, Delan", "Tacoma, WA", "2015", "Karla Smith", "206-880-0000");
              newBand.Save();

              Venue firstVenue = new Venue("Tacoma Dome", "08/23/18", "Tacoma, WA",  "206-880-0000");
              firstVenue.Save();
              Venue secondVenue = new Venue("Georgetown Ballroom", "07/07/19", "5623 Airport Way S, Seattle, WA 98108",  "(206) 763-4999");
              secondVenue.Save();
              newBand.AddVenue(firstVenue);
              newBand.AddVenue(secondVenue);
              newBand.DeleteVenueFromBand(firstVenue.GetId());

              List<Venue> result = newBand.GetVenues();
              List<Venue> testList = new List<Venue>{secondVenue};

              //Assert
              CollectionAssert.AreEqual(testList, result);
          }

          [TestMethod]
          public void Find_BandID_Band()
          {
              Band testBand = new Band("Black Cats", "blues", "Jack Goover", "Jill, John, Kate, Delan", "Tacoma, WA", "2015", "Karla Smith", "206-880-0000");
              testBand.Save();

              Band result = Band.Find(testBand.GetId());
              Assert.AreEqual(testBand, result);
          }

          [TestMethod]
          public void FindByName_BandName_Band()
          {
              Band firstBand = new Band("Black Cats", "blues", "Kloud McCris", "Jill, John, Kate, Delan", "Tampa, Florida", "1990", "Jane Smith", "206-700-0000");
              firstBand.Save();

              Band secondBand = new Band("Black JACK", "ROCK", "Jack Goover", "Jill, John, Kate, Delan", "Tacoma, WA", "2015", "Karla Smith", "206-880-0000");
              secondBand.Save();

              List<Band> result = Band.FindByName("Black");
              List<Band> test = new List<Band>{firstBand, secondBand};
              CollectionAssert.AreEqual(test, result);
          }

      }
}

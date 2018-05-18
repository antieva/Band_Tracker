using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BandTrackerApp.Controllers;
using BandTrackerApp;

namespace BandTrackerApp.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
      [TestMethod]
     public void Index_ReturnsCorrectView_True()
     {
         //Arrange
         HomeController controller = new HomeController();

         //Act
         ActionResult indexView = controller.Index();

         //Assert
         Assert.IsInstanceOfType(indexView, typeof(ViewResult));
     }

     // [TestMethod]
     //    public void Index_HasCorrectModelType_null()
     //    {
     //        //Arrange
     //        ViewResult indexView = new HomeController().Index() as ViewResult;
     //
     //        //Act
     //        var result = indexView.ViewData.Model;
     //
     //        //Assert
     //        //Assert.IsTrue(result.GetType() == typeof(List<Category>));
     //        Assert.IsInstanceOfType(result, typeof());
     //    }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Controllers;
using System.Web.Mvc;
using Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Web.UI.Tests.Controllers.VenueControllerTests
{
    [TestClass]
    public class DetailsTests
    {
        private VenueController _controller;
        private ViewResult _result;

        [TestInitialize]
        public void Setup()
        {
            _controller = new VenueController
            {
                Db = new FakeColumbusGiveCamp2011Context
                {
                    Venues = new FakeVenueSet
                                 {
                                     new VenueModel()
                                 }
                }
            };
        }

        [TestMethod]
        public void bad_details_index_should_return_proper_error_page()
        {
            //Arrange
            var index = -1;

            //Act
            _result = _controller.Details(index);

            //Assert
            Assert.AreEqual("NoVenue", _result.ViewName);
        }
    }
}

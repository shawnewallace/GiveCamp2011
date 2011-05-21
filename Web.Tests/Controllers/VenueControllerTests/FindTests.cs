using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Web.Controllers;
using Web.Models;
using Should;
using System.Web.Script.Serialization;

namespace Web.UI.Tests.Controllers.VenueControllerTests
{
    [TestClass]
    public class FindTests
    {
        private VenueController _controller;
        private JsonResult _result;

        [TestInitialize]
        public void Initialize()
        {
            _controller = new VenueController
            {
                Db = new FakeColumbusGiveCamp2011Context
                {
                    Venues = new FakeVenueSet
                    {
                        new VenueModel
                        { 
                            Id=1,
                            Name="Test Venue 1",
                            Address="123 Test Way",
                            City="Somewhere",
                            State="Ohio",
                            Zip="44444-5555",
                            PhoneNumber="1115551234"
                        },
                        new VenueModel
                        {
                            Id=2,
                            Name="Another Venue",
                            Address="456 Another Street",
                            City="Westerville",
                            State="Ohio",
                            Zip="55555-4444",
                            PhoneNumber="2225554321"
                        }
                    }
                }
            };
        }

        [TestMethod]
        public void find_on_valid_name_returns_one_correct_venue()
        {
            //Arrange
            string searchString = "Test";
            string _expected = @"{""Venues"":[{""Id"":1,""Name"":""Test Venue 1"",""Address"":""123 Test Way"",""City"":""Somewhere"",""State"":""Ohio"",""Zip"":""44444-5555"",""PhoneNumber"":""1115551234""}],""Count"":1}";

            //Act
            _result = _controller.Find(searchString) as JsonResult;
            var serializer = new JavaScriptSerializer();
            var output = serializer.Serialize(_result.Data);

            //Assert
            output.ShouldEqual(_expected);
        }

        [TestMethod]
        public void find_on_valid_name_returns_two_correct_venues()
        {
            //Arrange
            string searchString = "Venue";
            string venue1 = @"{""Id"":1,""Name"":""Test Venue 1"",""Address"":""123 Test Way"",""City"":""Somewhere"",""State"":""Ohio"",""Zip"":""44444-5555"",""PhoneNumber"":""1115551234""}";
            string venue2 = @"{""Id"":2,""Name"":""Another Venue"",""Address"":""456 Another Street"",""City"":""Westerville"",""State"":""Ohio"",""Zip"":""55555-4444"",""PhoneNumber"":""2225554321""}";
            string _expected = string.Format("{{\"Venues\":[{0},{1}],\"Count\":2}}",venue1,venue2);

            //Act
            _result = _controller.Find(searchString) as JsonResult;
            var serializer = new JavaScriptSerializer();
            var output = serializer.Serialize(_result.Data);

            //Assert
            output.ShouldEqual(_expected);
        }

        [TestMethod]
        public void find_on_valid_street_returns_one_correct_venue()
        {
            //Arrange
            string searchString = "Street";
            string venue2 = @"{""Id"":2,""Name"":""Another Venue"",""Address"":""456 Another Street"",""City"":""Westerville"",""State"":""Ohio"",""Zip"":""55555-4444"",""PhoneNumber"":""2225554321""}";
            string _expected = string.Format("{{\"Venues\":[{0}],\"Count\":1}}", venue2);

            //Act
            _result = _controller.Find(searchString) as JsonResult;
            var serializer = new JavaScriptSerializer();
            var output = serializer.Serialize(_result.Data);

            //Assert
            output.ShouldEqual(_expected);
        }

        [TestMethod]
        public void find_on_empty_string_returns_all_venues()
        {
            //Arrange
            string venue1 = @"{""Id"":1,""Name"":""Test Venue 1"",""Address"":""123 Test Way"",""City"":""Somewhere"",""State"":""Ohio"",""Zip"":""44444-5555"",""PhoneNumber"":""1115551234""}";
            string venue2 = @"{""Id"":2,""Name"":""Another Venue"",""Address"":""456 Another Street"",""City"":""Westerville"",""State"":""Ohio"",""Zip"":""55555-4444"",""PhoneNumber"":""2225554321""}";
            string _expected = string.Format("{{\"Venues\":[{0},{1}],\"Count\":2}}", venue1, venue2);

            //Act
            _result = _controller.Find() as JsonResult;
            var serializer = new JavaScriptSerializer();
            var output = serializer.Serialize(_result.Data);

            //Assert
            output.ShouldEqual(_expected);
        }

        [TestMethod]
        public void find_on_valid_name_and_street_returns_all_correct_venues()
        {
            //Arrange
            string searchString = "Test Street";
            string venue1 = @"{""Id"":1,""Name"":""Test Venue 1"",""Address"":""123 Test Way"",""City"":""Somewhere"",""State"":""Ohio"",""Zip"":""44444-5555"",""PhoneNumber"":""1115551234""}";
            string venue2 = @"{""Id"":2,""Name"":""Another Venue"",""Address"":""456 Another Street"",""City"":""Westerville"",""State"":""Ohio"",""Zip"":""55555-4444"",""PhoneNumber"":""2225554321""}";
            string _expected = string.Format("{{\"Venues\":[{0},{1}],\"Count\":2}}", venue1, venue2);

            //Act
            _result = _controller.Find(searchString) as JsonResult;
            var serializer = new JavaScriptSerializer();
            var output = serializer.Serialize(_result.Data);

            //Assert
            output.ShouldEqual(_expected);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Controllers;
using System.Web.Mvc;
using Web.Models;

namespace Web.UI.Tests.Controllers.VenueControllerTests
{
    [TestFixture]
    public class IndexTests
    {
        private VenueController _controller;
        private ViewResult result;

        [SetUp]
        public void Setup()
        {
            _controller = new VenueController();
            result = _controller.Index();
        }

        [Test]
        public void Smoke_Test()
        {
        }

        [Test]
        public void renders_index_view()
        {
            Assert.AreEqual("Index",result.ViewName);
        }

        [Test]
        public void index_view_should_have_list_of_venues()
        {
            var venueList = result.ViewBag.VenueList;
            var x = result.Model;

            Assert.That(x.GetType() == typeof(List<VenueModel>));
        }
    }
}

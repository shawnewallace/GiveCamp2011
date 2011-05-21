using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Controllers;

namespace Web.UI.Tests.Controllers.VenueControllerTests
{
    [TestFixture]
    public class IndexTests
    {
        private VenueController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new VenueController();
        }

        [Test]
        public void Smoke_Test()
        {
            var result = _controller.Index();
        }

        [Test]
        public void renders_index_view()
        {
            var result = _controller.Index();

            Assert.That(result.ViewName == "Index");
        }
    }
}

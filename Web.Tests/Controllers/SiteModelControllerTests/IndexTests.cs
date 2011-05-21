using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Controllers;
using System.Web.Mvc;
using Web.Models;
using Web.UI.Tests.Controllers.VenueControllerTests;

namespace Web.UI.Tests.Controllers.SiteModelControllerTests
{
    [TestFixture]
    public class IndexTests
    {
        private SiteModelsController _controller;
        private ViewResult _result;

        [SetUp]
        public void Setup()
        {
            _controller = new SiteModelsController
            {
                Db = new FakeColumbusGiveCamp2011Context
                         {
                             SiteModels = new FakeSiteModelSet
                                 {
                                     new SiteModel()
                                 }
                         }
            };
           _result = _controller.Index();
        }

        [Test]
        public void Smoke_Test()
        {
        }

        [Test]
        public void renders_index_view()
        {
            Assert.AreEqual("Index", _result.ViewName);
        }

        [Test]
        public void index_view_should_have_list_of_venues()
        {
            var x = _result.Model;

            Assert.That(x.GetType() == typeof(List<SiteModel>));
        }
    }
}

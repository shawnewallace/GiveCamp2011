using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Controllers;
using System.Web.Mvc;
using Web.Models;
using Web.UI.Tests.Controllers.VenueControllerTests;
using NUnit.Framework;

namespace Web.UI.Tests.Controllers.SiteModelControllerTests
{
    [TestFixture]
    public class DetailsTests
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
            _result = _controller.Edit(_controller.Db.SiteModels.First().Id);
        }

        [Test]
        public void detail_view_should_have_siteModel_model()
        {
            var x = _result.Model;

            Assert.That(x.GetType() == typeof(SiteModel));
        }

    }
}

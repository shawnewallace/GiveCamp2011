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
    public class EditTests
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
            _result = _controller.Details(_controller.Db.SiteModels.First().Id);
        }

        [Test]
        public void edit_view_should_have_a_siteModel()
        {
            var x = _result.Model;

            Assert.That(x.GetType() == typeof(SiteModel));
        }


        //TODO Come back when editing fake context works
        //[Test]
        //public void Edit_PostBackShouldEdit()
        //{
        //    var x = _result.Model;
        //    Assert.That(x.GetType() == typeof(SiteModel));
        //    ((SiteModel)x).PhoneNumber = "555-555-5555";
        //    _controller.Edit((SiteModel)x);
        //    Assert.That(((SiteModel)x).PhoneNumber == _controller.Db.SiteModels.First().PhoneNumber);        
        //}
    }
}

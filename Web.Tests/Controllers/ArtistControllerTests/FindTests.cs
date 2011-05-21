using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using Web.Controllers;
using Web.Models;
using Web.UI.Tests.Controllers.VenueControllerTests;

namespace Web.UI.Tests.Controllers.ArtistControllerTests
{
    [TestFixture]
    public class FindTests
    {
        private ArtistController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new ArtistController
            {
                Db = new FakeColumbusGiveCamp2011Context
                {
                    Artists = new FakeArtistSet
                                 {
                                     new ArtistModel
                                         {
                                             FirstName = new TextEntry{ Data = "FirstName", Enabled = true },
                                             LastName = new TextEntry{ Data = "LastName", Enabled = true }
                                         },
                                     new ArtistModel
                                         {
                                             FirstName = new TextEntry{ Data = "NewFirstName", Enabled = true },
                                             LastName = new TextEntry{ Data = "NewLastName", Enabled = true }
                                         }
                                 }
                }
            };
        }

        [Test]
        public void smoke_test()
        {
            var result = _controller.Find(string.Empty);
        }

        [Test]
        public void is_not_empty()
        {
            var result = _controller.Find("FirstName") as JsonResult;
        }
    }
}

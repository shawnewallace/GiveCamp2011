﻿using Web.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Drawing;

namespace Web.UI.Tests
{
    
    
    /// <summary>
    ///This is a test class for ImageServiceTest and is intended
    ///to contain all ImageServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ImageServiceTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for StoreCoverFlowImage
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        //[TestMethod()]
        //public void AwesomeTest()
        //{
        //    string file = "../../../Web.Tests/beerdog.jpg"; // TODO: Initialize to an appropriate value
        //    Image result = ImageService.GetResizedImage(file);
        //    Assert.IsTrue(result.Width == ImageService.PREFERRED_COVERFLOW_WIDTH || 
        //        result.Height == ImageService.MAX_COVERFLOW_HEIGHT);
        //}
    }
}

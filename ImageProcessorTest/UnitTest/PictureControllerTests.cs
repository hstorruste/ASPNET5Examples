using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ImageProcessorTest.Controllers;
using UnitTest.Stubs;
using BLL;
using DAL;
using Model;
using ImageProcessorTest;
using System.Collections;
using Microsoft.Extensions.PlatformAbstractions;
using System.Net.Http;
using Microsoft.AspNet.TestHost;

namespace UnitTest
{
    public class PictureControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public PictureControllerTest()
        {
            // Arrange
            _server = new TestServer(TestServer.CreateBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44371/api/Picture/");
        }
        [Fact]
        public void Get_Pictures_Ok()
        {
            //Arrange
            var expected = new List<Picture>()
            {
                new Picture
                {
                    id =1,
                    url= "www.1.no",
                    description = "Dette er en test!"
                },
                new Picture
                {
                    id =2,
                    url= "www.2.no",
                    description = "Dette er en test!"
                }
            };

            //Act
            var result = _client.GetAsync("");

            //Assert
            //Assert.Equal(expected: expected, actual: result, comparer: new PictureListComparer());
            Assert.Contains("1", result.Status.ToString());

        }
    }

    //public class PictureListComparer : IEqualityComparer
    //{
    //    //public new bool Equals(object x, object y)
    //    //{
    //    //    var list1 = (Picture)x;
    //    //    var list2 = (Picture)y;

    //    //    //if (list1.Count != list2.Count)
    //    //    //    return false;

    //    //    //for(int i=0; i < list1.Count; ++i)
    //    //    //{
    //    //    if (list1.id != list2.id
    //    //        || list1.url != list2.url
    //    //        || list1.description != list2.description)
    //    //        return false;
    //    //    //}

    //    //    return true;
    //    //}

    //    public int GetHashCode(object obj)
    //    {
    //        return obj.GetHashCode();
    //    }
    //}
}

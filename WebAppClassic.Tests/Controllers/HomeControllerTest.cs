using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAppClassic.Controllers;
using WebAppClassic.Models;

namespace WebAppClassic.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
		private readonly HttpClient _httpClient = new HttpClient();

		[TestMethod]
        public void Index()
        {
            // 排列
            HomeController controller = new HomeController();

            // 作用
            ViewResult result = controller.Index() as ViewResult;

            // 判斷提示
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
		}

		[TestMethod]
		public async Task Index1Async()
		{
			try
			{
				var test = new TestList { List = new List<TestObject>() };
				test.List.Add(new TestObject { Test = 1 });

				HttpResponseMessage response = await _httpClient.PostAsync(
					"https://postman-echo.com/post",
					new StringContent(
						JsonConvert.SerializeObject(test.List, Formatting.Indented),
						Encoding.UTF8));
				response.EnsureSuccessStatusCode();
				string responseBody = await response.Content.ReadAsStringAsync();
				Console.WriteLine(responseBody); // 得到 "data":"[\r\n  {\r\n    \"Test\": 1\r\n  }\r\n]"
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		[TestMethod]
        public async Task Index2Async()
        {
			try
			{
				var test = new TestList { List = new List<TestObject>() };
				test.List.Add(new TestObject { Test = 1 });

				HttpResponseMessage response = await _httpClient.PostAsync(
					"https://postman-echo.com/post",
					new StringContent(
						JsonConvert.SerializeObject(test.List, Formatting.Indented),
						Encoding.UTF8,
						"application/json"));
				response.EnsureSuccessStatusCode();
				string responseBody = await response.Content.ReadAsStringAsync();
				Console.WriteLine(responseBody); // 得到 "data":[{"Test":1}]
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
    }
}

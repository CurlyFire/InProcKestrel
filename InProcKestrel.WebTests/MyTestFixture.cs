using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace InProcKestrel.WebTests
{
  [TestFixture]
  public class MyTestFixture
  {
    private IWebHost _host;
    private string _baseAddress;

    [SetUp]
    public void SetUp ()
    {
      _host = new WebHostBuilder()
          .UseKestrel()
          .UseContentRoot (@"C:\Users\myuser\Documents\visual studio 2017\Projects\InProcKestrel\InProcKestrel\")
          .UseStartup<Startup>()
          .Build();

      _host.Start();
      _baseAddress = _host.ServerFeatures.Get<IServerAddressesFeature>().Addresses.Single();
    }

    [Test]
    public void MyTest ()
    {
      using (var webDriver = new ChromeDriver())
      {
        webDriver.Url = _baseAddress;
        var body = webDriver.FindElementByTagName ("body").Text;
        Assert.That (body, Is.Not.Empty);
      }
    }
  }
}
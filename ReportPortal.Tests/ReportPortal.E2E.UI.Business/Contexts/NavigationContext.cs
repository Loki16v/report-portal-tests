﻿using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using ReportPortal.E2E.Core;
using ReportPortal.E2E.Core.Driver;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.Helpers;
using ReportPortal.E2E.Core.Models;
using ReportPortal.E2E.UI.Business.Pages;

namespace ReportPortal.E2E.UI.Business.Contexts
{
    public class NavigationContext
    {
        private static readonly string BaseUrl =
            TestsBootstrap.Instance.ServiceProvider.GetRequiredService<ReportPortalConfig>().BaseUrl;

        private static readonly IWebDriver Driver = DriverFactory.GetDriver();

        public void GoToReportPortalBaseUrl()
        {
            Driver.Navigate().GoToUrl(BaseUrl);
        }

        public void GoTo<T>(params string[] parameters) where T : BasePage, new()
        {
            var urlTemplate = GetUrlTemplate<T>();
            Driver.Navigate().GoToUrl($"{BaseUrl}{urlTemplate.BindByPosition(parameters)}");
        }


        private string GetUrlTemplate<T>() where T : BasePage, new()
        {
            return ((PageUrlAttribute)Attribute.GetCustomAttribute(new T().GetType(), typeof(PageUrlAttribute)))?.Url;
        }
    }
}

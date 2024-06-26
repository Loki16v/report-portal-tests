﻿using Microsoft.Extensions.Logging;
using ReportPortal.E2E.API.Business.Models;

namespace ReportPortal.E2E.API.Business.StepDefinitions.ApiControllers
{
    public partial class ReportPortalApiSteps
    {
        public void CreateDemoData(string projectName)
        {
            var endpoint = string.Format(Endpoints.GenerateProjectData, projectName);
            Log.LogInformation($"CreateDemoData for project '{projectName}'\n Method: Post\n Endpoint: {endpoint}");
            _launchApiSteps.Post(endpoint, new { createDashboard = true });
        }
    }
}

﻿using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace ReportPortal.E2E.API.Framework.StepDefinitions
{
    public partial class ReportPortalApiSteps
    {
        public Task<HttpResponseMessage> CreateDemoData(string projectName)
        {
            var endpoint = string.Format(Endpoints.GenerateProjectData, projectName);
            Log.LogInformation($"CreateDemoData for project '{projectName}'\n Method: Post\n Endpoint: {endpoint}");
            return _launchApiSteps.PostAsJsonAsync(endpoint, new { createDashboard = true });
        }
    }
}

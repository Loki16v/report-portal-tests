﻿namespace ReportPortal.E2E.API.Business.Models
{
    public static class Endpoints
    {
        public const string ManageProject = "/api/v1/project";
        public const string GetProjectList = "/api/v1/project/list";
        public const string SearchProjectUser = "/api/v1/project/{0}/usernames?filter.cnt.users={1}";
        public const string AddUserToProject = "/api/v1/project/{0}/assign";

        public const string GenerateProjectData = "/api/v1/demo/{0}/generate";
        public const string GetLaunchNames = "/api/v1/{0}/launch/names";
        public const string GetLaunchesByFilter = "/api/v1/{0}/launch";
        public const string GetLatestLaunchByFilter = "/api/v1/{0}/launch/latest";
        public const string GetLaunchById = "/api/v1/{0}/launch/{1}";
        public const string PostCluster = "/api/v1/{0}/launch/cluster";
        public const string LaunchUpdate = "/api/v1/{0}/launch/{1}/update";
        public const string DeleteLaunchById = "/api/v1/{0}/launch/{1}";

        public const string Users = "/api/users";
        public const string SearchUsers = "/api/users/all";

        public const string GetDashboards = "/api/v1/{0}/dashboard";
        public const string DeleteDashboard = "/api/v1/{0}/dashboard/{1}";
    }
}

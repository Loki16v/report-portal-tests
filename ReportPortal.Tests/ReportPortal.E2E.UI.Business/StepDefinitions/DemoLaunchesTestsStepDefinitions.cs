using FluentAssertions;
using ReportPortal.E2E.UI.Business.Contexts;
using ReportPortal.E2E.UI.Business.Pages;
using TechTalk.SpecFlow;

namespace ReportPortal.E2E.UI.Business.StepDefinitions
{
    [Binding]
    public class DemoLaunchesTestsStepDefinitions
    {
        private static NavigationContext NavigationContext => new();
        private static LaunchesContext LaunchesContext => new();

        [When(@"I am on Launch page of '([^']*)'")]
        public void WhenIAmOnLaunchPageOf(string projectName)
        {
            NavigationContext.GoTo<LaunchesPage>(projectName);
        }

        [Then(@"'([^']*)' launches are displayed")]
        public void ThenLaunchesAreDisplayed(int expectedCount)
        {
            var launchesList = LaunchesContext.GetLaunches();

            launchesList.Should().HaveCount(expectedCount);

        }
    }
}

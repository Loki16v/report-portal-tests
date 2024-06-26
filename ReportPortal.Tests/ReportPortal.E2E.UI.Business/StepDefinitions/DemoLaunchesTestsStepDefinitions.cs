using FluentAssertions;
using ReportPortal.E2E.Core.Driver;
using ReportPortal.E2E.Core.Models;
using ReportPortal.E2E.UI.Business.Contexts;
using TechTalk.SpecFlow;

namespace ReportPortal.E2E.UI.Business.StepDefinitions
{
    [Binding]
    public class DemoLaunchesTestsStepDefinitions
    {
        public DemoLaunchesTestsStepDefinitions(FeatureContext featureContext)
        {
             var driver = featureContext.Get<DriverFactory>(ContextKeys.DriverFactory.ToString()).GetDriver();
            _navigationContext = new NavigationContext(driver);
            _launchesContext = new LaunchesContext(driver);
        }
        private readonly NavigationContext _navigationContext;
        private readonly LaunchesContext _launchesContext;

        [When(@"I am on Launch page of '([^']*)' project")]
        public void WhenIAmOnLaunchPageOf(string projectName)
        {
            _navigationContext.GoToLaunchesPage(projectName);
        }

        [Then(@"'([^']*)' launches are displayed")]
        public void ThenLaunchesAreDisplayed(int expectedCount)
        {
            var launchesList = _launchesContext.GetLaunches();
            launchesList.Should().HaveCount(expectedCount);
        }

        [Then(@"Launches contains execution information")]
        public void ThenLaunchesContainsExecutionInformation(List<LaunchModel> launchesInfo)
        {
            var launches = _launchesContext.GetLaunches();

            foreach (var item in launchesInfo)
            {
                var result = launches.Where(x => x.TotalCount.Text.Equals(item.Total.ToString())
                                                 && x.PassedCount.Text.Equals(item.Passed.ToString()));
                if (item.Failed != 0)
                {
                    result = result.Where(x => x.FailedCount.Text.Equals(item.Failed.ToString()));
                }
                if (item.Skipped != 0)
                {
                    result = result.Where(x => x.SkippedCount.Text.Equals(item.Skipped.ToString()));
                }

                result.Should().HaveCount(1);
            }
        }

        [When(@"I am switch to '([^']*)' launches view")]
        public void WhenIAmSwitchToLaunchesView(string option)
        {
            switch (option)
            {
                case "All":
                case "Latest":
                    _launchesContext.SelectLaunches(option);
                    break;
                default:
                    throw new Exception("Required to use only 'All' or 'Latest' launches view.");
            }
        }

        [Then(@"Actions button is (enabled|disabled) by default")]
        public void ThenActionsButtonIsDisabledByDefault(bool isEnabled)
        {
            _launchesContext.IsActionsEnabled().Should().Be(isEnabled);
        }

    }
}

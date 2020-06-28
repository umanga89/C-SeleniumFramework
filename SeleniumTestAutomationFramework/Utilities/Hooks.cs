using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorEngine.Compilation.ImpromptuInterface.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using TechTalk.SpecFlow;

namespace SeleniumTestAutomationFramework.Utilities
{
    [Binding]
    public sealed class Hooks
    {
        static ExtentReports extentReports;
        static ExtentReports extentLog;
        static ExtentTest feature;
        static ExtentTest scenario;
        static ExtentTest step;
        public static ExtentTest testLog;

        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            //Check and backup current test report then clear files in test report folder
            try
            {
                if (FrameworkUtility.CheckIfFolderContainsFiles(FrameworkUtility.GetTestReportDirectory(), "html"))
                {
                    FrameworkUtility.ZipFolderLocation(FrameworkUtility.GetTestReportDirectory(), FrameworkUtility.GetTestReportBackUpDirectory() + "TestReport" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".zip");
                    FrameworkUtility.DeleteAllFilesInFolder(FrameworkUtility.GetTestReportDirectory());
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //Create extent report object
            extentReports = new ExtentReports();
            //Create html reporter object and define reporter path
            var htmlReporter = new ExtentV3HtmlReporter(FrameworkUtility.GetTestReportDirectory() + "TestResultsReport.html");
            //Adding html reporter to extent report object
            extentReports.AttachReporter(htmlReporter);
            extentReports.CreateTest("IGNORE");
            extentReports.AddSystemInfo("Host Name", "Local");
            extentReports.AddSystemInfo("Environment", "Local");
            htmlReporter.LoadConfig(FrameworkUtility.GetProjectBaseDirectory() + @"\extent-config.xml");

            //Create extent log object
            extentLog = new ExtentReports();
            var htmlLogger = new ExtentLoggerReporter(FrameworkUtility.GetTestReportDirectory());
            htmlLogger.LoadConfig(FrameworkUtility.GetProjectBaseDirectory() + @"\extent-config.xml");
            extentLog.AttachReporter(htmlLogger);
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            feature = extentReports.CreateTest(FeatureContext.Current.FeatureInfo.Title, FeatureContext.Current.FeatureInfo.Description);
            Hooks.testLog = extentLog.CreateTest(FeatureContext.Current.FeatureInfo.Title);
        }
        [BeforeScenario]
        public void BeforeScenario()
        {
            scenario = feature.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title, ScenarioContext.Current.ScenarioInfo.Description);
            Hooks.testLog = extentLog.CreateTest(ScenarioContext.Current.ScenarioInfo.Title);
        }
        [AfterStep]
        public static void AfterStep()
        {
            List<StepImageContext> imageContext = (List<StepImageContext>)ScenarioContext.Current["StepImageContext"];
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
            object TestResult = getter.Invoke(ScenarioContext.Current, null);
            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                {
                    step = scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text);
                    foreach(StepImageContext context in imageContext)
                    {
                        LogInfoWithScreenshot(context);
                    }
                }
                else if (stepType == "When")
                {
                    step = scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text);
                    foreach (StepImageContext context in imageContext)
                    {
                        LogInfoWithScreenshot(context);
                    }
                }
                else if (stepType == "Then")
                {
                    step = scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text);
                    foreach (StepImageContext context in imageContext)
                    {
                        LogInfoWithScreenshot(context);
                    }
                }
            }
            else if (ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                {
                    step = scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                    foreach (StepImageContext context in imageContext)
                    {
                        LogInfoWithScreenshot(context);
                    }
                }
                else if (stepType == "When")
                {
                    step = scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                    foreach (StepImageContext context in imageContext)
                    {
                        LogInfoWithScreenshot(context);
                    }
                }
                else if (stepType == "Then")
                {
                    step = scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                    foreach (StepImageContext context in imageContext)
                    {
                        LogInfoWithScreenshot(context);
                    }
                }
            }
            if (TestResult.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                {
                    step = scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                    step.Log(Status.Skip, "STEP DEFINITION PENDING");
                }
                else if (stepType == "When")
                {
                    step = scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                    step.Log(Status.Skip, "STEP DEFINITION PENDING");
                }
                else if (stepType == "Then")
                {
                    step = scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                    step.Log(Status.Skip, "STEP DEFINITION PENDING");
                }
            }
            ScenarioContext.Current.Clear();
        }
        [AfterFeature]
        public static void AfterFeature()
        {
                     
        }
        [AfterTestRun]
        public static void AfterTestRun()
        {
            SeleniumHelper.getDriver().Quit();
            //End report
            extentReports.Flush();
            //End log
            extentLog.Flush();
        }
        public static void LogInfoWithScreenshot(StepImageContext imageContext)
        {
            if (!imageContext.ScreenshotPathWithName.Equals(""))
            {
                MediaEntityModelProvider mediaEntityModelProvider = MediaEntityBuilder.CreateScreenCaptureFromPath(imageContext.ScreenshotPathWithName).Build();
                step.Log(Status.Pass, imageContext.StepInformation, mediaEntityModelProvider);
            }
            else
            {
                step.Log(Status.Pass, imageContext.StepInformation);
            }
        }
        public static void LogErrorWithScreenshot(Exception exception, List<StepImageContext> imageContext)
        {
            Hooks.testLog.Log(Status.Fail, exception.StackTrace + "\n" + exception.InnerException);
            imageContext = SeleniumHelper.AddScreenshotToContext(imageContext, "Error" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png", "Error");
            ScenarioContext.Current.Add("StepImageContext", imageContext);
            Assert.Fail(exception.Message);
        }
    }
}

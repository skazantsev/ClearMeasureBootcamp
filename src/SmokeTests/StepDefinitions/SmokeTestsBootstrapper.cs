﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using TechTalk.SpecFlow;

namespace SmokeTests.StepDefinitions
{
    [Binding]
    public static class SmokeTestsBootstrapper
    {
        private static Process _iisProcess;

        [BeforeTestRun]
        public static void Startup()
        {
            // kill off existing IIS Express instance if present
            var path = AppDomain.CurrentDomain.BaseDirectory.Replace("SmokeTests\\bin\\Debug", "UI");
            var matchingProcess = Process.GetProcessesByName("iisexpress").FirstOrDefault();
            matchingProcess?.Kill();
            _iisProcess = new Process
                {
                    StartInfo =
                    {
                        FileName = @"C:\Program Files (x86)\IIS Express\iisexpress.exe",
                        Arguments = $"/path:{path} /port:43507"
                    }
                };
            _iisProcess.Start();
        }
        
        [AfterTestRun]
        public static void Cleanup()
        {
            // stop IIS Express
            _iisProcess?.Kill();
        }
    }
}
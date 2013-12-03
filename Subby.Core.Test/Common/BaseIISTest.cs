using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Subby.Core.Test.Common
{
    [TestClass]
    public class BaseIISTest : BaseTestWithFiles
    {
        private Process _iisProcess;

        [TestInitialize]
        public void Setup()
        {
            var thread = new Thread(StartIISExpress) { IsBackground = true };
            thread.Start();
        }

        private void StartIISExpress()
        {
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                ErrorDialog = true,
                LoadUserProfile = true,
                CreateNoWindow = false,
                UseShellExecute = false,
                Arguments = string.Format("/path:\"{0}\" /port:{1}", Path.Combine(TestContext.TestDeploymentDir, "Files"), 10500)
            };

            var programfiles =
                string.IsNullOrEmpty(startInfo.EnvironmentVariables["programfiles"])
                    ? startInfo.EnvironmentVariables["programfiles(x86)"]
                    : startInfo.EnvironmentVariables["programfiles"];

            startInfo.FileName = programfiles + "\\IIS Express\\iisexpress.exe";

            try
            {
                _iisProcess = new Process { StartInfo = startInfo };

                _iisProcess.Start();
                _iisProcess.WaitForExit();
            }
            catch
            {
                if (_iisProcess != null && !_iisProcess.HasExited)
                {
                    _iisProcess.Kill();
                    _iisProcess.Dispose();
                }
            }
        }

        [TestCleanup]
        public void Teardown()
        {
            try
            {
                if (_iisProcess == null || _iisProcess.HasExited) return;
                _iisProcess.Kill();
                _iisProcess.Dispose();
            }
            catch
            {
            }
        }
    }
}

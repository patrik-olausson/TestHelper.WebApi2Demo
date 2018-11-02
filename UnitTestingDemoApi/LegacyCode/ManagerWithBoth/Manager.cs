using System;
using System.Net;
using UnitTestingDemoApi.LegacyCode.ManagerBastardInjection;

namespace UnitTestingDemoApi.LegacyCode.ManagerWithBoth
{
    public class Manager
    {
        private readonly ILogger _logger;
        private readonly IFtpService _ftpService;

        public Manager(ILogger logger) : this(logger, new ManagerFtpService())
        {

        }

        protected Manager(ILogger logger, IFtpService ftpService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ftpService = ftpService ?? throw new ArgumentNullException(nameof(ftpService));
        }

        public bool Transmit(byte[] bytes)
        {
            try
            {
                var result = _ftpService.UploadData(bytes);
                if (ResultIndicatesError(result))
                {
                    _logger.LogError(result.StatusCode.ToString() + ":" + result.StatusDescription);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (WebException e)
            {
                System.Net.FtpWebResponse ftpresp = e.Response as System.Net.FtpWebResponse;
                if (ftpresp != null)
                {
                    string status = ftpresp.StatusCode + ":" + ftpresp.StatusDescription + ":" + ftpresp.WelcomeMessage;
                    _logger.LogError("WebException: " + e.Message + " > " + status);
                }
                else
                {
                    _logger.LogError("WebException: " + e.Message);
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: " + ex.Message);
                return false;
            }
        }

        protected virtual bool ResultIndicatesError(UploadResult result)
        {
            return result.StatusCode != FtpStatusCode.ClosingData &&
                   result.StatusCode != FtpStatusCode.CommandOK &&
                   result.StatusCode != FtpStatusCode.FileActionOK;
        }
    }
}
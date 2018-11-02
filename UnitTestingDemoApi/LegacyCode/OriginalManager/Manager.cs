using System;
using System.Configuration;
using System.IO;
using System.Net;

namespace UnitTestingDemoApi.LegacyCode.OriginalManager
{
    public class Manager
    {
        private readonly ILogger _logger;

        public Manager(ILogger logger)
        {
            _logger = logger;
        }

        public bool Transmit(byte[] bytes)
        {
            var destAdr = ConfigurationManager.AppSettings["TransmissionDestinationAddress"];

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(destAdr);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                Stream stream = request.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();

                FtpWebResponse response = (System.Net.FtpWebResponse)(request.GetResponse());
                response.Close();
                if (response.StatusCode != FtpStatusCode.ClosingData &&
                    response.StatusCode != FtpStatusCode.CommandOK &&
                    response.StatusCode != FtpStatusCode.FileActionOK)
                {
                    _logger.LogError(response.StatusCode.ToString() + ":" + response.StatusDescription);
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
    }
}
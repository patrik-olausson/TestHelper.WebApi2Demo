using System.Configuration;
using System.IO;
using System.Net;

namespace UnitTestingDemoApi.LegacyCode.ManagerBastardInjection
{
    public class ManagerFtpService : IFtpService
    {
        public UploadResult UploadData(byte[] bytes)
        {
            var destAdr = ConfigurationManager.AppSettings["TransmissionDestinationAddress"];
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(destAdr);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            Stream stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();

            FtpWebResponse response = (System.Net.FtpWebResponse)(request.GetResponse());
            response.Close();

            return new UploadResult(response.StatusCode, response.StatusDescription);
        }
    }
}
using System.Net;

namespace UnitTestingDemoApi.LegacyCode
{
    public class UploadResult
    {
        public FtpStatusCode StatusCode { get; }
        public string StatusDescription { get; }

        public UploadResult(FtpStatusCode statusCode, string statusDescription)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
        }
    }
}
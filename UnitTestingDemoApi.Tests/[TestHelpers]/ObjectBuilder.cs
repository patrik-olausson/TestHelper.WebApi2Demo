using System.Net;
using UnitTestingDemoApi.LegacyCode;
using UnitTestingDemoApi.LegacyCode.ManagerBastardInjection;
using UnitTestingDemoApi.Models;

namespace UnitTestingDemoApi.Tests
{
    public static class ObjectBuilder
    {
        public static DemoEntity CreateDemoEntity(
            string importantProperty = "DefaultImportantProperty",
            string notSoImportantProperty = "DefaultNotSoImportantProperty")
        {
            return new DemoEntity
            {
                ImportantProperty = importantProperty,
                NotSoImportantProperty = notSoImportantProperty
            };
        }

        public static UploadResult CreateFtpUploadResult(
            FtpStatusCode statusCode = FtpStatusCode.FileActionOK,
            string statusDescription = "DefaultStatusDescription")
        {
            return new UploadResult(
                statusCode,
                statusDescription);
        }
    }
}
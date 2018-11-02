namespace UnitTestingDemoApi.LegacyCode
{
    public interface IFtpService
    {
        UploadResult UploadData(byte[] bytes);
    }
}
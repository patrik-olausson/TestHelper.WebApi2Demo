using System;

namespace UnitTestingDemoApi.Models
{
    public interface IDemoService
    {
        DemoEntity GetById(Guid id);
    }
}
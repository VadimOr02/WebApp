using System;

namespace WebAppTest
{
    public interface IScopedService
    {
        string GetInstanceId();
    }

    public class ScopedService : IScopedService
    {
        private readonly string _instanceId;

        public ScopedService()
        {
            _instanceId = Guid.NewGuid().ToString();
        }

        public string GetInstanceId()
        {
            return _instanceId;
        }
    }
}

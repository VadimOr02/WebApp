using System;

namespace WebAppTest
{
    public interface ITransientService
    {
        string GetInstanceId();
    }

    public class TransientService : ITransientService
    {
        private readonly string _instanceId;

        public TransientService()
        {
            _instanceId = Guid.NewGuid().ToString();
        }

        public string GetInstanceId()
        {
            return _instanceId;
        }
    }
}

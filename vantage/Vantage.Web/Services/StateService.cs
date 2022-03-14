using System;
using System.Threading.Tasks;

namespace Vantage.Web.Services
{
    public class StateService
    {
        public const string LinkedChat = nameof(LinkedChat);
        
        public async Task ShareStateAsync<T>(T value)
        {
            if (Notify != null) await Notify.Invoke(value);
        }

        public event Func<object,Task> Notify;
    }
}

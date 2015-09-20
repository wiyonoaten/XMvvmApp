using Android.OS;
using System;
using XMvvmApp.Platform;

namespace XMvvmApp.Android.Platform
{
    public class HandlerThreadScheduler : IThreadScheduler
    {
        private readonly Handler _handler;

        public HandlerThreadScheduler(Handler handler)
        {
            _handler = handler;
        }

        public bool Post(Action action)
        {
            return _handler.Post(action);
        }

        public bool PostDelayed(Action action, TimeSpan delay)
        {
            return _handler.PostDelayed(action, delay.Milliseconds);
        }
    }
}
using System;

namespace Xvvm.Utils
{
    public static class EventHandlerExtensions
    {
        public static void TriggerSafely(this EventHandler handler, 
            object sender)
        {
            if (handler != null)
            {
                handler(sender, EventArgs.Empty);
            }
        }

        public static void TriggerSafely<TEventArgs>(this EventHandler<TEventArgs> handler, 
            object sender, TEventArgs args)
        {
            if (handler != null)
            {
                handler(sender, args);
            }
        }
    }
}

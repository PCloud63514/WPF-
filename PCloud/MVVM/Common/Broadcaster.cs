using System;
using System.Collections.Generic;

namespace MVVM.Common
{
    public class Broadcaster : Dictionary<string, EventHandler<object>>
    {
        /// <summary>
        /// Broadcast Event 등록
        /// </summary>
        /// <param name="broadcastName"></param>
        /// <param name="eventHandler"></param>
        public void Register(string broadcastName, EventHandler<object> eventHandler)
        {
            if (ContainsKey(broadcastName))
            {
                this[broadcastName] += eventHandler;
            }
            else
            {
                Add(broadcastName, eventHandler);
            }
        }

        /// <summary>
        /// Broadcast Event 제거
        /// </summary>
        /// <param name="broadcastName"></param>
        /// <param name="eventHandler"></param>
        public void Unregister(string broadcastName, EventHandler<object> eventHandler)
        {
            if (ContainsKey(broadcastName))
            {
                this[broadcastName] -= eventHandler;
            }
        }

        /// <summary>
        /// Broadcast Call
        /// </summary>
        /// <param name="broadcastName"></param>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Broadcast(string broadcastName, object sender, object args)
        {
            this[broadcastName](sender, args);
        }
    }
}

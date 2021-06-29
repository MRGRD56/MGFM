using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGFM.Extensions
{
    public static class TaskExtensions
    {
        public static async void Then(this Task task, Action callback)
        {
            await task;
            callback?.Invoke();
        }

        public static async void Then<TResult>(this Task<TResult> task, Action<TResult> callback)
        {
            var result = await task;
            callback?.Invoke(result);
        }
    }
}

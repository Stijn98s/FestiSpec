using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiApp.Util.Util
{
    public static class TaskExtension
    {
        public static void DontAwait(this Task task)
        {
            Task.Run(async () => { await task; }  );
        }
    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QrDemo.SuperAraneidDemo
{
    class TaskHelperImage
    {
        public static Action<string> ImageCallBack;
        public static ConcurrentQueue<string> taskQueue = new ConcurrentQueue<string>();
        public static async Task RunProgram()
        {
            //ConcurrentQueue<ImageUrlWork> taskQueue = new ConcurrentQueue<ImageUrlWork>();
            var cts = new CancellationTokenSource();            
            //同时启动四个任务处理队列中的任务  
            Task[] processors = new Task[15];
            for (int i = 1; i <= 15; i++)
            {
                string processId = i.ToString();
                processors[i - 1] = Task.Run(() => TaskProcessor(taskQueue, cts.Token, processId));
            }
            await Task.Delay(5000);
            //向任务发送取消信号  
            cts.CancelAfter(TimeSpan.FromSeconds(2));
            await Task.WhenAll(processors);
        }

        //执行
        static async Task TaskProcessor(ConcurrentQueue<string> queue, CancellationToken token, string strTaskName)
        {
            string workItem;
            bool dequeueSuccesful = false;
            //await GetRandomDelay();
            do
            {
                dequeueSuccesful = queue.TryDequeue(out workItem);
                if (dequeueSuccesful)
                {
                    DoWork(queue, workItem);
                }
                await GetRandomDelay();
            }
            while (!token.IsCancellationRequested);
            if (null != ImageCallBack)
            {
                ImageCallBack("图片下载线程关闭：" + strTaskName);
            }
        }
        static async void DoWork(ConcurrentQueue<string> queue, string url)
        {
            var rtn =DownloadHelper.DowloadImg(url);
            if (null != ImageCallBack)
            {
                ImageCallBack(rtn);
            }
            await Task.Delay(50);
        }
        static Task GetRandomDelay()
        {
            int delay = 50;
            return Task.Delay(delay);
        }
    }

    class ImageUrlWork
    {
        public string URL { get; set; }
    }
}

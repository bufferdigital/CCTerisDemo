using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QrDemo.SuperAraneidDemo
{
    class TaskHelperCopy
    {
        public static Action<string, string> CallBackAddUrl;
        static HashSet<string> HaveBeenVisitedURL = new HashSet<string>();
        public static HashSet<string> ImgUrls = new HashSet<string>();
        public static async Task RunProgram(HttpUrlWork workSrc)
        {
            ConcurrentQueue<HttpUrlWork> taskQueue = new ConcurrentQueue<HttpUrlWork>();
            var cts = new CancellationTokenSource();

            //生成任务添加至并发队列  
            var taskSource = Task.Run(() => TaskProducer(taskQueue, workSrc));
            //同时启动四个任务处理队列中的任务  
            Task[] processors = new Task[10];
            for (int i = 1; i <= 10; i++)
            {
                string processId = i.ToString();
                processors[i - 1] = Task.Run(() => TaskProcessor(taskQueue, cts.Token, processId));
            }
            await taskSource;
            //向任务发送取消信号  
            cts.CancelAfter(TimeSpan.FromSeconds(2));
            await Task.WhenAll(processors);
        }

        //生成
        static async Task TaskProducer(ConcurrentQueue<HttpUrlWork> queue, HttpUrlWork workItem)
        {
            string strUrl = workItem.URL;
            bool dequeueSuccesful = HaveBeenVisitedURL.Contains(strUrl);
            if (!dequeueSuccesful)
            {
                HaveBeenVisitedURL.Add(strUrl);
                Console.WriteLine(workItem.URL);
                await Task.Delay(50);
                queue.Enqueue(workItem);
            }
            //await Task.Delay(10);
        }
        //执行
        static async Task TaskProcessor(ConcurrentQueue<HttpUrlWork> queue, CancellationToken token, string strTaskName)
        {
            HttpUrlWork workItem;
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
            if (null != CallBackAddUrl)
            {
                CallBackAddUrl("URL解析线程关闭：" + strTaskName, "");
            }

        }
        static async void DoWork(ConcurrentQueue<HttpUrlWork> queue, HttpUrlWork workItem)
        {
            string html = HttpHelper.HtmlCode(workItem.URL);
            if (null != CallBackAddUrl)
            {
                CallBackAddUrl(workItem.URL, html);
            }
            //var lstImgLink = HttpHelper.GetHtmlImageUrlList(html);
            //for (int i = 0; i < lstImgLink.Count; i++)
            //{
            //    ImgUrls.Add(lstImgLink[i]);
            //    string strimg = lstImgLink[i];
            //    TaskHelperImage.taskQueue.Enqueue(strimg);
            //}

            var lstLink = HttpHelper.GetLinks(html);
            for (int i = 0; i < lstLink.Count; i++)
            {
                var strUrl = lstLink[i];
                await TaskProducer(queue, new HttpUrlWork() { URL = strUrl });
                //queue.Enqueue(new HttpUrlWork() { URL = strUrl });
            }
        }
        static Task GetRandomDelay()
        {
            int delay = 50;
            return Task.Delay(delay);
        }
    }

    class HttpUrlWork
    {
        public string URL { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class JobSystemNew : MonoBehaviour
{
    private Queue<IJob> jobQueue = new Queue<IJob>();
    private List<Thread> runningThreads = new List<Thread>();
    private bool isRunning = true;

    void Start()
    {
        Thread jobProcessingThread = new Thread(ProcessJobs);
        jobProcessingThread.Start();
    }

    public void ScheduleJob(IJob job)
    {
        lock (jobQueue)
        {
            jobQueue.Enqueue(job);
            Monitor.Pulse(jobQueue); 
        }
    }

    private void ProcessJobs()
    {
        while (isRunning)
        {
            IJob jobToExecute = null;

            lock (jobQueue)
            {
                while (jobQueue.Count == 0 && isRunning)
                {
                    Monitor.Wait(jobQueue); 
                }

                if (jobQueue.Count > 0)
                {
                    jobToExecute = jobQueue.Dequeue();
                }
            }

            if (jobToExecute != null)
            {
                Thread jobThread = new Thread(() => ExecuteJob(jobToExecute));
                lock (runningThreads)
                {
                    runningThreads.Add(jobThread);
                }
                jobThread.Start();
            }
        }
    }

    private void ExecuteJob(IJob job)
    {
        job.Execute();

        lock (runningThreads)
        {
            runningThreads.Remove(Thread.CurrentThread);
        }
    }

    void OnDestroy()
    {
        isRunning = false;
        lock (jobQueue)
        {
            Monitor.PulseAll(jobQueue); 
        }

        foreach (var thread in runningThreads)
        {
            if (thread.IsAlive)
            {
                thread.Join(); 
            }
        }
    }

    public void WaitForAllJobs()
    {
        List<Thread> threadsToWaitFor;
        lock (runningThreads)
        {
            threadsToWaitFor = new List<Thread>(runningThreads);
        }

        foreach (var thread in threadsToWaitFor)
        {
            if (thread.IsAlive)
            {
                thread.Join(); 
            }
        }
    }
}

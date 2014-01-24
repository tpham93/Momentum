using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GameTime
{
    private Stopwatch watch;
    public TimeSpan TotalTime { get; private set; }
    public TimeSpan ElapsedTime { get; private set; }

    public GameTime()
    {
        watch = new Stopwatch();
        TotalTime = TimeSpan.FromSeconds(0);
        ElapsedTime = TimeSpan.FromSeconds(0);
    }

    public void Start()
    {
        watch.Start();
    }

    public void Stop()
    {
        watch.Reset();
        TotalTime = TimeSpan.FromSeconds(0);
        ElapsedTime = TimeSpan.FromSeconds(0);
    }

    public void Update()
    {
        //causes bad graphic glitches:
       // while (watch.Elapsed - TotalTime < TimeSpan.FromMilliseconds(17)) { }
        ElapsedTime = watch.Elapsed - TotalTime;
        TotalTime = watch.Elapsed;
    }
}
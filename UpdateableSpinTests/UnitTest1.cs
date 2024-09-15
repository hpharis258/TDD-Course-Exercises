using System.Diagnostics;

namespace UpdateableSpinTests;

[TestFixture]
public class UpdateableSpinTests
{
    [Test]
    public void UpdatableSipnTests()
    {
        UpdateableSpin updateableSpin = new UpdateableSpin();
        bool wasPulsed = updateableSpin.Wait(TimeSpan.FromMilliseconds(10));
        Assert.IsFalse(wasPulsed);
    }

    [Test]
    public void Wait_Pulse_ReturnsTrue()
    {
        UpdateableSpin updateableSpin = new UpdateableSpin();
        Task.Factory.StartNew(() =>
        {
            Thread.Sleep(100);
            updateableSpin.Set();
        });
        bool wasPulsed = updateableSpin.Wait(TimeSpan.FromSeconds(10));
        Assert.IsTrue(wasPulsed);    
    }

    [Test]
    public void Wait50Millisec_ActuallyWaytingFor50Millisec()
    {
        var spin = new UpdateableSpin();
        Stopwatch watcher = new Stopwatch();
        watcher.Start();
        spin.Wait(TimeSpan.FromMilliseconds(50));
        watcher.Stop();
        TimeSpan actual = TimeSpan.FromMilliseconds(watcher.ElapsedMilliseconds);
        TimeSpan leftEpsilon = TimeSpan.FromMilliseconds(50 - (50 * 0.1));
        TimeSpan rightEpsilon = TimeSpan.FromMilliseconds(50 + (50 * 0.1));
        Assert.IsTrue(actual > leftEpsilon && actual < rightEpsilon);
    }

    [Test]
    public void Wait50Millisec_UpdateAfter300Millisec_TotalWaitingIsApprox800Milllisec()
    {
        var spin = new UpdateableSpin();
        Stopwatch watcher = new Stopwatch();
        watcher.Start();
        const int timeout = 500;
        const int spanBeforeUpdate = 300;
        Task.Factory.StartNew(() =>
        {
            Thread.Sleep(spanBeforeUpdate);
            spin.UpdateTimeout();
        });
        spin.Wait(TimeSpan.FromMilliseconds(timeout));
        watcher.Stop();
        TimeSpan actual = TimeSpan.FromMilliseconds(watcher.ElapsedMilliseconds);
        const int expected = timeout + spanBeforeUpdate;
        TimeSpan left = TimeSpan.FromMilliseconds(expected - (expected * 0.1));
        TimeSpan right = TimeSpan.FromMilliseconds(expected + (expected * 0.1));
        Assert.IsTrue(actual > left && actual < right);
    }
}

public class UpdateableSpin
{
    private readonly object lockObject = new object();
    private bool shouldWait = true;
    private long executionStartTime;
    public bool Wait(TimeSpan timeout, int SpinDuration = 0)
    {
        UpdateTimeout();
        while (true)
        {
            lock (lockObject)
            {
                if(!shouldWait)
                {
                    return true;
                }
                if(DateTime.UtcNow.Ticks - executionStartTime > timeout.Ticks)
                {
                    return false;
                }
            }
            Thread.Sleep(SpinDuration); 
        }
        
    }
    public void Set()
    {
        lock (lockObject)
        {
            shouldWait = false;
        }
        
        
    }
    public void UpdateTimeout()
    {
        lock (lockObject)
        {
            executionStartTime = DateTime.UtcNow.Ticks;
        }
    }
}
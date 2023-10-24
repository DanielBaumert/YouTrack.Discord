namespace YouTrack.Discord;

public class RecurringJob
{
    private Func<CancellationToken, Task> _job;
    private Task _timerTask;
    private PeriodicTimer _timer;
    private CancellationTokenSource _cts;
    private bool _isRunning;
    
    public RecurringJob(TimeSpan interval)
    {
        _timer = new PeriodicTimer(interval);
    }
    
    public void Start(Func<CancellationToken, Task> job)
    {
        _cts = new CancellationTokenSource();
        _job = job;
        _timerTask = TimerTask();
    }

    private async Task TimerTask()
    {
        while (await _timer.WaitForNextTickAsync(_cts.Token))
        {
            try
            {
                await _job(_cts.Token);

            }
            catch (Exception e)
            {
                ;
            }
        }
    }
    
    public async Task StopAsync()
    {
        if (_timer == null)
        {
            return;
        }
        
        _cts.Cancel();
        await _timerTask;
        _cts.Dispose();
    }
}
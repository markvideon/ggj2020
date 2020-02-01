using System;
using System.Timers;
using UnityEngine;

public class Interpolator<T>
{
    private Timer timer;
    public T value { get;  private set; }
    private Listener OnInterval;
    private Listener OnComplete;
    private Calculation<T> Interpolation;

    private float accrued;
    private float length;
    private int intervals;

    private T first;
    private T last;

    private const int SECONDS_TO_MILLISECONDS = 1000;

    public Interpolator(T initial, T final,
        int intervalCount, float duration,
        Calculation<T> calculationCallback,
        Listener intervalCallback = null,
        Listener completeCallback = null) {

        length = duration * SECONDS_TO_MILLISECONDS;

        timer = new Timer(length/intervalCount);
        timer.Elapsed += OnTick;

        intervals = intervalCount;
        accrued = 0f;

        // Initialise values
        first = initial;
        last = final;

        Interpolation = calculationCallback;
        OnInterval = intervalCallback;
        OnComplete = completeCallback;
    }

    public void Initiate()
    {
        timer.Start();
    }

    public void Regenerate(T initial, T final, Listener completeCallback)
    {
        if (timer != null)
        {
            timer.Stop();
            timer.Dispose();
        }

        timer = new Timer(length / intervals);
        timer.Elapsed += OnTick;

        accrued = 0f;

        // Initialise values
        first = initial;
        last = final;

        OnComplete = completeCallback; 
    }

    public void OnTick(System.Object o, ElapsedEventArgs args)
    {
        if (accrued >= length)
        {
            timer.Stop();
            timer.Dispose();

            // Try-Catch: For exceptions that may not manifest in editor
            try
            {
                OnComplete?.Invoke();

            }
            catch (Exception exception)
            {
                Debug.Log(exception);
            }
        } else
        {
            value = Interpolation(first, last, accrued / length);
            OnInterval?.Invoke();
        }

        accrued += (float) timer.Interval;
    }
}

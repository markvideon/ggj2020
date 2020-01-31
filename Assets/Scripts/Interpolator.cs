using System;
using System.Timers;


public class Interpolator<T>
{
    private Timer timer;
    private T value;

    private const int SECONDS_TO_MILLISECONDS = 1000;

    Interpolator<T>() {
    }
}

using UnityEngine;
using System.Timers;
using System.Collections.Generic;

public class GameClock : MonoBehaviour
{
    [SerializeField] private float realTimeTick;
    [SerializeField] private float gameTimeScale;

    Dictionary<string, Listener> calendar;

    private Timer timer;
    private bool tick = false;
    private string displayedTime;

    private int DAY_TO_HOURS = 24;
    private int HOUR_TO_MINUTES = 60;
    private int MINUTE_TO_SECONDS = 60;
    private int SECOND_TO_MILLISECONDS = 1000;

    private int currentMilliseconds = 0;

    // Values generated from currentMilliseconds on tick
    private int currentSeconds = 0;
    private int currentMinutes = 0;
    private int currentHours = 0;
    private int currentDays = 0;

    // Notable day events
    private int CurrentHours
    {
        get { return currentHours; }
        set
        {
            if (value == 6)
            {
                OnNightToDay();
            }
            else if (value == 18)
            {
                OnDayToNight();
            }

            currentHours = value;
        }
    }

    private int CurrentDays
    {
        get { return currentDays; }
        set
        {
            if (value > currentDays)
            {
                OnNextDay();
            }

            currentDays = value;
        }
    }

    private void Start()
    {
        calendar = new Dictionary<string, Listener>();
        timer = new Timer(realTimeTick * SECOND_TO_MILLISECONDS);
        timer.Elapsed += OnTick;
    }

    private void Update()
    {
        if (tick)
        {
            // Increment time
            currentMilliseconds += (int) (timer.Interval * gameTimeScale);

            int workingTimeCopy = currentMilliseconds;

            CurrentDays = workingTimeCopy /
                    (SECOND_TO_MILLISECONDS * MINUTE_TO_SECONDS * HOUR_TO_MINUTES
                    * DAY_TO_HOURS);

            workingTimeCopy -= currentDays * (SECOND_TO_MILLISECONDS * MINUTE_TO_SECONDS * HOUR_TO_MINUTES
                    * DAY_TO_HOURS);

            CurrentHours = workingTimeCopy / (SECOND_TO_MILLISECONDS * MINUTE_TO_SECONDS * HOUR_TO_MINUTES);
            workingTimeCopy -= currentHours * (SECOND_TO_MILLISECONDS * MINUTE_TO_SECONDS * HOUR_TO_MINUTES);

            currentMinutes = workingTimeCopy / (SECOND_TO_MILLISECONDS * MINUTE_TO_SECONDS);
            workingTimeCopy -= currentMinutes * (SECOND_TO_MILLISECONDS * MINUTE_TO_SECONDS);

            currentSeconds = workingTimeCopy / (SECOND_TO_MILLISECONDS);
            workingTimeCopy -= currentSeconds * SECOND_TO_MILLISECONDS;

            SetDisplayedTime();

            tick = false;
        }
    }

    // Displayed time
    private void SetDisplayedTime()
    {
        var currentDaysLabel = currentDays.ToString();
        var currentHoursLabel = BuildTimeUnitsLabel(currentHours);
        var currentMinutesLabel = BuildTimeUnitsLabel(currentMinutes);
        var currentSecondsLabel = BuildTimeUnitsLabel(currentSeconds);

        displayedTime = currentHoursLabel + ":" + currentMinutesLabel;

        Debug.LogFormat("Days : {0}, Time: {1}:{2}:{3}",
            currentDaysLabel,
            currentHoursLabel,
            currentMinutesLabel,
            currentSecondsLabel);
    }

    private string BuildTimeUnitsLabel(int measurement)
    {
        return (measurement.ToString().Length > 1 ? "" : "0") + measurement.ToString();
    }

    // Day-based events
    private void OnDayToNight() => Debug.Log("Changed from day to night");
    private void OnNightToDay() => Debug.Log("Changed from night to day");
    private void OnNextMinute() { }
    private void OnNextHour() {}
    private void OnNextDay() {}


    // One-liners
    public void Initialise() => timer.Start();
    private void OnTick(System.Object o, ElapsedEventArgs args) => tick = true;
    private void ScheduleEvent(string time, Listener action) => calendar.Add(time, action);
    private void OnDestroy() => timer.Dispose();

}

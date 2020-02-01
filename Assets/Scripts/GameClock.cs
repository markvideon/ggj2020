using UnityEngine;
using System.Timers;

public class GameClock : MonoBehaviour
{
    [SerializeField] private float realTimeTick;
    [SerializeField] private float gameTimeScale;

    private Timer timer;
    private bool tick = false;

    private int DAY_TO_HOURS = 24;
    private int HOUR_TO_MINUTES = 60;
    private int MINUTE_TO_SECONDS = 60;
    private int SECOND_TO_MILLISECONDS = 1000;

    private int currentMilliseconds = 0;

    // Derives
    private int currentSeconds = 0;
    private int currentMinutes = 0;
    private int currentHours = 0;
    private int currentDays = 0;

    private void Start()
    {
        timer = new Timer(realTimeTick * SECOND_TO_MILLISECONDS);
        timer.Elapsed += OnTick;
        timer.Start();
    }

    private void OnTick(System.Object o, ElapsedEventArgs args)
    {
        tick = true;
    }

    private void Update()
    {
        if (tick)
        {
            // Increment time
            currentMilliseconds += (int) (timer.Interval * gameTimeScale);

            int workingTimeCopy = currentMilliseconds;

            currentDays = workingTimeCopy /
                    (SECOND_TO_MILLISECONDS * MINUTE_TO_SECONDS * HOUR_TO_MINUTES
                    * DAY_TO_HOURS);

            workingTimeCopy -= currentDays * (SECOND_TO_MILLISECONDS * MINUTE_TO_SECONDS * HOUR_TO_MINUTES
                    * DAY_TO_HOURS);

            currentHours = workingTimeCopy / (SECOND_TO_MILLISECONDS * MINUTE_TO_SECONDS * HOUR_TO_MINUTES);
            workingTimeCopy -= currentHours * (SECOND_TO_MILLISECONDS * MINUTE_TO_SECONDS * HOUR_TO_MINUTES);

            currentMinutes = workingTimeCopy / (SECOND_TO_MILLISECONDS * MINUTE_TO_SECONDS);
            workingTimeCopy -= currentMinutes * (SECOND_TO_MILLISECONDS * MINUTE_TO_SECONDS);

            currentSeconds = workingTimeCopy / (SECOND_TO_MILLISECONDS);
            workingTimeCopy -= currentSeconds * SECOND_TO_MILLISECONDS;

            var currentDaysLabel = currentDays.ToString();
            var currentHoursLabel = BuildTimeUnitsLabel(currentHours);
            var currentMinutesLabel = BuildTimeUnitsLabel(currentMinutes);
            var currentSecondsLabel = BuildTimeUnitsLabel(currentSeconds);

            Debug.LogFormat("Days : {0}, Time: {1}:{2}:{3}",
                currentDaysLabel,
                currentHoursLabel,
                currentMinutesLabel,
                currentSecondsLabel);

            tick = false;
        }
    }

    private string BuildTimeUnitsLabel(int measurement)
    {
        return (measurement.ToString().Length > 1 ? "" : "0") + measurement.ToString();
    }

    private void OnDestroy()
    {
        timer.Dispose();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationFader : MonoBehaviour
{
    [SerializeField] float duration;

    private SpriteRenderer mask;
    private Interpolator<float> maskTimer;
    private float value;
    private bool isUpdating = false;

    private float startValue = 0f;
    private float endValue = 1f;

    void Start()
    {
        mask = GetComponent<SpriteRenderer>();

        maskTimer = new Interpolator<float>(
            initial: startValue,
            final: endValue,
            intervalCount: 100,
            duration: duration,
            calculationCallback: (start, end, proportion) => proportion * end + (1f - proportion) * start,
            intervalCallback: () => value = maskTimer.value,
            completeCallback: () => value = endValue
        );
    }

    private void Update()
    {
        if (isUpdating)
        {
            mask.color = new Vector4(
                mask.color.r,
                mask.color.g,
                mask.color.b,
                value
            );
        } 
    }

    public void FadeOut(Listener onFadeOut)
    {
        Listener onComplete = onFadeOut;
        onComplete += () => isUpdating = false;

        isUpdating = true;
        maskTimer.Regenerate(startValue, endValue, onFadeOut) ;
        maskTimer.Initiate();
    }

    public void FadeIn(Listener onFadeIn)
    {
        Listener onComplete = onFadeIn;
        onComplete += () => isUpdating = false;

        isUpdating = true;
        maskTimer.Regenerate(endValue, startValue, onComplete);
        maskTimer.Initiate();
    }
}

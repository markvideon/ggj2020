using UnityEngine;

// Bring me a dream
public class MisterSoundman : MonoBehaviour
{
    [SerializeField] private AudioClip effect;
    [SerializeField] private AudioClip background;

    private AudioSource backgroundSource;
    private AudioSource effectSource;

    void Start()
    {
        backgroundSource = this.gameObject.AddComponent<AudioSource>();
        effectSource = this.gameObject.AddComponent<AudioSource>();

        backgroundSource.clip = background;
        effectSource.clip = effect;
    }

    public void SetEffectClip(AudioClip nextSound) => effectSource.clip = nextSound;
    public void SetAmbientClip(AudioClip nextSound) => backgroundSource.clip = nextSound;
    public void PlayAmbient() => backgroundSource.Play();
    public void PlayEffect() => effectSource.Play();
    public void PauseAmbient() => backgroundSource.Pause();
    public void PauseEffect() => effectSource.Pause();
}

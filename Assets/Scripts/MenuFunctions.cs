using UnityEngine;
using UnityEngine.EventSystems;

public class MenuFunctions : MonoBehaviour
{
    [SerializeField] private GameObject menuRootObject;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip playSound;

    [SerializeField] private AudioClip postMenuAmbient;

    private MisterSoundman audioPlayer;

    private int speechIdx;

    private void Start()
    {
        audioPlayer = FindObjectOfType<MisterSoundman>();
        audioPlayer.SetAmbientClip(menuMusic);
        audioPlayer.PlayAmbient();
        audioPlayer.SetEffectClip(playSound);

        if (menuRootObject == null)
        {
            menuRootObject = gameObject;
        }

    }

    public void Play()
    {
        audioPlayer.PlayEffect();
        menuRootObject.SetActive(false);

        audioPlayer.SetAmbientClip(postMenuAmbient);
        audioPlayer.SetAmbientVolume(0.5f);
        audioPlayer.PlayAmbient();
    }
}

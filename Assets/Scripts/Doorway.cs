using UnityEngine;

public class Doorway : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Transform agent;
    private NavigationFader fader;

    private CameraTechnician[] cameras;

    private Listener fadeOutCallback;
    private Listener fadeInCallback;

    private bool fadedOut = false;

    public void Start()
    {
        // FindObjectsOfType only finds Active Gameobjects at Start()
        cameras = FindObjectsOfType<CameraTechnician>();
    }

    // System.Timer executes outside of main thread
    public void Update()
    {
        if (fadedOut)
        {
            OnFadedOut();
            fadedOut = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        agent = collision.collider.transform;

        // Get mask reference
        var maskReference = agent.transform.Find("NavMask");
        fader = maskReference.GetComponent<NavigationFader>();

        // Snap to the next area after fade complete
        fadeOutCallback = () => fadedOut = true;
        fader.FadeOut(fadeOutCallback);
    }

    public void OnFadedOut()
    {
        StopAllCameras();
        agent.position = target.position;
        fader.FadeIn(fadeInCallback);
        SetActiveCamera();
    }

    // Assumes each area has a child named Camera
    public void SetActiveCamera()
    {
        var camera = target.transform.Find("Camera");
        camera.GetComponent<CameraTechnician>().active = true;
        camera.GetComponent<Camera>().enabled = true;
    }

    public void StopAllCameras()
    {
        for (int idx = 0; idx < cameras.Length; idx++)
        {
            cameras[idx].GetComponent<CameraTechnician>().active = false;
            cameras[idx].GetComponent<Camera>().enabled = false;

        }
    }
}

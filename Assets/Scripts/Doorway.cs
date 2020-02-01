using UnityEngine;
using System.Linq;

public class Doorway : MonoBehaviour
{
    [SerializeField] private Transform target;
    private CameraTechnician[] cameras;

    public void Start()
    {
        // FindObjectsOfType only finds Active Gameobjects at Start()
        cameras = FindObjectsOfType<CameraTechnician>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        StopAllCameras();
        collision.collider.transform.position = target.position;
        SetActiveCamera();
        
    }

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

using UnityEngine;
using UnityEngine.EventSystems;

public class MenuFunctions : MonoBehaviour
{
    [SerializeField] private GameObject menuRootObject;

    public void Start()
    {
        if (menuRootObject == null)
        {
            menuRootObject = gameObject;
        }
    }

    public void Play()
    {
        menuRootObject.SetActive(false);
    }
}

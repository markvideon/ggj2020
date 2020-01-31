using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private Transform topLeftBoundary;
    [SerializeField] private Transform bottomRightBoundary;

    private void Start()
    {
        if (topLeftBoundary == null || bottomRightBoundary == null)
        {
            Debug.Log("A boundary is null!");
        } else
        {
            Assert.IsTrue(topLeftBoundary.position.y > bottomRightBoundary.position.y);
            Assert.IsTrue(topLeftBoundary.position.x < bottomRightBoundary.position.x);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.A))
		{
            this.gameObject.transform.position += speedMultiplier * Vector3.left * Time.deltaTime;
		}

        if (Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.position += speedMultiplier * Vector3.right * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.W))
        {
            this.gameObject.transform.position += speedMultiplier * Vector3.up * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.S))
        {
            this.gameObject.transform.position += speedMultiplier * Vector3.down * Time.deltaTime;
        }
    }
}

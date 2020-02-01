using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private Transform topLeftBoundary;
    [SerializeField] private Transform bottomRightBoundary;

    private bool enforceBounds = true;

    private void Start()
    {
        if (topLeftBoundary == null || bottomRightBoundary == null)
        {
            enforceBounds = false;
            //Debug.Log("A boundary is null!");
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
            OptEnforceBounds(speedMultiplier * Vector3.left * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.D))
        {
            OptEnforceBounds(speedMultiplier * Vector3.right * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.W))
        {
            OptEnforceBounds(speedMultiplier * Vector3.up * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.S))
        {
            OptEnforceBounds(speedMultiplier * Vector3.down * Time.deltaTime);
        }

    }

    void EnforceBounds(Vector3 result)
    {
        Vector3 proposal = this.transform.position + result;
        if (proposal.x > bottomRightBoundary.position.x ||
            proposal.y < bottomRightBoundary.position.y ||
            proposal.x < topLeftBoundary.position.x ||
            proposal.y > topLeftBoundary.position.y)
        {
            Debug.Log("Would have taken out of bounds");
        } else
        {
            this.transform.position += result;
        }
    }

    void OptEnforceBounds (Vector3 result)
    {
        if (enforceBounds)
        {
            EnforceBounds(result);
        }
        else
        {
            this.transform.position += result;
        }
    }
}

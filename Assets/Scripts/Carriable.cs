using UnityEngine;

public class Carriable : MonoBehaviour
{
    [SerializeField] private ItemSO itemObject;

    private static Backpack backpack;

    private void Start()
    {
        if (backpack == null)
        {
            backpack = FindObjectOfType<Backpack>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Pickup seed
            backpack.Insert(itemObject.type);

            // Destroy GO
            Destroy(this.gameObject);
        }
    }
}

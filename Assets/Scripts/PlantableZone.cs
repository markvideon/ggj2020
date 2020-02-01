using UnityEngine;

public class PlantableZone : MonoBehaviour
{
    [SerializeField] ItemSO acceptedItem;
    [SerializeField] GameObject plantedPrefab;
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
            var inventory = collision.GetComponent<Backpack>();
            // If have seed
            if (inventory.Check(acceptedItem.type))
            {
                inventory.Remove(acceptedItem.type);
                // Plant seed
                if (plantedPrefab != null)
                {
                    Instantiate(plantedPrefab, this.transform);
                } else
                {
                    Debug.Log("Planted seed!");
                }
            }
        }
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 2)] // 1
public class ItemSO : ScriptableObject
{
    public ItemType type;
}

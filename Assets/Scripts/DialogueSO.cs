using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 1)] // 1
public class DialogueSO : ScriptableObject
{
    public string[] chain;
    public AudioClip[] supportingAudio;
}

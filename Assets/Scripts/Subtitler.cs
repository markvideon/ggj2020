using UnityEngine;
using TMPro;

// Managing dialogue box UI
public class Subtitler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textBox;

    public void Start()
    {
        ShowBox(false);
    }

    public void ShowBox(bool value)
    {
        textBox.transform.parent.gameObject.SetActive(value);
    }

    public void SetText(string speech)
    {
        textBox.text = speech;
    }
}

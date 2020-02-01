using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] DialogueSO dialogue;

    private string[] speech;

    private bool nearby = false;
    private Subtitler subtitler;
    private int speechIdx;

    private void Start()
    {
        subtitler = FindObjectOfType<Subtitler>();
        speech = dialogue.chain;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        nearby = true;

        if (collision.gameObject.GetComponent<PlayerController>() == null)
        {
            Debug.Log("Collider for NPC was set off by something other than the player!");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        nearby = false;
    }

    public void Update()
    {
        if (nearby)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (speechIdx < speech.Length)
                {
                    subtitler.ShowBox(true);
                    subtitler.SetText(speech[speechIdx]);
                    speechIdx++;
                } else
                {
                    speechIdx = 0;
                    subtitler.SetText("");
                    subtitler.ShowBox(false);
                }
                
            }
        }
    }
}

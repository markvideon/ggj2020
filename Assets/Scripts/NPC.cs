using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private DialogueSO dialogue;

    private string[] speech;

    private bool nearby = false;
    private Subtitler subtitler;
    private MisterSoundman audioPlayer;

    private int speechIdx;

    private void Start()
    {
        subtitler = FindObjectOfType<Subtitler>();
        audioPlayer = FindObjectOfType<MisterSoundman>();
        speech = dialogue.chain;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() == null)
        {
            Debug.Log("Collider for NPC was set off by something other than the player!");
            Debug.Log(collision.gameObject.name);
        } else
        {
            nearby = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        nearby = false;
        EndDialogue();
    }

    public void Update()
    {
        if (nearby)
        {
            Debug.Log("Nearby is true for " + this.gameObject.name);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                audioPlayer.PauseEffect();
                if (speechIdx < speech.Length)
                {

                    subtitler.ShowBox(true);
                    subtitler.SetText(speech[speechIdx]);

                   
                    if (dialogue.supportingAudio[speechIdx] != null)
                    {
                        audioPlayer.SetEffectClip(dialogue.supportingAudio[speechIdx]);
                        audioPlayer.PlayEffect();
                    }

                    speechIdx++;
                } else
                {
                    EndDialogue();
                }
                
            }
        }
    }

    public void SetActiveDialogue(DialogueSO speechSet)
    {
        dialogue = speechSet;
        speech = speechSet.chain;
    }

    public void EndDialogue()
    {
        speechIdx = 0;
        subtitler.SetText("");
        subtitler.ShowBox(false);
    }
}

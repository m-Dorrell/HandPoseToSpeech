using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadSpeaker;

public class NPC : Entity
{
    [SerializeField]
    public DialogueScript script;

    // Start is called before the first frame update
    void Start()
    {
        speaker = GetComponent<TTSSpeaker>();
    }

    public void Talk(){
        DialogueManager.instance.OpenDialogue(script);
    }
}

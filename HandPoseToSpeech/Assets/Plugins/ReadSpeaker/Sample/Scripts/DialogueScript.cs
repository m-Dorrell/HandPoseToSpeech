using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueEntry
{
    public string text;
    public string actorName;
}

[System.Serializable]
public class DialogueScript
{

    public float timeBetweenEntries = .5f;
    public List<DialogueEntry> dialogueEntries = new List<DialogueEntry>();

    public Queue<DialogueEntry> GetEntryQueue()
    {
        return new Queue<DialogueEntry>(dialogueEntries);
    }
}

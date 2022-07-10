using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ReadSpeaker;

public class DialogueManager : MonoBehaviour
{

    private static DialogueManager _instance;

    public static DialogueManager instance { get { return _instance; } }

    private bool isBusy;
    bool showDialogue = false;
    string speakerName = "Aware";
    string speakText = "Hell other you , welcome to hell.";

    public bool IsBusy(){
        return isBusy;
    }

    public void ShowDialogue(Entity entity, string text){
        isBusy = true;
        speakerName = entity.name;
        speakText = text;
        showDialogue = true;
    }

    public void HideDialogue(){
        showDialogue = false;
        isBusy = false;
    }

    public void OpenDialogue(DialogueScript script)
    {
        if (!IsBusy())
        {
            StartCoroutine(StartDialogue(script));
        }
    }
    
    void OnGUI(){
        if(showDialogue){
            GUI.BeginGroup(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 175));
            GUI.Box(new Rect(0,50, 400, 125),"");
            GUI.Label(new Rect(5, 50, 400, 25), speakerName);
            GUI.Label(new Rect(5,75, 400, 100), speakText);
            GUI.EndGroup();
        }
    }

    IEnumerator StartDialogue(DialogueScript script)
    {
        TTS.PauseAll();
        Queue<DialogueEntry> dialogueQueue = script.GetEntryQueue();
        while (dialogueQueue.Count > 0)
        {
            DialogueEntry entry = dialogueQueue.Dequeue();
            string dialogueText = ProcessText(entry.text);
            Entity entity = GameObject.Find(entry.actorName).GetComponent<Entity>();
            TTSSpeaker speaker = entity.GetComponent<TTSSpeaker>();
            ShowDialogue(entity, dialogueText);
            TTS.SayAsync(dialogueText, speaker);
            yield return new WaitUntil(() => speaker.audioSource.isPlaying);
            yield return new WaitUntil(() => !speaker.audioSource.isPlaying);
            yield return new WaitForSeconds(script.timeBetweenEntries);
        }
        HideDialogue();
        TTS.ResumeAll();
    }

    string ProcessText(string text)
    {
        string newText = text.Replace("#player", Player.instance.name);
        return newText;
    }

    void Awake(){
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }else{
            _instance = this;
        }
    }
}

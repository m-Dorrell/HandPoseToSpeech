using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ReadSpeaker;

public class Player : Entity
{

    private static Player _instance;
    public static Player instance { get { return _instance; } }

    NPC availableNPC;
    PlaybackDummy availableDummy;
    string messageText = "";
    public bool showControls = false;

    void Awake(){
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }
 
    // Update is called once per frame
    void Update()
    {
        if (availableDummy)
        {
            if (availableDummy.speaker.audioSource.isPlaying)
            {
                messageText = "Press [E] to pause";
                if (Input.GetKeyDown("e"))
                {
                    availableDummy.Pause();
                }
            }
            else if (availableDummy.isPaused)
            {
                messageText = "Press [E] to resume";
                if (Input.GetKeyDown("e"))
                {
                    availableDummy.Resume();
                }
            }
            else
            {
                messageText = "Press [E] to play";
                if (Input.GetKeyDown("e"))
                {
                    availableDummy.Talk();
                }
            }
        }else if (availableNPC){
                messageText = "Press [E] to talk";
                if(Input.GetKeyDown("e")){
                    availableNPC.Talk();
                }
        }else{
            messageText = "";
        }
    }

    void OnGUI(){
        if(showControls){
            GUI.BeginGroup(new Rect(0,0, 200, 185));
            GUI.Box(new Rect(0,0,200,200), "");
            GUI.Label(new Rect(5, 5, 195, 25), "Controls");
            GUI.Label(new Rect(5, 30, 195, 25), "W - Move forward");
            GUI.Label(new Rect(5, 55, 195, 25), "A - Move left");
            GUI.Label(new Rect(5, 80, 195, 25), "S - Move back");
            GUI.Label(new Rect(5, 105, 195, 25), "D - Move right");
            GUI.Label(new Rect(5, 130, 195, 25), "E - Interact");
            GUI.Label(new Rect(5, 155, 195, 25), "Use the mouse to look around");
            GUI.EndGroup();
        }
        GUI.BeginGroup(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200));
        if(messageText != ""){
            GUI.Label(new Rect(50, 100, 150, 100), messageText);
        }
        GUI.EndGroup();
    }

    void OnTriggerEnter(Collider col){
        NPC npc = col.gameObject.GetComponent<NPC>();
        PlaybackDummy playbackDummy = col.gameObject.GetComponent<PlaybackDummy>();
        if (npc){
            availableNPC = npc;
        }else if (playbackDummy)
        {
            availableDummy = playbackDummy;
        }
    }

    void OnTriggerExit(Collider col){
        NPC npc = col.gameObject.GetComponent<NPC>();
        PlaybackDummy playbackDummy = col.gameObject.GetComponent<PlaybackDummy>();
        if (npc){
            availableNPC = null;
        }else if (playbackDummy && playbackDummy == availableDummy)
        {
            availableDummy = null;
        }
    }
}

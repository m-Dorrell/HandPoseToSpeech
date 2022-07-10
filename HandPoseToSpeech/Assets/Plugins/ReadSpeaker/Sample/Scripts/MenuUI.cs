using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadSpeaker;

public class MenuUI : MonoBehaviour
{

    string playerName = "Input your name here...";
    int pitchSliderValue = 90;
    int speedSliderValue = 150;
    TTSEngine selectedEngine;
    List<TTSEngine> engines;

    void OnEnable(){
        TTS.Init();
        engines = TTS.GetInstalledEngines();
        selectedEngine = engines[0];
    }

    void OnGUI(){
        GUI.Box(new Rect(0,0, Screen.width, Screen.height), "");
        GUI.BeginGroup(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 300, 800, 600));
        Texture texture = Resources.Load<Texture>("Textures/logo_readspeaker-1");
        float x = texture.width / 2;
        float y = texture.height / 2;
        GUI.DrawTexture(new Rect(400 - (x / 2), 50, x, y), texture);

        GUI.Label(new Rect(200, 160, 200, 25), "Voice: " + selectedEngine.id);
        GUI.BeginGroup(new Rect(200, 185, 800, 50));
        float xPos = 0;
        float yPos = 0;
        for(int i = 0; i < engines.Count && i < 8; i++){
            if(GUI.Button(new Rect(xPos, yPos, 100, 25), engines[i].id)){
                selectedEngine = engines[i];
            }
            xPos += 100;
            if(i == 3){
                yPos = 25;
                xPos = 0;
            }
            
        }
        GUI.EndGroup();

        GUI.Label(new Rect(200, 250, 50, 25), "Name: ");
        playerName = GUI.TextField(new Rect(250, 250, 350, 25), playerName);
        GUI.Label(new Rect(200, 300, 50, 25), "Pitch: ");
        pitchSliderValue = (int)GUI.HorizontalSlider(new Rect(250, 305, 350, 50), (int)pitchSliderValue, 50, 200);
        GUI.Label(new Rect(200, 350, 50, 25), "Speed: ");
        speedSliderValue = (int)GUI.HorizontalSlider(new Rect(250, 355, 350, 50), (int)speedSliderValue, 50, 400);
        if(GUI.Button(new Rect(200, 400, 400, 50), "Preview")){
            Listen();
        }

        if(GUI.Button(new Rect(200, 450, 400, 50), "Play")){
            Play();
        }
        GUI.EndGroup();
    }

    void Listen(){
        ApplyCharacteristics();
        Player player = Player.instance;
        TTS.Say("My name is " + playerName, player.speaker, TextType.Normal);
    }

    void ApplyCharacteristics(){
        Player player = Player.instance;
        player.name = playerName;
        player.speaker.characteristics.Engine = selectedEngine;
        player.speaker.characteristics.Pitch = pitchSliderValue;
        player.speaker.characteristics.Speed = speedSliderValue;
    }

    void Play(){
        ApplyCharacteristics();
        Player player = Player.instance;
        player.enabled = true;
        player.showControls =
        Camera.main.GetComponent<CameraLook>().enabled = true;
        transform.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadSpeaker;

public class RandomVoiceSetter : MonoBehaviour
{

    TTSSpeaker speaker;

    // Start is called before the first frame update
    void Start()
    {
        speaker = GetComponent<TTSSpeaker>();
        if(speaker){
            List<TTSEngine> engines = TTS.GetInstalledEngines();
            if(engines.Count > 0){
                int rand = Random.Range(0,engines.Count);
                speaker.characteristics.Engine = engines[rand];
            }
        }
    }
}

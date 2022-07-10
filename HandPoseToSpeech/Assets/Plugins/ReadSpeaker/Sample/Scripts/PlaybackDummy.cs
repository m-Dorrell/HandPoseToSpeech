using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadSpeaker;

public class PlaybackDummy : MonoBehaviour
{

    public TTSSpeaker speaker;
    private string sayString = "Feel free to pause me at any time.";
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        speaker = GetComponent<TTSSpeaker>();
    }

    public void Pause()
    {
        if (!isPaused)
        {
            speaker.Pause();
            isPaused = true;
        }
    }

    public void Resume()
    {
        if (isPaused)
        {
            speaker.Resume();
            isPaused = false;
        }
    }

    public void Talk()
    {
        TTS.SayAsync(sayString,speaker);
    }
}

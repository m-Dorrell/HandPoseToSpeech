using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadSpeaker;

public class Dummy : MonoBehaviour
{
    public TTSSpeaker speaker;
    public string sayString;
    public float waitTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SayLoop());
    }

    IEnumerator SayLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            TTS.SayAsync(sayString, speaker, TextType.Normal, true);
        }
    }
}
using NatCorder.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    public ReplayCam recorder;
    public bool isRecording = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleRecord()
    {
        isRecording = !isRecording;
        if (isRecording)
            recorder.StopRecording();
        else
            recorder.StartRecording();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioSyncer : MonoBehaviour
{
    public GameObject linePrefab;
    //GameObject line;

    [Range(0.05f, 1)] public float bias;
    public float timeStep;
    //public float timeToBeat;
    //public float restSmoothTime;

    float previousAudioValue;
    float maxAudioValue;
    float audioValue;
    float timer;
    public int bandIndex;
    public bool isBeat;
    public virtual void OnBeat()
    {
        timer = 0;
        isBeat = true;
    }


    void Start()
    {
       // line = Instantiate(linePrefab, new Vector3(-25, 4.3f, -2f), Quaternion.identity);

    }


    private void Update()
    {
        OnUpdate();
    }
    public virtual void OnUpdate()
    {
        //if (line)
           // line.transform.position = new Vector3(line.transform.position.x, bias * AudioSpectrum.GetMaxScale, line.transform.position.z);

                previousAudioValue = audioValue;
        if (audioValue < previousAudioValue)
            maxAudioValue = previousAudioValue;
        // update audio value
    

        if (AudioSpectrum.GetUseBuffer)
            audioValue = AudioSpectrum.bandBuffers[bandIndex];
        else
            audioValue = AudioSpectrum.freqBands[bandIndex];

        // if audio value went below the bias during this frame
        if (previousAudioValue > bias &&
            audioValue <= bias)
        {
            // if minimum beat interval is reached
            //if (timer > timeStep)
            //OnBeat();
        }

        // if audio value went above the bias during this frame
        if (previousAudioValue <= bias &&
            audioValue > bias)
        {
            // if minimum beat interval is reached
            //if (timer > timeStep)
            //   OnBeat();
        }

        if (audioValue >= bias)
        {
            //if (m_timer > timeStep)
            // OnBeat();
        }

        if (audioValue > previousAudioValue && audioValue >= bias)
        {

            if (timer > timeStep)
                OnBeat();
        }

        timer += Time.deltaTime;
    }


}
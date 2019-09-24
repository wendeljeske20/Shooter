using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioSyncer : MonoBehaviour
{
    public GameObject linePrefab;
    GameObject line;

    [Range(0.05f, 1)] public float bias;
    public float timeStep;
    //public float timeToBeat;
    //public float restSmoothTime;

    private float m_previousAudioValue;
    private float m_audioValue;
    private float m_timer;
    public int bandIndex;
    public bool m_isBeat;
    public virtual void OnBeat()
    {
        m_timer = 0;
        m_isBeat = true;
    }


    void Start()
    {
        line = Instantiate(linePrefab, new Vector3(-25, 4.3f, -2f), Quaternion.identity);

    }


    private void Update()
    {
        OnUpdate();
    }
    public virtual void OnUpdate()
    {
        if (line)
            line.transform.position = new Vector3(line.transform.position.x, bias * AudioSpectrum.GetMaxScale, line.transform.position.z);
        // update audio value
        m_previousAudioValue = m_audioValue;

        if (AudioSpectrum.GetUseBuffer)
            m_audioValue = AudioSpectrum.audioBandBuffers[bandIndex];
        else
            m_audioValue = AudioSpectrum.audioBands[bandIndex];

        // if audio value went below the bias during this frame
        if (m_previousAudioValue > bias &&
            m_audioValue <= bias)
        {
            // if minimum beat interval is reached
            if (m_timer > timeStep)
                OnBeat();
        }

        // if audio value went above the bias during this frame
        if (m_previousAudioValue <= bias &&
            m_audioValue > bias)
        {
            // if minimum beat interval is reached
            if (m_timer > timeStep)
                OnBeat();
        }

        if (m_audioValue >= bias)
        {
            //if (m_timer > timeStep)
            // OnBeat();
        }

        m_timer += Time.deltaTime;
    }


}
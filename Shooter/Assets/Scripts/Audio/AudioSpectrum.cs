using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioSpectrum : MonoBehaviour
{
    public GameObject[] cubes;
    //public GameObject line;

    public int maxScale = 10;
    public static int GetMaxScale;
    public bool useBuffer;
    public static bool GetUseBuffer;
    //public static float spectrumValue { get; private set; }

    // Unity fills this up for us
    public static float[] samples = new float[512];
    public static float[] freqBands = new float[8];
    public static float[] bandBuffers = new float[8];
    float[] bufferDecrease = new float[8];

    float[] freqBandHighests = new float[8];
    public static float[] audioBands = new float[8];
    public static float[] audioBandBuffers = new float[8];

    float amplitude, amplitudeBuffer;
    float amplitudeHighest;

    public SubAudioClip[] subAudioClips;

    AudioSource audioSource;

    public Text musicTimeText;

    // private float[] freqData;
    // private int nSamples = 1024;
    // private float fMax;

    // float BandVol(float fLow, float fHigh)
    // {

    //     fLow = Mathf.Clamp(fLow, 20, fMax); // limit low...
    //     fHigh = Mathf.Clamp(fHigh, fLow, fMax); // and high frequencies
    //                                             // get spectrum
    //     audioSource.GetSpectrumData(freqData, 0, FFTWindow.BlackmanHarris);
    //     int n1 = (int)Mathf.Floor(fLow * nSamples / fMax);
    //     int n2 = (int)Mathf.Floor(fHigh * nSamples / fMax);
    //     float sum = 0;
    //     // average the volumes of frequencies fLow to fHigh
    //     for (int i = n1; i <= n2; i++)
    //     {
    //         sum += freqData[i];
    //     }
    //     return sum / (n2 - n1 + 1);
    // }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //line = GameObject.Find("Line");

        //freqData = new float[nSamples];
        //fMax = AudioSettings.outputSampleRate / 2;

    }
    private void Update()
    {
        musicTimeText.text = audioSource.time.ToString();
        GetMaxScale = maxScale;
        GetUseBuffer = useBuffer;

        GetSpectrumAudioSource();
        MakeFrequenceBands();
        BandBuffer();
        CreateAudioBands();
        //GetAmplitude();
        UpdateCubes();

        // if (samples != null && samples.Length > 0)
        // {
        //     if (useBuffer)
        //         spectrumValue = audioBandBuffers[0];
        //     else
        //         spectrumValue = audioBands[0];
        // }
    }

    void MakeFrequenceBands()
    {


        int count = 0;
        for (int i = 0; i < cubes.Length; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == 7)
                sampleCount += 2;

            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;
            freqBands[i] = average * 10;



        }
    }

    void BandBuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (freqBands[i] > bandBuffers[i])
            {
                bandBuffers[i] = freqBands[i];
                bufferDecrease[i] = 0.005f;
            }

            if (freqBands[i] < bandBuffers[i])
            {
                bandBuffers[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
        }
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (freqBands[i] > freqBandHighests[i])
            {
                freqBandHighests[i] = freqBands[i];
            }
            audioBands[i] = freqBands[i] / freqBandHighests[i];
            audioBandBuffers[i] = bandBuffers[i] / freqBandHighests[i];
        }
    }
    void GetAmplitude()
    {
        float currentAmplitude = 0;
        float currentAmplitudeBuffer = 0;

        for (int i = 0; i < 8; i++)
        {
            currentAmplitude += audioBands[i];
            currentAmplitudeBuffer += audioBandBuffers[i];
        }
        if (currentAmplitude > amplitudeHighest)
            amplitudeHighest = currentAmplitude;

        amplitude = currentAmplitude / amplitudeHighest;
        amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
    }
    void UpdateCubes()
    {
        for (int i = 0; i < 8; i++)
        {
            if (audioBands[i] > 0)
            {
                float scaleY = 0;
                if (useBuffer)
                {
                    scaleY = audioBandBuffers[i] * maxScale;
                }
                else
                {
                    scaleY = audioBands[i] * maxScale;
                }

                if (scaleY >= 0)
                    cubes[i].transform.localScale = new Vector3(0.5f, scaleY, 0.5f);
            }


            Vector3 newPos = cubes[i].transform.position;
            newPos.y = cubes[i].transform.localScale.y / 2;
            cubes[i].transform.position = newPos;
        }
    }
    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Rectangular);
    }




}
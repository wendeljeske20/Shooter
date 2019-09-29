using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioSpectrum : MonoBehaviour
{
    public GameObject[] cubes;
    //public GameObject line;

    public int scale = 10;
    public static int GetMaxScale;
    public bool useBuffer;
    public static bool GetUseBuffer;
    //public static float spectrumValue { get; private set; }

    // Unity fills this up for us
    public static float[] samples = new float[512];
    public static float[] freqBands = new float[8];
    public static float[] bandBuffers = new float[8];
    float[] bufferDecrease = new float[8];

    // float[] freqBandHighests = new float[8];
    //public static float[] audioBands = new float[8];
    //public static float[] audioBandBuffers = new float[8];

    float amplitude, amplitudeBuffer;
    float amplitudeHighest;
    public GameObject spectrumGO;
    AudioManager audioManager;
    //public float interval = 0.1f;
    // public float nextInterval;
    public static List<List<float>> position2DList = new List<List<float>>(8);
    AudioSource audioSource;
    public Text musicTimeText;
    public int graphicLenght;
    public static float timer;
    public static bool canDrawSpectrum;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        //line = GameObject.Find("Line");

        //freqData = new float[nSamples];
        //fMax = AudioSettings.outputSampleRate / 2;
        for (int i = 0; i < 8; i++)
        {
            position2DList.Add(new List<float>());
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        musicTimeText.text = audioSource.time.ToString();
        GetMaxScale = scale;
        GetUseBuffer = useBuffer;

        GetSpectrumAudioSource();
        MakeFrequenceBands();
        BandBuffer();
        //CreateAudioBands();
        //GetAmplitude();
        UpdateCubes();

        if (!audioManager.isPaused)
        {
            timer++;
            //nextInterval += Time.deltaTime;

        }

        if (canDrawSpectrum)
            DrawSpectrum();

        // if (samples != null && samples.Length > 0)
        // {
        //     if (useBuffer)
        //         spectrumValue = audioBandBuffers[0];
        //     else
        //         spectrumValue = audioBands[0];
        // }


    }

    void DrawSpectrum()
    {
        Vector3 drawPosition = transform.position;

        float offsetX = -timer;


        //for (int i = 0; i < audioManager.subClips.Length; i++)
        {
            int i = audioManager.currentClipIndex;
            float bias = audioManager.subClips[i].bias * scale;
            float bandIndex = audioManager.subClips[i].bandIndex;

            float y = bias - bandIndex * scale;

            Debug.DrawLine(drawPosition + new Vector3(-graphicLenght, y, 0), drawPosition + new Vector3(0, y, 0), Color.green);

        }

        for (int i = 0; i < 8; i++)
        {
            Debug.DrawLine(new Vector3(offsetX, scale * -i, 0) + drawPosition, new Vector3(0, scale * -i, 0) + drawPosition, Color.white);

            //if (nextInterval >= interval)
            {
                if (!audioManager.isPaused)
                {
                    float y = useBuffer ? freqBands[i] : bandBuffers[i];
                    position2DList[i].Add(y);
                }

                //    nextInterval = 0;
            }

            for (int j = 1; j < position2DList[i].Count; j++)
            {

                //Debug.DrawLine(drawPosition + new Vector3(-100,  -i * maxScale, 0), drawPosition + new Vector3(0, -i * maxScale, 0), Color.white);
                Vector3 pointA = drawPosition + new Vector3(j - 1 + offsetX, position2DList[i][j - 1] - i * scale, 0);
                Vector3 pointB = drawPosition + new Vector3(j + offsetX, position2DList[i][j] - i * scale, 0);
                Debug.DrawLine(pointA, pointB, Color.gray);

                if (j > graphicLenght)
                {
                    timer -= 1f / 8f;
                    position2DList[i].RemoveAt(0);
                }

            }
        }
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
                bufferDecrease[i] = 0.05f;
            }

            if (freqBands[i] < bandBuffers[i])
            {
                bandBuffers[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
        }
    }

    // void CreateAudioBands()
    // {
    //     for (int i = 0; i < 8; i++)
    //     {
    //         if (freqBands[i] > freqBandHighests[i])
    //         {
    //             freqBandHighests[i] = freqBands[i];
    //         }
    //         audioBands[i] = freqBands[i] / freqBandHighests[i];
    //         audioBandBuffers[i] = bandBuffers[i] / freqBandHighests[i];
    //     }
    // }
    // void GetAmplitude()
    // {
    //     float currentAmplitude = 0;
    //     float currentAmplitudeBuffer = 0;

    //     for (int i = 0; i < 8; i++)
    //     {
    //         currentAmplitude += audioBands[i];
    //         currentAmplitudeBuffer += audioBandBuffers[i];
    //     }
    //     if (currentAmplitude > amplitudeHighest)
    //         amplitudeHighest = currentAmplitude;

    //     amplitude = currentAmplitude / amplitudeHighest;
    //     amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
    // }
    void UpdateCubes()
    {
        for (int i = 0; i < 8; i++)
        {

            float scaleY = 0;
            if (useBuffer)
                scaleY = bandBuffers[i];
            else
                scaleY = freqBands[i];


            if (scaleY >= 0)
                cubes[i].transform.localScale = new Vector3(0.5f, scaleY, 0.5f);



            Vector3 newPos = cubes[i].transform.position;
            newPos.y = spectrumGO.transform.position.y + cubes[i].transform.localScale.y / 2;
            cubes[i].transform.position = newPos;
        }
    }
    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Rectangular);
    }




}
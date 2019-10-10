using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[System.Serializable]
public class SubClip
{
    public float startTime;//, endTime;
    public int bandIndex;
    public float bias = 0.5f;
}


public class AudioManager : MonoBehaviour
{

    AudioSource audioSource;

    public bool isPaused;
    public int currentClipIndex;


    GameObject pointLight;
    public SubClip[] subClips = new SubClip[20];



    void Start()
    {
        pointLight = GameObject.Find("Sun");
        audioSource = GameObject.FindObjectOfType<AudioSource>();

        audioSource.Play();
        isPaused = false;
        currentClipIndex = 0;
        AudioSpectrum.canDrawSpectrum = true;
        //PlaySubClip(0);

    }
    private void Update()
    {


        if (currentClipIndex < subClips.Length - 1 && audioSource.time > subClips[currentClipIndex + 1].startTime &&
        subClips[currentClipIndex + 1].startTime != 0 && subClips[currentClipIndex].startTime < subClips[currentClipIndex + 1].startTime)
        {

            currentClipIndex++;

        }

        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
        for (int i = 0; i < weapons.Length; i++)
        {
            //weapons[i].GetComponent<AudioSyncer>().subClipIndex ANTERIOR
            weapons[i].GetComponent<AudioSyncer>().subClip = subClips[currentClipIndex];
            if (pointLight)
                pointLight.GetComponent<AudioSyncer>().subClip = subClips[currentClipIndex];
            //if (currentClipIndex > weapons[i].GetComponent<AudioSyncer>().subClipIndex)
            {
                // if (!weapons[i].transform.parent.gameObject.CompareTag("Player"))
                //weapons[i].SetActive(false);
            }

        }

    }

    public void PauseMusic()
    {
        audioSource.Pause();
        isPaused = true;

    }

    public void ResumeMusic()
    {
        audioSource.UnPause();
        isPaused = false;
    }


    public void PlaySubClip(int i)
    {
        if (subClips[i].startTime < audioSource.clip.length)
        {
            audioSource.time = subClips[i].startTime;

            currentClipIndex = i;

            AudioSpectrum.timer = 0;
            AudioSpectrum.canDrawSpectrum = true;

            for (int j = 0; j < 8; j++)
            {
                AudioSpectrum.position2DList[j].Clear();

            }

            audioSource.Play();
            isPaused = false;
        }



    }


}

// [CustomEditor(typeof(AudioManager))]
// public class AudioManagerEditor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         AudioManager audioManager = (AudioManager)target;

//         if (!audioManager.isPaused)
//         {
//             if (GUILayout.Button("Pause Music"))
//             {
//                 audioManager.PauseMusic();
//             }
//         }
//         else
//         {
//             if (GUILayout.Button("Resume Music"))
//             {
//                 audioManager.ResumeMusic();
//             }
//         }



//         for (int i = 0; i < audioManager.subClips.Length; i++)
//         {
//             Rect rect = EditorGUILayout.GetControlRect(false, 2);

//             //rect.height = 2;

//             EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));

//             EditorGUILayout.BeginHorizontal();
//             GUILayout.Label("Clip " + (i + 1));

//             if (GUILayout.Button("Play"))
//             {
//                 audioManager.PlaySubClip(i);
//             }

//             EditorGUILayout.EndHorizontal();

//             EditorGUILayout.Space();

//             EditorGUILayout.BeginHorizontal();

//             if (audioManager.subClips != null)
//             {
//                 audioManager.subClips[i].startTime = EditorGUILayout.FloatField("Start Time", audioManager.subClips[i].startTime);
//             }

//             EditorGUILayout.EndHorizontal();

//             EditorGUILayout.BeginHorizontal();
//             GUILayout.Label("Band");
//             audioManager.subClips[i].bandIndex = EditorGUILayout.IntSlider(audioManager.subClips[i].bandIndex, 0, 7);

//             GUILayout.Label("Bias");
//             audioManager.subClips[i].bias = EditorGUILayout.Slider(audioManager.subClips[i].bias, 0, 1);
//             EditorGUILayout.EndHorizontal();
//         }

//     }
// }
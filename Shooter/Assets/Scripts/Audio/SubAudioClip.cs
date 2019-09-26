using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class SubAudioClip : MonoBehaviour
{
    AudioSource audioSource;

    int currentClipIndex;
    [System.Serializable]
    public class Info
    {
        public float startTime;//, endTime;
        public int bandIndex;
        public float bias = 0.5f;
    }

    [HideInInspector] public Info[] infos = new Info[12];



    void Start()
    {
        audioSource = GameObject.Find("Music").GetComponent<AudioSource>();

    }
    private void Update()
    {


        if (currentClipIndex < infos.Length - 1 && audioSource.time > infos[currentClipIndex + 1].startTime && infos[currentClipIndex + 1].startTime != 0)
        {

            currentClipIndex++;

        }
        //Debug.Log(currentClipIndex);
        // if (audioSource.time == startTime)


        // audioSource.Stop();
        // Debug.Log(audioSource.clip.length);

        // if (audioSource.time >= endTime )
        // audioSource.Stop();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<AudioSyncer>().bias = infos[currentClipIndex].bias;
            enemies[i].GetComponent<AudioSyncer>().bandIndex = infos[currentClipIndex].bandIndex;
        }

    }


    public void PlaySubClip(int i)
    {
        if (infos[i].startTime < audioSource.clip.length)
        {
            audioSource.time = infos[i].startTime;

            currentClipIndex = i;
            for (int j = 0; j < 8; j++)
            {
                AudioSpectrum.position2DList[j].Clear();
                AudioSpectrum.timer = 0;
                AudioSpectrum.canDrawSpectrum = true;
            }

            audioSource.Play();
        }



    }

    private void OnValidate()
    {

    }

}



[CustomEditor(typeof(SubAudioClip))]
public class SubAudioClipEditor : Editor
{
    public override void OnInspectorGUI()
    {


        // base.OnInspectorGUI();


        SubAudioClip subAudioClip = (SubAudioClip)target;


        for (int i = 0; i < subAudioClip.infos.Length; i++)
        {
            Rect rect = EditorGUILayout.GetControlRect(false, 2);

            //rect.height = 2;

            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Clip " + (i + 1));

            if (GUILayout.Button("Play"))
            {
                subAudioClip.PlaySubClip(i);
            }


            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();




            if (subAudioClip.infos != null)
            {
                subAudioClip.infos[i].startTime = EditorGUILayout.FloatField("Start Time", subAudioClip.infos[i].startTime);
            }



            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Band");
            subAudioClip.infos[i].bandIndex = EditorGUILayout.IntSlider(subAudioClip.infos[i].bandIndex, 0, 7);

            GUILayout.Label("Bias");
            subAudioClip.infos[i].bias = EditorGUILayout.Slider(subAudioClip.infos[i].bias, 0, 1);
            EditorGUILayout.EndHorizontal();
        }

    }
}
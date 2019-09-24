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

    public Info[] info = new Info[8];



    void Start()
    {
        audioSource = GameObject.Find("Music").GetComponent<AudioSource>();

    }
    private void Update()
    {
        if (currentClipIndex < info.Length - 1 && audioSource.time > info[currentClipIndex + 1].startTime)
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
            enemies[i].GetComponent<AudioSyncer>().bias = info[currentClipIndex].bias;
            enemies[i].GetComponent<AudioSyncer>().bandIndex = info[currentClipIndex].bandIndex;
        }

    }
    public void PlaySubClip(int i)
    {
        if (info[i].startTime < audioSource.clip.length)
        {
            audioSource.time = info[i].startTime;

            currentClipIndex = i;
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


        for (int i = 0; i < subAudioClip.info.Length; i++)
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




            if (subAudioClip.info != null)
            {
                subAudioClip.info[i].startTime = EditorGUILayout.FloatField("Start Time", subAudioClip.info[i].startTime);
            }



            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Band");
            subAudioClip.info[i].bandIndex = EditorGUILayout.IntSlider(subAudioClip.info[i].bandIndex, 0, 7);

            GUILayout.Label("Bias");
            subAudioClip.info[i].bias = EditorGUILayout.Slider(subAudioClip.info[i].bias, 0, 1);
            EditorGUILayout.EndHorizontal();
        }

    }
}
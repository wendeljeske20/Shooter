using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// [CustomEditor(typeof(PathManager))]
// public class PathManagerEditor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         DrawDefaultInspector();

//         PathManager pathManager = (PathManager)target;
//         if (GUILayout.Button("Update Path"))
//         {
//             pathManager.ResetPathPositions();
//         }
//     }
// }
public class PathManager : MonoBehaviour
{


    float nextTime;

    //public float last1 = 1, last2 = 1, current = 2;

    public int highlightOffset, highlight;

    public Color defaultColor;
    public Color highlightColor;

    public List<Path> pathList = new List<Path>();
    [Range(0, 10)] public int selectedPathIndex;
    //float x, y;

    public float interval;

    private void Start()
    {

    }
    private void Update()
    {

        // turnFraction += Time.deltaTime / 10;
        if (Time.time - interval >= interval)
        {
            //Debug.Log("teste");
        }
    }


    private void OnDrawGizmos()
    {
        if (selectedPathIndex < pathList.Count)
        {
            Path selectedPath = pathList[selectedPathIndex];
            selectedPath.CreatePositions();

            for (int i = 0; i < selectedPath.pointsAmount; i++)
            {

                Gizmos.color = defaultColor;
                if ((i + highlightOffset) % highlight == 0)
                {
                    Gizmos.color = highlightColor;
                }



                Gizmos.DrawSphere(selectedPath.positionList[i], 0.1f);
            }
        }

    }



}

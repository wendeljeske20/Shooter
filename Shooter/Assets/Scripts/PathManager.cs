using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PathManager : MonoBehaviour
{


    float nextTime;

  
    public bool drawLines, drawPoints;

    public Color defaultColor;
    public Color highlightColor;

    public List<Path> pathList = new List<Path>();
    [Range(0, 10)] public int selectedPathIndex;

    void DrawPathLines()
    {
        for (int i = 0; i < pathList.Count; i++)
        {
            Path path = pathList[i];
            for (int j = 0; j < path.positionList.Count - 1; j++)
            {
                Debug.DrawLine(path.positionList[j], path.positionList[j + 1], Color.blue);
            }
        }
    }

    void DrawPathPoints(Path path)
    {

        for (int i = 0; i < path.positionList.Count; i++)
        {

            Gizmos.color = defaultColor;
            if ((i + path.highlightOffset) % path.highlight == 0)
            {
                Gizmos.color = highlightColor;
            }

            Gizmos.DrawSphere(path.positionList[i], 0.1f);
        }
    }


    private void OnDrawGizmos()
    {
        if (selectedPathIndex >= pathList.Count)
            return;

        if (drawLines)
        {

            DrawPathLines();
        }



        for (int i = 0; i < pathList.Count; i++)
        {


            Path selectedPath = pathList[i];


            if (drawPoints)
                DrawPathPoints(selectedPath);


        }



    }



}

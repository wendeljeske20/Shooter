using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PathManager : MonoBehaviour
{


    float nextTime;

    //public float last1 = 1, last2 = 1, current = 2;

    public bool drawLines, drawPoints;

    public Color defaultColor;
    public Color highlightColor;

    public List<Path> pathList = new List<Path>();
    [Range(0, 10)] public int selectedPathIndex;


    // private void OnValidate() {
    //      if (selectedPathIndex >= pathList.Count)
    //         selectedPathIndex = pathList.Count;
    // }

    private void Update()
    {

        // turnFraction += Time.deltaTime / 10;
        //if (Time.time - interval >= interval)
        {
            //Debug.Log("teste");
        }
        // Path selectedPath = pathList[selectedPathIndex];
        // selectedPath.MovePoints();

    }

    void DrawPathLines(Path path)
    {

        LineRenderer line = GetComponent<LineRenderer>();
        line.enabled = true;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.positionCount = path.positionList.Count;
        line.SetPositions(path.positionList.ToArray());
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

    void DrawControlPoints(Path path)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(path.pointA, 0.1f);
        Gizmos.DrawSphere(path.pointB, 0.1f);
        Gizmos.DrawSphere(path.pointC, 0.1f);
        Gizmos.DrawSphere(path.pointD, 0.1f);
        Gizmos.DrawSphere(path.pointE, 0.1f);
        Gizmos.DrawSphere(path.pointF, 0.1f);
    }

    private void OnDrawGizmos()
    {
        if (selectedPathIndex >= pathList.Count)
            return;

        if (drawLines && pathList[selectedPathIndex].positionList.Count > 1)
        {
            DrawPathLines(pathList[selectedPathIndex]);
        }
        else
        {
            LineRenderer line = GetComponent<LineRenderer>();
            line.enabled = false;
        }


        for (int i = 0; i < pathList.Count; i++)
        {


            Path selectedPath = pathList[i];


            if (drawPoints)
                DrawPathPoints(selectedPath);

            // if (selectedPath.curveType == Path.CurveType.Bezier)
            // {
            //     DrawControlPoints(selectedPath);
            // }

        }



    }



}

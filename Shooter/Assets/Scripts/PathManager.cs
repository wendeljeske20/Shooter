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

    public bool drawLines, drawPoints;

    public Color defaultColor;
    public Color highlightColor;

    public List<Path> pathList = new List<Path>();
    [Range(0, 10)] public int selectedPathIndex;
    //float x, y;

    // public float interval;

    // public float time;
    private void Start()
    {

    }
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
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
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

        if (selectedPathIndex < pathList.Count)
        {
            Path selectedPath = pathList[selectedPathIndex];
            if (drawLines && selectedPath.positionList.Count > 1)
                DrawPathLines(selectedPath);
            else
            {
                LineRenderer line = GetComponent<LineRenderer>();
                line.enabled = false;
            }

            if (drawPoints)
                DrawPathPoints(selectedPath);

            if (selectedPath.curveType == Path.CurveType.Bezier)
            {
                DrawControlPoints(selectedPath);
            }



        }

    }



}

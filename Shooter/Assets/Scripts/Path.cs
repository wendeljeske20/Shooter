using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Path", menuName = "Path")]
public class Path : ScriptableObject
{
    public enum CurveType
    {
        Parametric,
        Bezier
    }
    public CurveType curveType;


    public int pointsAmount = 10;
    public float turnFraction = 1;
    public float scale = 1;
    public float pow = 1;

    public Vector2 radius = Vector2.one;

    [Range(1, 10)] public int highlight = 1;
    public int highlightOffset;

    public Vector2 test = Vector2.one;

    public List<Vector3> positionList = new List<Vector3>();

    public Vector2 pointA, pointB, pointC, pointD, pointE, pointF;







    private void Start()
    {
        CreateParametricCurve();
    }

    public void CreateParametricCurve()
    {

        for (int i = 0; i < pointsAmount; i++)
        {
            float t = Mathf.Pow(i / (pointsAmount - 1f), pow);
            //float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = 2 * Mathf.PI * turnFraction * i;

            float x = radius.x * t * Mathf.Cos(azimuth / 1000 * test.x);
            float y = radius.y * t * Mathf.Sin(azimuth / 1000 * test.y);

            // float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth * test);
            // float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth * test2);
            // float z = Mathf.Cos(inclination);

            Vector3 position = new Vector3(x, y, 0) * scale;

            if (positionList.Count < pointsAmount)
            {
                if ((i + highlightOffset) % highlight == 0)
                {
                    positionList.Add(position);
                }
            }



        }
    }


    public void CreateBezierCurve()
    {

        for (int i = 0; i < pointsAmount; i++)
        {
            float t = (float)i / (pointsAmount - 1);
            Vector2 pos = Vector2.zero;
            // if (i < pointsAmount / 2)
            //     pos = QuadraticCurve(pointA, pointB, pointC, t);
            // else
            //     pos = QuadraticCurve(pointC, pointD, pointE, t);


            pos = CubicCurve(pointA, pointB, pointC, pointD, t);

            Vector3 position = new Vector3(pos.x, pos.y, 0);

            if (positionList.Count < pointsAmount)
                positionList.Add(position);


        }
    }

    public void MovePoints()
    {
        Vector2 deltaMove = pointA - pointB;
        ///Debug.Log(deltaMove);
        pointA += deltaMove;


    }

    public Vector2 Lerp(Vector2 a, Vector2 b, float t)
    {
        return a + (b - a) * t;
    }

    public Vector2 QuadraticCurve(Vector2 a, Vector2 b, Vector2 c, float t)
    {
        Vector2 p0 = Lerp(a, b, t);
        Vector2 p1 = Lerp(b, c, t);
        return Lerp(p0, p1, t);
    }

    public Vector2 CubicCurve(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float t)
    {
        Vector2 p0 = QuadraticCurve(a, b, c, t);
        Vector2 p1 = QuadraticCurve(b, c, d, t);
        return Lerp(p0, p1, t);
    }

    public Vector2 Test(Vector2 a, Vector2 b, Vector2 c, Vector2 d, Vector2 e, float t)
    {
        Vector2 p0 = CubicCurve(a, b, c, d, t);
        Vector2 p1 = CubicCurve(b, c, d, e, t);
        // Vector2 p2 = QuadraticCurve(c, d, e, t);
        return Lerp(p0, p1, t);


    }

    void OnValidate()
    {

        UpdatePoints();
        //MovePoints();

    }





    public void UpdatePoints()
    {
        positionList.Clear();
        if (curveType == CurveType.Parametric)
            CreateParametricCurve();
        if (curveType == CurveType.Bezier)
            CreateBezierCurve();

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Path", menuName = "Path")]
public class Path : ScriptableObject
{
    public enum CurveType
    {
        Simple,
        Simple2,
        Ondulated,
        Spiral,
    }
    public CurveType curveType;


    public int pointsAmount = 10;
    public float turnFraction = 1;
    public float scale = 1;
    public Vector2 indivisualScale = Vector2.one;
    public float pow = 1;

    public int less, greater = 100;

    public float rotationAngle;

    public Vector2 radius = Vector2.one;

    public Vector2 frequence = Vector2.one;

    public Vector2 multiplier = Vector2.one;
    public Vector2 offset = Vector2.zero;

    [Range(1, 10)] public int highlight = 1;
    public int highlightOffset;

    public List<Vector3> positionList = new List<Vector3>();

    public Vector2 pointA, pointB, pointC, pointD, pointE, pointF;



    public void AddPosition(int i, Vector3 position)
    {
        if (positionList.Count < pointsAmount)
        {
            if ((i + highlightOffset) % highlight == 0)
            {
                positionList.Add(position);
            }
        }
    }

    public Vector3 StartPosition { get { return positionList[0]; } }
    public Vector3 EndPosition { get { return positionList[positionList.Count - 1]; } }

    public void CreateSpiralCurve()
    {

        for (int i = less; i < greater; i++)
        {
            float t = Mathf.Pow(i / (pointsAmount - 1f), pow);
            //float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = 2 * Mathf.PI * (turnFraction / 1000f) * i;

            float x = radius.x * t * Mathf.Cos(azimuth * frequence.x);
            float y = radius.y * t * Mathf.Sin(azimuth * frequence.y);

            //float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth * angulation.x);
            //float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth * angulation.y);
            //float z = Mathf.Cos(inclination);


            Vector3 position = new Vector3(x, y, 0) * indivisualScale * scale;
            position.x += offset.x;
            position.y += offset.y;


            AddPosition(i, position);

        }
    }





    public void CreateSimpleCurve()
    {
        for (int i = less; i < greater; i++)
        {

            float t = i * multiplier.x;// * ((360f * multiplier.x) / (pointsAmount - 1f));



            float x = radius.x * t * t;
            float y = radius.y * t;

            float u = x * Mathf.Cos(rotationAngle * Mathf.Deg2Rad) - y * Mathf.Sin(rotationAngle * Mathf.Deg2Rad);
            float v = x * Mathf.Sin(rotationAngle * Mathf.Deg2Rad) + y * Mathf.Cos(rotationAngle * Mathf.Deg2Rad);

            x = u;
            y = v;


            Vector3 position = new Vector3(x, y, 0) * indivisualScale * scale;
            position.x += offset.x;
            position.y += offset.y;

            AddPosition(i, position);



        }
    }

    public void CreateSimpleCurve2()
    {
        for (int i = less; i < greater; i++)
        {

            float t = i * multiplier.x;// * ((360f * multiplier.x) / (pointsAmount - 1f));



            float x = radius.x * t;
            float y = radius.y * Mathf.Pow(t, pow);

            float u = x * Mathf.Cos(rotationAngle * Mathf.Deg2Rad) - y * Mathf.Sin(rotationAngle * Mathf.Deg2Rad);
            float v = x * Mathf.Sin(rotationAngle * Mathf.Deg2Rad) + y * Mathf.Cos(rotationAngle * Mathf.Deg2Rad);

            x = u;
            y = v;


            Vector3 position = new Vector3(x, y, 0) * indivisualScale * scale;
            position.x += offset.x;
            position.y += offset.y;

            AddPosition(i, position);



        }
    }

    public void CreateOndulatedCurve()
    {
        for (int i = less; i < greater; i++)
        {
            float t = i * ((360f * multiplier.x) / (pointsAmount - 1f));


            float x = radius.x * Mathf.Cos(t * frequence.x * Mathf.Deg2Rad) + (1f / 360f) * t;
            float y = radius.y * Mathf.Sin(t * frequence.y * Mathf.Deg2Rad);

            float u = x * Mathf.Cos(rotationAngle * Mathf.Deg2Rad) - y * Mathf.Sin(rotationAngle * Mathf.Deg2Rad);
            float v = x * Mathf.Sin(rotationAngle * Mathf.Deg2Rad) + y * Mathf.Cos(rotationAngle * Mathf.Deg2Rad);

            x = u;
            y = v;

            Vector3 position = new Vector3(x, y, 0) * indivisualScale * scale;

            position.x += offset.x;
            position.y += offset.y;

            AddPosition(i, position);



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

        if (less < 0 && greater - less > pointsAmount)
            greater = pointsAmount + less;
        else if (less >= 0 && greater - less > pointsAmount)
            greater = pointsAmount + less;
        //if (less + greater < pointsAmount)
        //less = greater - pointsAmount;

        //if (greater + less > pointsAmount)
        // pointsAmount = greater + less;
    }





    public void UpdatePoints()
    {
        positionList.Clear();
        if (curveType == CurveType.Simple)
            CreateSimpleCurve();
        else if (curveType == CurveType.Ondulated)
            CreateOndulatedCurve();
        else if (curveType == CurveType.Spiral)
            CreateSpiralCurve();
        else if (curveType == CurveType.Simple2)
            CreateSimpleCurve2();

    }

}

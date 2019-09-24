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

    public float multiplier = 1;
    public Vector2 offset = Vector2.zero;

    [Range(1, 10)] public int highlight = 1;
    public int highlightOffset;

    public List<Vector3> positionList = new List<Vector3>();

    public Vector2 pointA, pointB, pointC, pointD, pointE, pointF;



    public void AddPosition(float i, Vector3 position)
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


    public Vector3 CreateCurve(float time, bool debug = false)
    {
        float t = 0, x = 0, y = 0, u = 0, v = 0;

        t = time * multiplier;
        if (curveType == CurveType.Simple)
        {
            //t = time * multiplier;// * ((360f * multiplier.x) / (pointsAmount - 1f));

            x = radius.x * t * t;
            y = radius.y * t;


        }
        else if (curveType == CurveType.Simple2)
        {
            //t = time * multiplier;// * ((360f * multiplier.x) / (pointsAmount - 1f));

            x = radius.x * t;
            y = radius.y * Mathf.Pow(t, pow);


        }
        else if (curveType == CurveType.Spiral)
        {
            t /= 10;
            //t = Mathf.Pow(time / (pointsAmount - 1f), pow);
            //float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = 2 * Mathf.PI * (turnFraction / 1000f) * time;

            x = radius.x * t * Mathf.Cos(azimuth * frequence.x);
            y = radius.y * t * Mathf.Sin(azimuth * frequence.y);

            //float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth * angulation.x);
            //float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth * angulation.y);
            //float z = Mathf.Cos(inclination);
        }
        else if (curveType == CurveType.Ondulated)
        {
            //t = time * ((360f * multiplier) / (pointsAmount - 1f));
            t *= 10;

            x = radius.x * Mathf.Cos(t * frequence.x * Mathf.Deg2Rad) + (1f / 360f) * t;
            y = radius.y * Mathf.Sin(t * frequence.y * Mathf.Deg2Rad);
        }



        u = x * Mathf.Cos(rotationAngle * Mathf.Deg2Rad) - y * Mathf.Sin(rotationAngle * Mathf.Deg2Rad);
        v = x * Mathf.Sin(rotationAngle * Mathf.Deg2Rad) + y * Mathf.Cos(rotationAngle * Mathf.Deg2Rad);

        x = u;
        y = v;

        Vector3 position = new Vector3(x, y, 0) * indivisualScale * scale;
        position.x += offset.x;
        position.y += offset.y;

        if (debug)
            AddPosition(time, position);

        return position;


    }





    void OnValidate()
    {

        UpdatePoints();
        //MovePoints();
        //if (less < 0)
        //    pointsAmount = greater + less;
        //else
            pointsAmount = greater - less;
        //if (less < 0 && greater - less > pointsAmount)
        ///     greater = pointsAmount + less;
        // else if (less >= 0 && greater - less > pointsAmount)
        //greater = pointsAmount + less;



    }





    public void UpdatePoints()
    {
        positionList.Clear();

        for (int i = less; i < greater; i++)
        {
            CreateCurve(i, true);
        }




    }

}

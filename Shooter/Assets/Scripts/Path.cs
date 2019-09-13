using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Path", menuName = "Path")]
public class Path : ScriptableObject
{
    public int pointsAmount = 10;
    public float turnFraction = 1;
    public float scale = 1;
    public float pow = 1;

    [Range(1, 10)] public int highlight = 1;
    public int highlightOffset;

    public float test;
    public float test2;
    public List<Vector3> positionList = new List<Vector3>();

    public bool editing;



    private void Start()
    {
        CreatePositions();
    }

    public void CreatePositions()
    {

        for (int i = 0; i < pointsAmount; i++)
        {
            float t = Mathf.Pow(i / (pointsAmount - 1f), pow);
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = 2 * Mathf.PI * turnFraction * i;

            float x = t * Mathf.Cos(azimuth / 1000 * test);
            float y = t * Mathf.Sin(azimuth / 1000 * test2);

           // float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth * test);
           // float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth * test2);
           // float z = Mathf.Cos(inclination);

            Vector3 position = new Vector3(x * scale, y * scale, 0 * scale);

            if (positionList.Count < pointsAmount)
                positionList.Add(position);


        }
    }


    void OnValidate()
    {

        UpdatePoints();

    }

    public void UpdatePoints()
    {
        positionList.Clear();
        CreatePositions();
    }

}

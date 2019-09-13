using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Path", menuName = "Path")]
public class Path : ScriptableObject
{
    public int pointsAmount;
    public float turnFraction;
    public float scale;
    public float pow;
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
            float t = Mathf.Pow(i / (pointsAmount - 1f), pow) * scale;
            float angle = 2 * Mathf.PI * turnFraction * i;
            float x = t * Mathf.Cos(Mathf.Deg2Rad * angle);
            float y = t * Mathf.Sin(Mathf.Deg2Rad * angle);


            Vector3 position = new Vector3(x, y, 0);

            if (positionList.Count < pointsAmount)
                positionList.Add(position);

           
        }
    }


    void OnValidate()
    {

        positionList.Clear();
        CreatePositions();

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UB.Simple2dWeatherEffects.Standard
{
    public class Sun : MonoBehaviour
    {
        AudioSyncer audioSyncer;
        public float enableTime;
        new Light light;
        private void Start()
        {
            audioSyncer = GetComponent<AudioSyncer>();
            light = GetComponent<Light>();
        }
        void Update()
        {

            if (audioSyncer.isBeat)
            {
                StartCoroutine(WaitLight());
                audioSyncer.isBeat = false;
            }
        }



        IEnumerator WaitLight()
        {

            light.enabled = true;

            
            yield return new WaitForSeconds(enableTime);

            Color color = RandomColor();
            //light.color = color;
            light.enabled = false;
            Camera.main.GetComponent<D2FogsPE>().Color = color;


        }

        Color RandomColor()
        {
            Color color = new Color(0.5f, 0.5f, 0.5f);

            int i = Random.Range(0, 3);


            if (i == 0)
                color.r = 0;
            else if (i == 1)
                color.g = 0;
            else if (i == 2)
                color.b = 0;

            i = Random.Range(0, 3);

            if (i == 0)
                color.r = 1;
            else if (i == 1)
                color.g = 1;
            else if (i == 2)
                color.b = 1;


            else if (color.r == 0.5f)
                color.r = Random.Range(0f, 1f);
            else if (color.g == 0.5f)
                color.g = Random.Range(0f, 1f);
            else if (color.b == 0.5f)
                color.b = Random.Range(0f, 1f);

            return color;

        }
    }
}
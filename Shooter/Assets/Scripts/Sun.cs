using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UB.Simple2dWeatherEffects.Standard
{
    public class Sun : MonoBehaviour
    {
        AudioSyncer audioSyncer;
        public float time;
        private void Start()
        {
            audioSyncer = GetComponent<AudioSyncer>();
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
            Light light = GetComponent<Light>();
            light.enabled = true;

            Color color = Random.ColorHSV();
            light.color = color;
            Camera.main.GetComponent<D2FogsPE>().Color = color;


            yield return new WaitForSeconds(time);

            light.enabled = false;

        }
    }
}
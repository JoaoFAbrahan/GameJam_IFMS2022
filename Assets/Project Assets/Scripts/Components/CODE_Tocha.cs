using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CHARACTERS
{
    public class CODE_Tocha : MonoBehaviour
    {
        public Vector2 tremularValue_MinMax = new Vector2(1.8f, 2.2f);
        public Vector2 timer_MinMax = new Vector2(0.1f, 0.5f);

        private float _randValue;
        private float _randTime;
        private float _timeCounter;

        private void Start()
        {
            _randValue = Random.RandomRange(tremularValue_MinMax.x, tremularValue_MinMax.y);
            _randTime = Random.RandomRange(timer_MinMax.x, timer_MinMax.y);
            _timeCounter = 1f;
        }

        private void Update()
        {
            TremularLuz();
        }

        private void TremularLuz()
        {
            if (_timeCounter > 0)
            {

                this.transform.GetChild(0).transform.GetChild(1).GetComponent<Light2D>().intensity = _randValue;
                _timeCounter -= Time.deltaTime;
            }
            else
            {
                _randValue = Random.RandomRange(tremularValue_MinMax.x, tremularValue_MinMax.y);
                _randTime = Random.RandomRange(timer_MinMax.x, timer_MinMax.y);
                _timeCounter = _randTime;
            }
        }
    }
}
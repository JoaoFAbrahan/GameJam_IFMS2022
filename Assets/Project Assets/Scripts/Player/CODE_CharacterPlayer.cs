using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    public class CODE_CharacterPlayer : CODE_PauseFunction, ICODE_CharacterMovimentation
    {
        public float speed = 1;
        private float _speedScale = 1000;

        private void Start()
        {
            speed *= _speedScale;
        }

        private void Update()
        {
            this.Movimentation();
        }

        public void Movimentation()
        {
            float vertical_movement = Input.GetAxis("Vertical");
            float horizontal_movement = Input.GetAxis("Horizontal");
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal_movement * speed * Time.deltaTime, vertical_movement * speed * Time.deltaTime);
        }

        private void DeathCondition()
        {

        }

        public void PausePlayer()
        {

        }

    }
}
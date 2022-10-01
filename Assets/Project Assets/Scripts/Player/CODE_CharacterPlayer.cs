using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    public class CODE_CharacterPlayer : CODE_PauseFunction, ICODE_CharacterMovimentation
    {
        public float speed = 1;
        private float _speedScale = 1000;
        private Rigidbody2D rb;

        private void Start()
        {
            speed *= _speedScale;
            speed *= Time.deltaTime;
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            this.Movimentation();
        }

        public void Movimentation()
        {
            float vertical_movement = Input.GetAxis("Vertical");
            float horizontal_movement = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(horizontal_movement * speed , vertical_movement * speed);
        }

        private void DeathCondition()
        {

        }

        public void PausePlayer()
        {

        }

    }
}
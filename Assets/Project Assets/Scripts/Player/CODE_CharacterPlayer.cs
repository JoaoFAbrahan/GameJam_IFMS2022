using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    public class CODE_CharacterPlayer : CODE_PauseFunction, ICODE_CharacterMovimentation
    {
        public float moveSpeed = 1;
        public ENUM_PlayerState playerState;

        private float _speedFactorScale = 10;
        private Rigidbody2D _rigidBodyRef;
        private Vector3 _oldPosition;
        private Vector3 _newPosition;


        private void Start()
        {
            _oldPosition = transform.position;
            _newPosition = transform.position;
            tilemapSceneRef = GameObject.Find("Grid");
            moveSpeed *= _speedFactorScale;
            playerState = ENUM_PlayerState.UNPAUSED;
            //moveSpeed *= Time.deltaTime;
            _rigidBodyRef = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                playerState = FlipFlop();

                if (playerState == ENUM_PlayerState.UNPAUSED)
                {
                    this._newPosition = this.transform.position;
                    this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    this.transform.position = Vector3.Lerp(_oldPosition, _newPosition, 0.2f);
                }
            }

            CheckPauseState();
        }


        private void CheckPauseState()
        {
            switch (playerState)
            {
                case ENUM_PlayerState.UNPAUSED:
                    if (this.transform.position != _newPosition)
                    {
                        this._newPosition = this.transform.position;
                        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        this.transform.position = Vector3.Lerp(_oldPosition, _newPosition, 0.2f);
                    }
                    this.GetComponent<BoxCollider2D>().enabled = true;
                    Movimentation();
                    break;
                case ENUM_PlayerState.PAUSED:
                    this._oldPosition = transform.position;
                    this.GetComponent<BoxCollider2D>().enabled = false;
                    PausePlayer();
                    break;
            }
        }

        public void Movimentation()
        {
            float vertical_movement = Input.GetAxis("Vertical");
            float horizontal_movement = Input.GetAxis("Horizontal");
            _rigidBodyRef.velocity = new Vector2(horizontal_movement * moveSpeed, vertical_movement * moveSpeed);
        }

        private void DeathCondition()
        {

        }

        public void PausePlayer()
        {
            this.transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * speedMapRotation));
            PauseGame();
        }

    }
}
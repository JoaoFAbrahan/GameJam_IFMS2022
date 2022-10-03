using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    public class CODE_CharacterPlayer : CODE_PauseClass, ICODE_CharacterMovimentation
    {
        // Attributes of class
        public float moveSpeed = 1;

        private float _speedFactorScale = 10;
        private CODE_ColliderClass _collidderController;


        private void Start()
        {
            // Instantiating attributes
            this.moveSpeed *= this._speedFactorScale;
            playerState = ENUM_PlayerState.UNPAUSED;
            _collidderController = new CODE_ColliderClass();

            // Spawn a reference object from the center of map
            _pointOfRotation = new GameObject();
            _pointOfRotation.name = "CenterOfmap";
            _pointOfRotation.transform.position = Vector3.zero;

            // Instantiating components
            _camera = Camera.main;
            _spriteObj = GameObject.Find("SpriteObj");
            _rigidBodyRef = GetComponent<Rigidbody2D>();
            _sceneMapRef = GameObject.Find("Grid");
        }

        private void Update()
        {
            PlayerInputPause();
            CheckState();
        }


        /// <summary>
        /// Check the running state
        /// </summary>
        private void CheckState()
        {
            switch (playerState)
            {
                case ENUM_PlayerState.UNPAUSED:
                    // Execution in UNPAUSED state
                    _collidderController.ColliderEnable(_spriteObj.GetComponent<BoxCollider2D>());
                    Movimentation();

                    break;
                case ENUM_PlayerState.PAUSED:
                    // Execution in PAUSED state
                    _collidderController.ColliderDisable(_spriteObj.GetComponent<BoxCollider2D>());
                    _rigidBodyRef.velocity = Vector2.zero;
                    PlayerRotation();

                    break;
            }
        }

        /// <summary>
        /// Move the player
        /// </summary>
        public void Movimentation()
        {
            // Get movimentation inputs
            float vertical_movement = Input.GetAxis("Vertical");
            float horizontal_movement = Input.GetAxis("Horizontal");

            // Apply movimentation
            _rigidBodyRef.velocity = new Vector2(horizontal_movement * moveSpeed, vertical_movement * moveSpeed);
        }

        /// <summary>
        /// GameOver Condition
        /// </summary>
        public void DeathCondition()
        {
            // ADD COLISION OF ENEMYS
        }
    }
}
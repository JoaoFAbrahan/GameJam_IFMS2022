using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    public class CODE_CharacterEnemy : ACODE_PauseClass, ICODE_CharacterMovimentation
    {
        // Attributes of class
        public float moveSpeed = 1;

        private float _speedFactorScale = 10;
        private GameObject _playerRef;


        private void Start()
        {
            // Instantiating attributes
            this.moveSpeed *= this._speedFactorScale;
            _pointOfRotation = GameObject.Find("CenterOfmap");
            _playerRef = GameObject.Find("PFB_Player");

            // Instantiating components
            _rigidBodyRef = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            CheckState();
        }


        /// <summary>
        /// Check the running state
        /// </summary>
        private void CheckState()
        {
            // Check the player state
            switch (_playerRef.GetComponent<CODE_CharacterPlayer>().playerState)
            {
                case ENUM_PlayerState.UNPAUSED:
                    // Execution in UNPAUSED state
                    Movimentation();

                    break;
                case ENUM_PlayerState.PAUSED:
                    // Execution in PAUSED state
                    _rigidBodyRef.velocity = Vector2.zero;
                    MapRotation();

                    break;
            }
        }

        /// <summary>
        /// Move the Enemy
        /// </summary>
        public void Movimentation()
        {

        }

        protected override void MapRotation()
        {
            // Rotate the enemy based on the center of the map
            this.transform.RotateAround(_pointOfRotation.transform.position, new Vector3(0f, 0f, 1f), speedRotation * Time.deltaTime );
        }
    }
}
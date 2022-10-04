using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    public class CODE_CharacterPlayer : ACODE_PauseClass, ICODE_CharacterMovimentation
    {
        // Attributes of class
        public float moveSpeed = 3;
        public float stepSpeedFactor = 0.05f;

        private float _speedFactorScale = 2;
        private float _timeBetweenSteps;
        private AudioSource _playerAudioSource;

        private void Start()
        {
            // Instantiating attributes
            this.moveSpeed *= this._speedFactorScale;
            this._timeBetweenSteps = this.moveSpeed * stepSpeedFactor;
            playerState = ENUM_PlayerState.UNPAUSED;
            _playerAudioSource = GetComponent<AudioSource>();
            _soundManager = GameObject.Find("SoundManager").GetComponent<SONORIZATION.CODE_SoundManager>();
            _soundManager.FindSMAudioSource();

            // Play OST_Level00 for the first time
            _soundManager.PlayOst("OST_Level00");

            // Spawn a reference object from the center of map
            _pointOfRotation = new GameObject();
            _pointOfRotation.name = "CenterOfmap";
            _pointOfRotation.transform.position = Vector3.zero;

            // Instantiating components
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
                    Collider2D[] intersecting = Physics2D.OverlapCircleAll(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y), 0.01f, LayerMask.GetMask("Wall"));
                    if (intersecting.Length != 0)
                    {
                        //code to run if intersecting
                        MapRotation();
                        this._rigidBodyRef.bodyType = RigidbodyType2D.Kinematic;
                    }
                    else
                    {
                        Movimentation();
                        this._rigidBodyRef.bodyType = RigidbodyType2D.Dynamic;
                    }
                    break;
                case ENUM_PlayerState.PAUSED:
                    // Execution in PAUSED state
                    _rigidBodyRef.velocity = Vector2.zero;
                    MapRotation();

                    break;
            }
        }

        /// <summary>
        /// Move the player
        /// </summary>
        public void Movimentation()
        {
            Vector2 LookDirection;
            Vector2 SpriteDirection;
            Quaternion SpriteAngle;

            // Get movimentation inputs
            float vertical_movement = Input.GetAxis("Vertical");
            float horizontal_movement = Input.GetAxis("Horizontal");
            LookDirection = new Vector2(horizontal_movement, vertical_movement);
            LookDirection.Normalize();

            // Play step sound effect
            SoundController();


            // Apply rotation in sprite based to inputs
            if (LookDirection != Vector2.zero)
            {
                SpriteAngle = Quaternion.LookRotation(Vector3.forward, LookDirection);
                this.transform.GetChild(0).transform.rotation = Quaternion.RotateTowards(this.transform.GetChild(0).transform.rotation, SpriteAngle, 500 * Time.deltaTime);
            }

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

        /// <summary>
        /// Play the sounds effects
        /// </summary>
        private void SoundController()
        {
            _timeBetweenSteps -= Time.deltaTime;
            if (_timeBetweenSteps < 0 && (_rigidBodyRef.velocity.x != 0 || _rigidBodyRef.velocity.y != 0))
            {
                _timeBetweenSteps = moveSpeed * stepSpeedFactor;
                _soundManager.playStepSound(_playerAudioSource);
            }
        }
    }
}
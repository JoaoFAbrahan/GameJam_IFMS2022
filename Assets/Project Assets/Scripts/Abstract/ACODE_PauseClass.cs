using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    public abstract class ACODE_PauseClass : MonoBehaviour
    {
        // Attributes of class
        public float speedRotation = 50;
        public ENUM_PlayerState playerState;
        public bool pressedPause;

        protected GameObject _pointOfRotation;
        protected GameObject _sceneMapRef;
        protected Rigidbody2D _rigidBodyRef;
        protected SONORIZATION.CODE_SoundManager _soundManager;
        protected CODE_PlayerColliderChecker _playerColliderChecker;
        protected CapsuleCollider2D _playerCollider;
        protected AudioSource _quetzaAudioSource;

        private static ENUM_PlayerState _pauseState = ENUM_PlayerState.UNPAUSED;
        private float _distanceCenterToPlayer;

        /// <summary>
        /// Controller of the toggles state
        /// </summary>
        /// <returns></returns>
        protected ENUM_PlayerState FlipFlop()
        {
            _pauseState = _pauseState == ENUM_PlayerState.PAUSED ? ENUM_PlayerState.UNPAUSED : ENUM_PlayerState.PAUSED;
            return _pauseState;
        }

        /// <summary>
        /// Getting a player input of PAUSE/UNPAUSE
        /// </summary>
        protected void PlayerInputPause()
        {
            //if (Input.GetKeyDown(KeyCode.P))
            //{
            //    _soundManager.PlayOSTOutOfTime();
            //}
            // Unique state which the player is PAUSED, COLLIDING and PRESSING SPACE
            if (Input.GetKeyDown(KeyCode.Space) && _playerColliderChecker.isColliding && playerState == ENUM_PlayerState.PAUSED)
            {
                // Save press input
                pressedPause = true;
            }

            if (Input.GetKeyDown(KeyCode.Space) && !pressedPause)
            {
                if (!_playerColliderChecker.isColliding)
                {  
                    // Always run on the first state cycle
                    CheckStateAfterInput();
                }
            }
        }

        protected void CheckStateAfterInput() 
        {
            // Toggles state
            playerState = FlipFlop();

            if (playerState == ENUM_PlayerState.PAUSED)
            {
                _soundManager.PlayQuetzaSound(_quetzaAudioSource);
                // Toggles Distorted Level OST
                _soundManager.SwitchMainTheme("OST_Distorted_Level00");
                _playerCollider.enabled = false;
                //this._rigidBodyRef.bodyType = RigidbodyType2D.Kinematic;
                // IMPLEMENT
            }

            if (playerState == ENUM_PlayerState.UNPAUSED)
            {
                _soundManager.StopSFX(_quetzaAudioSource);
                // Toggles Normal Level OST
                _soundManager.SwitchMainTheme("OST_Level00");
                // Reset the components rotation
                _playerCollider.enabled = true;
                //this._rigidBodyRef.bodyType = RigidbodyType2D.Dynamic;
                this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }

        /// <summary>
        /// Rotate the map
        /// </summary>
        protected virtual void MapRotation()
        {
            // Rotates the map relative to the center of the Grid pivot
            _distanceCenterToPlayer = Vector2.Distance(this._sceneMapRef.transform.position, this.transform.position);
            this._sceneMapRef.transform.Rotate(new Vector3(0f, 0f, speedRotation * Time.deltaTime * -1f) / _distanceCenterToPlayer);
        }

        protected void ColliderException()
        {
            // Always run on the first state cycle
            if (playerState == ENUM_PlayerState.PAUSED)
            {
                this._rigidBodyRef.bodyType = RigidbodyType2D.Kinematic;
                // IMPLEMENT
            }
        }
    }
}
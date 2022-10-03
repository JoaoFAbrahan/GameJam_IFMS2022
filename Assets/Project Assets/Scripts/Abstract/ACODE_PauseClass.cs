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

        protected GameObject _pointOfRotation;
        protected GameObject _sceneMapRef;
        protected Rigidbody2D _rigidBodyRef;

        private static ENUM_PlayerState _pauseState = ENUM_PlayerState.UNPAUSED;


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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Toggles state
                playerState = FlipFlop();

                // Always run on the first state cycle
                if (playerState == ENUM_PlayerState.PAUSED)
                {
                    // IMPLEMENT
                }

                if (playerState == ENUM_PlayerState.UNPAUSED)
                {
                    // Reset the components rotation
                    this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
        }

        /// <summary>
        /// Rotate the map
        /// </summary>
        protected virtual void MapRotation()
        {
            // Rotates the map relative to the center of the Grid pivot
            this._sceneMapRef.gameObject.transform.Rotate(new Vector3(0f, 0f, speedRotation * Time.deltaTime * -1f));
        }
    }
}
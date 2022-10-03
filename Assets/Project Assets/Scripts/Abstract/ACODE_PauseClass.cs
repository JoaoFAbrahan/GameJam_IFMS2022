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
        protected Camera _camera;
        protected GameObject _spriteObj;
        protected GameObject _astralEnemyRef;
        protected List<GameObject> _physicEnemyRef;
        protected Rigidbody2D _rigidBodyRef;
        protected GameObject _sceneMapRef;

        private static ENUM_PlayerState _bPauseState = ENUM_PlayerState.UNPAUSED;


        /// <summary>
        /// Controller of the toggles state
        /// </summary>
        /// <returns></returns>
        protected ENUM_PlayerState FlipFlop()
        {
            _bPauseState = _bPauseState == ENUM_PlayerState.PAUSED ? ENUM_PlayerState.UNPAUSED : ENUM_PlayerState.PAUSED;
            return _bPauseState;
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
                    //this._camera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    //this._spriteObj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
        }

        /// <summary>
        /// Rotate player to new position
        /// </summary>
        protected void PlayerRotation()
        {
            // Rotates the player relative to the center of the map
            //this.transform.RotateAround(_pointOfRotation.transform.position, new Vector3(0f, 0f, 1f), speedRotation * Time.deltaTime );
            //this._camera.transform.Rotate(new Vector3(0f, 0f, speedRotation * Time.deltaTime * -1f));
            //this._spriteObj.transform.Rotate(new Vector3(0f, 0f, speedRotation * Time.deltaTime * -1f));
            this._sceneMapRef.gameObject.transform.Rotate(new Vector3(0f, 0f, speedRotation * Time.deltaTime * 1f));
        }
    }
}
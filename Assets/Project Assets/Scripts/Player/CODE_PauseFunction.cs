using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    public class CODE_PauseFunction : MonoBehaviour
    {
        // Attributes of class
        public float speedMapRotation = 1;

        protected GameObject tilemapSceneRef;
        protected GameObject astralEnemyRef;
        protected List<GameObject> physicEnemyRef;

        private static ENUM_PlayerState _bPauseState = ENUM_PlayerState.UNPAUSED;


        protected void PauseGame()
        {
            // Rotate the scene Tilemap in pause condition
            tilemapSceneRef.transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * speedMapRotation));
        }

        /// <summary>
        /// Switch boolean Method
        /// </summary>
        /// <returns></returns>
        protected ENUM_PlayerState FlipFlop()
        {
            _bPauseState = _bPauseState == ENUM_PlayerState.PAUSED ? ENUM_PlayerState.UNPAUSED : ENUM_PlayerState.PAUSED;
            return _bPauseState;
        }
    }
}
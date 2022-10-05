using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{

    public class CODE_BodyMovimentation : CODE_BodyRotation
    {
        private void Start()
        {
            _playerRef = GameObject.Find("PFB_Player");
            target = _playerRef.transform;
        }

        void Update()
        {
            RotateBody();
            MoveBody();
        }

        /// <summary>
        /// Move enemy body
        /// </summary>
        private void MoveBody()
        {
            if (_playerRef.GetComponent<CODE_CharacterPlayer>().playerState == ENUM_PlayerState.PAUSED)
            {
                // Move the enemy towards the target position
                transform.position = Vector2.MoveTowards(transform.position, target.position, flySpeed * Time.deltaTime);
            }            
        }
    }
}
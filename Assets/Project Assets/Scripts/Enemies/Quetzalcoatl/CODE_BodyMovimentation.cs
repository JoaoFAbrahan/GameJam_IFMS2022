using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{

    public class CODE_BodyMovimentation : CODE_BodyRotation
    {
        public bool playerPaused;
        private void Start()
        {
            _playerRef = GameObject.Find("PFB_Player");
            if(gameObject.name == "PFB_Quetzalcoatl")
                target = _playerRef.transform;
        }

        void Update()
        {
            if (_playerRef.GetComponent<CODE_CharacterPlayer>().isDead)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                if (playerPaused)
                {
                    if (gameObject.name == "PFB_Quetzalcoatl")
                    {
                        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    }
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                }
                else
                {
                    if (gameObject.name == "PFB_Quetzalcoatl")
                    {
                        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    }
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 1f, 0.2f);
                }

                RotateBody();
                MoveBody();
            }
           
        }

        /// <summary>
        /// Move enemy body
        /// </summary>
        private void MoveBody()
        {
            if (_playerRef.GetComponent<CODE_CharacterPlayer>().playerState == ENUM_PlayerState.PAUSED)
            {
                playerPaused = true;
                // Move the enemy towards the target position
                transform.position = Vector2.MoveTowards(transform.position, target.position, flySpeed * Time.deltaTime);
            }
            else
            {
                playerPaused = false;
            }
        }
    }
}
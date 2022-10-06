using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    public class CODE_PlayerColliderChecker : MonoBehaviour
    {
        public bool isColliding;
        public bool deathChecker;
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (LayerMask.LayerToName(collision.gameObject.layer) == LayerMask.LayerToName(3))
            {
                isColliding = true;
            }

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (LayerMask.LayerToName(collision.gameObject.layer) == LayerMask.LayerToName(3))
            {
                isColliding = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (LayerMask.LayerToName(collision.gameObject.layer) == LayerMask.LayerToName(9))
            {
                deathChecker = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (LayerMask.LayerToName(collision.gameObject.layer) == LayerMask.LayerToName(8))
            {
                deathChecker = true;
            }
        }
    }

}

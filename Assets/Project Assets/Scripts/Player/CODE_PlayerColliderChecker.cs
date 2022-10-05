using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    public class CODE_PlayerColliderChecker : MonoBehaviour
    {
        public bool isColliding;
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
    }

}

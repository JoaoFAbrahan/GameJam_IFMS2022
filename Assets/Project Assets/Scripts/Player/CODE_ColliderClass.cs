using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    public class CODE_ColliderClass : MonoBehaviour
    {
        /// <summary>
        /// Disable the entity collider
        /// </summary>
        /// <param name="collider2D"></param>
        public void ColliderDisable(BoxCollider2D collider2D)
        {
            collider2D.enabled = false;
        }

        /// <summary>
        /// Enable the entity collider
        /// </summary>
        /// <param name="collider2D"></param>
        public void ColliderEnable(BoxCollider2D collider2D)
        {
            collider2D.enabled = true;
        }
    }
}
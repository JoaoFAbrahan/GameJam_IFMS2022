using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    public class CODE_ColliderClass : MonoBehaviour
    {
        /// <summary>
        /// Disable the entity collider
        /// </summary>
        /// <param name="Collider2D"></param>
        public void ColliderDisable(BoxCollider2D Collider2D)
        {
            Collider2D.enabled = false;
        }

        /// <summary>
        /// Enable the entity collider
        /// </summary>
        /// <param name="Collider2D"></param>
        public void ColliderEnable(BoxCollider2D Collider2D)
        {
            Collider2D.enabled = true;
        }
    }
}
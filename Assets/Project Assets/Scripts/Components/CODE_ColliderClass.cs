using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ENVIRONMENT
{
    public class CODE_ColliderClass : MonoBehaviour
    {
        /// <summary>
        /// Disable the entity collider
        /// </summary>
        /// <param name="Collider2D"></param>
        public void ColliderDisable(CapsuleCollider2D Collider2D)
        {
            Collider2D.enabled = false;
        }

        /// <summary>
        /// Enable the entity collider
        /// </summary>
        /// <param name="Collider2D"></param>
        public void ColliderEnable(CapsuleCollider2D Collider2D)
        {
            Collider2D.enabled = true;
        }
    }
}
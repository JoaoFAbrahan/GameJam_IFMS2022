using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SAP2D;

namespace CHARACTERS
{
    [RequireComponent(typeof(SAP2DAgent))]
    public class CODE_CharacterAhuizolt : ACODE_PauseClass, ICODE_CharacterMovimentation
    {
        // Attributes of class
        public float moveSpeed = 1;
        public float sizeTargetArea = 5;

        private float _speedFactorScale = 10;
        private GameObject _playerRef;
        private SAP2DAgent _agent;
        private LayerMask _playerLayer;
        private LayerMask _wallLayer;
        private RaycastHit2D _raycastHit;
        private GameObject _spriteObjRef;
        private ENVIRONMENT.CODE_ColliderClass _colliderController;


        private void Start()
        {
            // Instantiating attributes
            this.moveSpeed *= this._speedFactorScale;
            this._playerRef = GameObject.Find("PFB_Player");
            this._agent = this.GetComponent<SAP2DAgent>();
            this._playerLayer = LayerMask.GetMask("Player");
            this._wallLayer = LayerMask.GetMask("Wall");
            this._agent.Target = _playerRef.transform.GetChild(0).transform;
            this._agent.CanMove = false;
            _pointOfRotation = GameObject.Find("Grid");

            // Instantiating components
            this._spriteObjRef = this.transform.GetChild(0).gameObject;
            this._colliderController = new ENVIRONMENT.CODE_ColliderClass();
            _rigidBodyRef = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            CheckState();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sizeTargetArea);
        }


        /// <summary>
        /// Check the running state
        /// </summary>
        private void CheckState()
        {
            // Check the player state
            switch (_playerRef.GetComponent<CODE_CharacterPlayer>().playerState)
            {
                case ENUM_PlayerState.UNPAUSED:
                    // Execution in UNPAUSED state
                    _colliderController.ColliderEnable(_spriteObjRef.GetComponent<BoxCollider2D>());
                    Movimentation();

                    break;
                case ENUM_PlayerState.PAUSED:
                    // Execution in PAUSED state
                    _colliderController.ColliderDisable(_spriteObjRef.GetComponent<BoxCollider2D>());
                    _rigidBodyRef.velocity = Vector2.zero;
                    _agent.CanMove = false;
                    MapRotation();

                    break;
            }
        }

        /// <summary>
        /// Move the Enemy
        /// </summary>
        public void Movimentation()
        {
            // Create are detection
            _raycastHit = Physics2D.CircleCast(this.transform.position, sizeTargetArea, Vector2.right, sizeTargetArea, _playerLayer);
                        
            if (_raycastHit)
            {
                // Create linecast for player vision detection
                if (!Physics2D.Linecast(this.transform.position, _playerRef.transform.position, _wallLayer))
                {
                    // Move the enemy in player direction
                    _agent.CanMove = true;
                }
                else
                {
                    // Stop the enemy
                    _agent.CanMove = false;
                }
            }
            else
            {
                // Stop the enemy
                _agent.CanMove = false;
            }
        }

        protected override void MapRotation()
        {
            // Rotate the enemy based on the center of the map            
            this.transform.RotateAround(_pointOfRotation.transform.position, new Vector3(0f, 0f, 1f), speedRotation * Time.deltaTime * -1f);
        }
    }
}
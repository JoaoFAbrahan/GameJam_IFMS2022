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


        private void Start()
        {
            // Instantiating attributes
            this.moveSpeed *= this._speedFactorScale;
            _pointOfRotation = GameObject.Find("CenterOfmap");
            this._playerRef = GameObject.Find("PFB_Player");
            this._agent = this.GetComponent<SAP2DAgent>();
            //_agent.Target = _playerRef.transform.GetChild(0).transform;
            this._playerLayer = LayerMask.NameToLayer("Player");
            this._wallLayer = LayerMask.NameToLayer("Wall");

            // Instantiating components
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
                    Movimentation();

                    break;
                case ENUM_PlayerState.PAUSED:
                    // Execution in PAUSED state
                    _rigidBodyRef.velocity = Vector2.zero;
                    MapRotation();

                    break;
            }
        }

        /// <summary>
        /// Move the Enemy
        /// </summary>
        public void Movimentation()
        {
            _raycastHit = Physics2D.CircleCast(this.transform.position, sizeTargetArea, Vector2.right, sizeTargetArea, _playerLayer);

            if (_raycastHit)
            {
                if (!Physics2D.Linecast(this.transform.position, _raycastHit.transform.position, _wallLayer))
                    _agent.CanMove = true;
                else
                    _agent.CanMove = false;
            }
            else
            {
                _agent.CanMove = false;
            }
        }

        protected override void MapRotation()
        {
            // Rotate the enemy based on the center of the map
            this.transform.RotateAround(_pointOfRotation.transform.position, new Vector3(0f, 0f, 1f), speedRotation * Time.deltaTime );
        }
    }
}
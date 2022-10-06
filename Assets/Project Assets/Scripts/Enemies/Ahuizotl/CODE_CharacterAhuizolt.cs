using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    public class CODE_CharacterAhuizolt : ACODE_PauseClass, ICODE_CharacterMovimentation
    {
        // Attributes of class
        public float moveSpeed = 1;
        public float sizeTargetArea = 5;

        private float _speedFactorScale = 10;
        private GameObject _playerRef;
        private LayerMask _playerLayer;
        private LayerMask _wallLayer;
        private RaycastHit2D _raycastHit;
        private GameObject _spriteObjRef;
        private ENVIRONMENT.CODE_ColliderClass _colliderController;
        private static float _randomTime = 0;
        private static GameObject _tempDirection;
        private float _distanceCenterToMonster;


        private void Start()
        {
            // Instantiating attributes
            this.moveSpeed *= this._speedFactorScale;
            this._playerRef = GameObject.Find("PFB_Player");
            speedRotation = _playerRef.GetComponent<CODE_CharacterPlayer>().speedRotation;
            this._playerLayer = LayerMask.GetMask("Player");
            this._wallLayer = LayerMask.GetMask("Wall");
            _tempDirection = new GameObject();
            _tempDirection.transform.position = new Vector2(Random.RandomRange(-4f, 4f), Random.RandomRange(-4f, 4f));
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
                    _colliderController.ColliderEnable(_spriteObjRef.GetComponent<CapsuleCollider2D>());
                    Movimentation();

                    break;
                case ENUM_PlayerState.PAUSED:
                    // Execution in PAUSED state
                    _colliderController.ColliderDisable(_spriteObjRef.GetComponent<CapsuleCollider2D>());
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
            // Create are detection
            _raycastHit = Physics2D.CircleCast(this.transform.position, sizeTargetArea, Vector2.right, sizeTargetArea, _playerLayer);
                        
            if (_raycastHit)
            {
                // Create linecast for player vision detection
                if (!Physics2D.Linecast(this.transform.position, _playerRef.transform.position, _wallLayer))
                {
                    // Move the enemy in player direction
                    Vector3 PlayerDirection = _playerRef.transform.position - this.transform.position;
                    float Angle = Mathf.Atan2(PlayerDirection.y, PlayerDirection.x) * Mathf.Rad2Deg;
                    _rigidBodyRef.rotation = Angle;
                    PlayerDirection.Normalize();


                    this._rigidBodyRef.MovePosition(this.transform.position + (PlayerDirection * moveSpeed * Time.deltaTime));

                }
                else
                {
                    // Stop the enemy
                    //MoveRandomize();
                }
            }
            else
            {
                // Stop the enemy
               // MoveRandomize();
            }
        }

        protected override void MapRotation()
        {
            // Rotate the enemy based on the center of the map
            _distanceCenterToMonster = Vector2.Distance(_pointOfRotation.transform.position, _playerRef.transform.position);
            this.transform.RotateAround(_pointOfRotation.transform.position, new Vector3(0f, 0f, 1f), (speedRotation * Time.deltaTime / _distanceCenterToMonster)  * -1f);
        }

        private void MoveRandomize()
        {
            if (_randomTime <= 0)
            {
                _randomTime = Random.RandomRange(3, 12);
                _tempDirection = new GameObject();
                _tempDirection.transform.position = new Vector2(Random.RandomRange(-2f, 2f), Random.RandomRange(-2f, 2f));
            }
            else
            {
                _randomTime -= Time.deltaTime;
            }            
        }
    }
}
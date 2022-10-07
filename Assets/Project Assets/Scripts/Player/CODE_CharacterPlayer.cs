using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CHARACTERS
{
    public class CODE_CharacterPlayer : ACODE_PauseClass, ICODE_CharacterMovimentation
    {
        // Attributes of class
        public float moveSpeed = 3;
        public float stepSpeedFactor = 0.05f;
        public float maxTime = 300;
        public int sec, min;
        public bool isDead;

        public float s;

        private float _speedFactorScale = 2;
        private float _timeBetweenSteps;
        private AudioSource _playerAudioSource;
        private Animator _playerAnimator;
        private CODE_PlayerColliderChecker _playerDeathChecker;
        private CODE_PlayerSpawn _playerSpawn;
        private USER_INTERFACE.CODE_PauseMenu _pauseMenu;

        private bool hasCalculatedGrid = true;
        private bool _hasAlreadyPlayed = false;

        private void Start()
        {
            switch(SceneManager.GetActiveScene().name)
            {
                case "MAP_Level1":
                    maxTime = 300f;

                    break;
                case "MAP_Level2":
                    maxTime = 500f;

                    break;
                case "MAP_Level3":
                    maxTime = 600f;

                    break;
            }

            Time.timeScale = 1f;
            // Spawn position
            this._playerSpawn = FindObjectOfType<CODE_PlayerSpawn>();
            this.transform.position = _playerSpawn.GetSpawnPosition().position;

            // Instantiating attributes
            this.moveSpeed *= this._speedFactorScale;
            this._timeBetweenSteps = this.moveSpeed * stepSpeedFactor;
            this._playerAudioSource = GetComponent<AudioSource>();
            this._playerAnimator = GetComponentInChildren<Animator>();
            this._playerCollider = GameObject.Find("PFB_Player").transform.GetChild(0).GetComponent<CapsuleCollider2D>();
            this._quetzaAudioSource = GameObject.Find("PFB_Quetzalcoatl").GetComponent<AudioSource>();
            playerState = ENUM_PlayerState.UNPAUSED;
            _soundManager = GameObject.Find("SoundManager").GetComponent<SONORIZATION.CODE_SoundManager>();
            _soundManager.FindSMAudioSource();
            _playerColliderChecker = this.transform.GetChild(2).GetComponent<CODE_PlayerColliderChecker>();
            _playerDeathChecker = this.transform.GetChild(0).GetComponent<CODE_PlayerColliderChecker>();
            _timeTextMenu = GameObject.Find("PFB_UI Canvas Timer").GetComponent<USER_INTERFACE.CODE_PauseMenu>();
            _pauseMenu = GameObject.Find("PFB_UI Canvas Levels").GetComponent<USER_INTERFACE.CODE_PauseMenu>();

            // Play OST_Level00 for the first time
            _soundManager.PlayOSTLevel();

            // Instantiating components
            _rigidBodyRef = GetComponent<Rigidbody2D>();
            _sceneMapRef = GameObject.Find("Grid");

        }

        private void Update()
        {
            if (!isDead && Time.timeScale != 0f)
            {
                DeathCondition();
                PlayerInputPause();
                if (_playerColliderChecker.isColliding)
                {
                    //ColliderException(); // Change to RB.Kinematic
                }
                else if (pressedPause) // Will change player state to UNPAUSED automatically after PAUSED + COLLIDING + INPUT
                {
                    pressedPause = false;
                    CheckStateAfterInput();
                }
                CheckState();
            }
        }
        /// <summary>
        /// Check the running state
        /// </summary>
        private void CheckState()
        {
            switch (playerState)
            {
                case ENUM_PlayerState.UNPAUSED:
                    TimeCounter();
                    if (!hasCalculatedGrid)
                    {
                        hasCalculatedGrid = true;
                    }
                    // Execution in UNPAUSED state
                    this._playerAnimator.SetBool("hasPaused", false);
                    Movimentation();

                    break;
                case ENUM_PlayerState.PAUSED:
                    hasCalculatedGrid = false;
                    // Execution in PAUSED state
                    _rigidBodyRef.velocity = Vector2.zero;
                    this._playerAnimator.SetBool("hasPaused", true);
                    MapRotation();

                    break;
            }
        }

        /// <summary>
        /// Move the player
        /// </summary>
        public void Movimentation()
        {
            Vector2 LookDirection;
            Quaternion SpriteAngle;

            // Get movimentation inputs
            float vertical_movement = Input.GetAxis("Vertical");
            float horizontal_movement = Input.GetAxis("Horizontal");
            LookDirection = new Vector2(horizontal_movement, vertical_movement);
            LookDirection.Normalize();

            // Play step sound effect
            SoundController();


            // Apply rotation in sprite based to inputs
            if (LookDirection != Vector2.zero)
            {
                SpriteAngle = Quaternion.LookRotation(Vector3.forward, LookDirection);
                this.transform.GetChild(0).transform.rotation = Quaternion.RotateTowards(this.transform.GetChild(0).transform.rotation, SpriteAngle, 10000 * Time.deltaTime);
            }



            // Apply movimentation
            _rigidBodyRef.velocity = (new Vector2(horizontal_movement, vertical_movement).normalized * moveSpeed);

            // Play animation
            AnimationController();
        }

        /// <summary>
        /// GameOver Condition
        /// </summary>
        public void DeathCondition()
        {
            if (_playerDeathChecker.deathChecker || _playerColliderChecker.deathChecker || maxTime < 1f)
            {
                _pauseMenu.DeathMenu();
                isDead = true;
                Time.timeScale = 0f;
                playerState = ENUM_PlayerState.UNPAUSED;
                _soundManager.PlayOSTGameOver();

            }
        }

        /// <summary>
        /// Play the sounds effects
        /// </summary>
        private void SoundController()
        {
            _timeBetweenSteps -= Time.deltaTime;
            if (_timeBetweenSteps < 0 && (_rigidBodyRef.velocity.x != 0 || _rigidBodyRef.velocity.y != 0))
            {
                _timeBetweenSteps = moveSpeed * stepSpeedFactor;
                _soundManager.PlayStepSound(_playerAudioSource);
            }
        }

        /// <summary>
        /// Play the animations
        /// </summary>
        /// 
        private void AnimationController()
        {
            if (Mathf.Abs(_rigidBodyRef.velocity.x) > 0.01f || Mathf.Abs(_rigidBodyRef.velocity.y) > 0.01f)
                this._playerAnimator.SetBool("isWalking", true);
            else
                this._playerAnimator.SetBool("isWalking", false);
        }

        private void TimeCounter()
        {
            // Timer Counter
            maxTime -= Time.deltaTime;

           

            // Timer create display
            int Sec, Hour, Min;
            Hour = (int)maxTime / 3600;
            Min = ((int)maxTime - Hour * 3600) / 60;
            Sec = ((int)maxTime - Hour * 3600 - Min * 60);

            // Set in public attributes
            sec = Sec;
            min = Min;

           
            secondsLeft = maxTime - Time.deltaTime;
            s = secondsLeft;

            if (secondsLeft <= 12f)
            {
                
                if (!_hasAlreadyPlayed)
                {
                    _soundManager.PlayOSTOutOfTime();
                    _hasAlreadyPlayed = true;
                }

                if(secondsLeft % 2f == 0)
                {
                    _timeTextMenu.WarningTextColor();
                }
                else
                {
                    _timeTextMenu.WarningTextColor();
                }
                
            }
            _timeTextMenu.RefreshTimeText(Min, Sec);
        }
    }
}









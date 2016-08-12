﻿using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {

        private GameObject statsScreen;
        private GameObject HpBar;
        private GameObject pausedScreen;
        private GameObject playerObj;
        private Stopwatch _stopWatch = new Stopwatch();
        private bool _gamePaused;
        private bool _firstTime = true;
        private float _timeScaleTemp;
        private Player _player;

        private static GameManager _instance;
        private bool _playerIsOnScene;

        public static GameManager Instance
        {
            get { return _instance; }
        }


        private void TogglePause()
        {
            if (!_gamePaused)
            {
                _gamePaused = true;
                _timeScaleTemp = Time.timeScale;
                Time.timeScale = 0;
            }
            else
            {
                _gamePaused = false;
                Time.timeScale = _timeScaleTemp;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        // Use this for initialization
        void Start () {
            DontDestroyOnLoad(this);
        }

        void OnLevelWasLoaded(int level)
        {
            if (statsScreen = GameObject.FindGameObjectWithTag ("StatScreen")) 
            {
                statsScreen.SetActive(false);
                print("Last Hp: " + PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_Hp"));
                print("Last Ink: " + PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_Ink"));
                print("Last Minute: " + PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_Minutes"));
                print("Last Second: " + PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_Seconds"));
            }
            
            if (pausedScreen = GameObject.FindGameObjectWithTag ("PausedScreen")) 
            {
                pausedScreen.SetActive(false);
                HpBar = GameObject.FindGameObjectWithTag("HPBarCanvas");	
            }
            if (playerObj = GameObject.FindGameObjectWithTag("Player"))
            {
                _playerIsOnScene = true;
                _player = playerObj.GetComponent<Player>();
            }
        }
	
        // Update is called once per frame
        void Update ()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                print(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (Input.GetKeyDown(KeyCode.P) && pausedScreen)
            {
                if (!_gamePaused)
                {
                    pausedScreen.SetActive(true);
                    print("PAUSED");
                }
                else
                {
                    pausedScreen.SetActive(false);
                    print("UNPAUSED");
                }
                TogglePause();
            }
            if(_playerIsOnScene)
                if (_player.FirstInput && _firstTime)
                {
                    _stopWatch.Start();
                    _firstTime = false;
                }
        }

        public void LevelEnd()
        {
            statsScreen.SetActive(true);
            var stat = statsScreen.GetComponent<StatScreen>();
            _stopWatch.Stop();
            stat.SetStats(_player, _stopWatch);
            HpBar.SetActive (false);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Hp", ((int)_player.Hp));
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Ink", (int)_player.InkCollected);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Minutes", _stopWatch.Elapsed.Minutes);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Seconds", _stopWatch.Elapsed.Seconds);
        }

        public bool IsPaused()
        {
            return _gamePaused;
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

namespace Mirrorfall.Core
{
    public class MirrorfallGameManager : MonoBehaviour
    {
        public static MirrorfallGameManager Instance { get; private set; }

        public enum GameState { Intro, Gameplay, Victory, Defeat }
        public GameState CurrentState { get; private set; }

        [Header("UI References")]
        [SerializeField] private GameObject introUI;
        [SerializeField] private GameObject gameplayUI;
        [SerializeField] private GameObject victoryUI;
        [SerializeField] private GameObject defeatUI;

        [Header("Game Settings")]
        [SerializeField] private int enemiesRemaining;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            SetState(GameState.Intro);
        }

        private void Update()
        {
            if (CurrentState == GameState.Intro && Input.anyKeyDown)
            {
                SetState(GameState.Gameplay);
            }
        }

        public void SetState(GameState newState)
        {
            CurrentState = newState;
            UpdateUI();

            switch (CurrentState)
            {
                case GameState.Intro:
                    Time.timeScale = 1f; // Or 0 if you want to pause
                    break;
                case GameState.Gameplay:
                    Time.timeScale = 1f;
                    // Unlock cursor if needed, start spawning logic if applicable
                    break;
                case GameState.Victory:
                case GameState.Defeat:
                    // Time.timeScale = 0f; // Optional: Stop game
                    // Show Cursor
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;
            }
        }

        private void UpdateUI()
        {
            if(introUI) introUI.SetActive(CurrentState == GameState.Intro);
            if(gameplayUI) gameplayUI.SetActive(CurrentState == GameState.Gameplay);
            if(victoryUI) victoryUI.SetActive(CurrentState == GameState.Victory);
            if(defeatUI) defeatUI.SetActive(CurrentState == GameState.Defeat);
        }

        public void RegisterEnemy()
        {
            enemiesRemaining++;
        }

        public void EnemyDefeated()
        {
            enemiesRemaining--;
            if (enemiesRemaining <= 0 && CurrentState == GameState.Gameplay)
            {
                SetState(GameState.Victory);
            }
        }

        public void PlayerDied()
        {
            if (CurrentState == GameState.Gameplay)
            {
                SetState(GameState.Defeat);
            }
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

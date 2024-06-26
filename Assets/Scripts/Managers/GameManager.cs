using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DungeonArena.CharacterControllers;

namespace DungeonArena.Managers {
    public class GameManager : Singleton<GameManager> {
        [Header("Game Settings")]
        [SerializeField] private int _maxHealth = 3;
        [SerializeField] private int _score = 0;
        [SerializeField] private int _scoreMultiplier = 1;

        private Player _player;
        private Coroutine _activePowerUpCoroutine;

        public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
        public int Score { get => _score; set => _score = value; }
        public int ScoreMultiplier { get => _scoreMultiplier; set => _scoreMultiplier = value; }
        public float PowerUpDuration { get; set; } = 0;
        public float PowerUpTimer { get; set; } = 0;
        public Player Player { get => _player; set => _player = value; }
        public List<Enemy> Enemies { get; set; } = new List<Enemy>();


        public void NewGame() {
            if (SceneManager.Instance.IsLoaded && SelectCharacterManager.Instance.SelectedPlayerCharacter != null) {
                // Instantiate the selected player .
                Player = Instantiate(SelectCharacterManager.Instance.SelectedPlayerCharacter);
                
                // Set the player's health to the maximum health.
                Player.Health = MaxHealth;
            }

            // Reset the score.
            Score = 0;

            // Reset the power-up timer.
            PowerUpDuration = 0;
            PowerUpTimer = 0;
            StopAllCoroutines();

            // Reset the score multiplier.
            ScoreMultiplier = 1;
        }

        public void EndGame() {
            // Destroy the player.
            if (Player != null) {
                Destroy(Player.gameObject);
            }

            // Destroy all enemies.
            foreach (Enemy enemy in Enemies) {
                if (enemy != null) {
                    Destroy(enemy.gameObject);
                }
            }

            // Clear the list of enemies.
            Enemies.Clear();
        }


        public void AddScore(int points) {
            // Add score to the total score.
            Score += points * ScoreMultiplier;
        }


        // Update the power-up timer.
        public void StartPowerUpTimer(float duration) {
            // Stop the power-up timer coroutine if it is already running.
            if (_activePowerUpCoroutine != null) {
                StopCoroutine(_activePowerUpCoroutine);
            }

            // Start the new power-up timer.
            _activePowerUpCoroutine = StartCoroutine(PowerUpTimerCoroutine(duration));
        }

        private IEnumerator PowerUpTimerCoroutine(float duration) {
            // Set the power-up duration.
            PowerUpDuration = duration;
            PowerUpTimer = duration;

            // Wait for the power-up duration.
            while (PowerUpTimer > 0) {
                yield return new WaitForSeconds(1);
                PowerUpTimer--;
            }

            // Reset the power-up duration.
            PowerUpTimer = 0;
            yield break;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePausePanel : MonoBehaviour
{
    [SerializeField] protected UIManager _uIManager;
    [SerializeField] protected TextMeshProUGUI _scoreField;

    private void OnEnable()
    {
        _scoreField.text = "Score: " + _uIManager.GameManager.PlayerManager.PlayerScore;
        Time.timeScale = 0.0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }

    public void QuitToStartMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("StartMenu");
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");
    }
}

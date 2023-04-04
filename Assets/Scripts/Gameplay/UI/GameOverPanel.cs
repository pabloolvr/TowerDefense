using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : GamePausePanel
{
    private void OnEnable()
    {
        _scoreField.text = "Score: " + _uIManager.GameManager.PlayerManager.PlayerScore;
        ScoreSaver.TrySaveScore(_uIManager.GameManager.PlayerManager.PlayerScore);
    }
}

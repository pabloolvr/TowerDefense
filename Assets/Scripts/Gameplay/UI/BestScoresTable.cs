using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScoresTable : MonoBehaviour
{
    [SerializeField] private Transform _scoresContainer;

    private TextMeshProUGUI[] _scoreFields;

    void Start()
    {
        _scoreFields = _scoresContainer.GetComponentsInChildren<TextMeshProUGUI>();
        int[] bestScores = ScoreSaver.LoadBestScores();

        for (int i = 0; i < _scoreFields.Length; i++)
        {
            if (bestScores[i] > 0)
            {
                _scoreFields[i].text = (i + 1) + " - " + bestScores[i];
            }
            else
            {
                _scoreFields[i].text = (i + 1) + " - ";
            }          
        }
    }
}

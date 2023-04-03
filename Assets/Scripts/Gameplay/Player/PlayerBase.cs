using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out Enemy enemy))
        {
            enemy.CallOnGoalReach();
            col.gameObject.SetActive(false);
        }
    }
}

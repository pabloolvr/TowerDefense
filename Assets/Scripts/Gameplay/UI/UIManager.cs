using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerHealthField;
    [SerializeField] private TextMeshProUGUI _playerGoldField;
    [SerializeField] private TextMeshProUGUI _playerScoreField;

    private Camera _camera;
    private int _layerMask;

    // Start is called before the first frame update
    private void Start()
    {
        _camera = Camera.main;
        _layerMask = 1 << 6;
    }

    private void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow, 999);
            
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, _layerMask))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log($"Hit {hit.transform.name}");
            }
        }
        */
    }

    public void UpdateGoldAmount(int amount)
    {
        _playerGoldField.text = "Gold: " + amount.ToString();
    }

    public void UpdateHealthAmount(int amount)
    {
        _playerHealthField.text = "Health: " + amount.ToString();
    }

    public void UpdateScore(int amount)
    {
        _playerScoreField.text = "Score: " + amount.ToString();
    }
}

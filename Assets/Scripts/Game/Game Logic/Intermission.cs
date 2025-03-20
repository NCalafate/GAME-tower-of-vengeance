using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intermission : MonoBehaviour
{
    [SerializeField] ScriptableInteger _turn;
    [SerializeField] ScriptableInteger _intermissionEnemy;

    private GameObject _panel;

    public static bool inIntermission = false;

    void Start()
    {
        _panel = gameObject.transform.GetChild(0).gameObject;

        _turn.AddListener(TurnEnded);
        _intermissionEnemy.AddListener(TurnIntermissionEnemyEnded);
    }

    private void TurnEnded()
    {
        if(_turn.Value != 1)
        {
            _panel.SetActive(true);
            inIntermission = true;
        }
    }

    private void TurnIntermissionEnemyEnded()
    {
        if(_intermissionEnemy.Value != 1)
        {
            _panel.SetActive(false);
            inIntermission = false;
        }
    }
}

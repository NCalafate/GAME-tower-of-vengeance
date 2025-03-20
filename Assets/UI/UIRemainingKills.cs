using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRemainingKills : MonoBehaviour
{
    [SerializeField] ScriptableInteger _aliveEnemies;

    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();

        _text.text = _aliveEnemies.Value.ToString();
        _aliveEnemies.AddListener(UpdateAlive);
    }

    private void UpdateAlive()
    {
        _text.text = _aliveEnemies.Value.ToString();
    }
}

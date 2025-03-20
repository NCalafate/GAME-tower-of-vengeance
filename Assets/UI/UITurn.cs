using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITurn : MonoBehaviour
{
    [SerializeField] ScriptableInteger _turn;
    [SerializeField] ScriptableInteger _intermissionEnemy;

    private TMP_Text _turnText;
    private Button _button;

    void Start()
    {
        _intermissionEnemy.AddListener(UpdateTurn);
        _turnText = GetComponentInChildren<TMP_Text>();
        _button = GetComponentInChildren<Button>();

        _button.onClick.AddListener(NextTurn);
    }

    private void UpdateTurn()
    {
        _turnText.text = _intermissionEnemy.Value.ToString();
    }

    private void NextTurn()
    {
        _turn.Value++;
    }
}

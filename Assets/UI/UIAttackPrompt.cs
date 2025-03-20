using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAttackPrompt : MonoBehaviour
{
    private TextMeshProUGUI[] _text;
    private Slider _slider;
    [SerializeField] private ScriptableInteger _lock;
    private string _attackName;
    private float _accuracy, _damage, _hp, _hpMax;
    private Action<RaycastHit[], float> _action;
    private RaycastHit[] _hits;

    private void Awake()
    {
        _lock.Value = 1;
        _text = GetComponentsInChildren<TextMeshProUGUI>();
        _slider = GetComponentInChildren<Slider>();
        gameObject.SetActive(false);
    }

    private void Start()
    {
        Time.timeScale = 0.0f;
    }

    public void Show(RaycastHit[] hits, string name, float accuracy, float damage, float hp, float hpMax,
        Action<RaycastHit[], float> action)
    {
        _attackName = name;
        _hits = hits;
        _accuracy = Mathf.Round(accuracy * 100 * 10.0f * 0.1f);
        _damage = Mathf.Round(damage * 10.0f) * 0.1f;
        _hp = Mathf.Round(hp * 10.0f) * 0.1f;
        _hpMax = Mathf.Round(hpMax * 10.0f) * 0.1f;
        _action = action;
        Refresh();
    }

    public void Cancel()
    {
        _lock.Value = 0;
        Time.timeScale = 1.0f;
        Destroy(gameObject);
    }

    public void Confirm()
    {
        _lock.Value = 0;
        Time.timeScale = 1.0f;
        _action.Invoke(_hits, _accuracy);
        Destroy(gameObject);
    }

    private void Refresh()
    {
        _text[0].text = _attackName;
        _text[1].text = " " + _accuracy + "%";
        _text[2].text = " " + _damage;
        _text[3].text = _hp + " / " + _hpMax;
        _slider.value = _hp / _hpMax;
        gameObject.SetActive(true);
    }
}
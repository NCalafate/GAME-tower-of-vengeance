using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private ScriptableInteger _turn;
    [SerializeField] private ScriptableInteger _intermission;
    [SerializeField] private ScriptableInteger _attackSelected;
    [SerializeField] private AttackAccuracyScriptableObject attackAccuracy;

    [SerializeField] private SelectedObjectScriptable _selected;
    [SerializeField] private LayerMask _layersToHit;
    [SerializeField] private GameObject _prefabSelectionCircle, _prefabAttackPrompt;
    private Action<RaycastHit[], float> _action;
    private AttackScriptableObject _attack;
    private GameObject _selectionCircle, _attackUI;


    private void Awake()
    {
        _selected.AddListener(handleAttackUI);
        _selectionCircle = Instantiate(_prefabSelectionCircle, new Vector3(0, 0, 0), Quaternion.identity);
        _attackUI = GameObject.Find("UI Attack").GameObject();
        _attackUI.SetActive(false);
        _selectionCircle.SetActive(false);
        _turn.AddListener(HandleTurn);
        _intermission.AddListener(HandleTurn);
    }

    private void Update()
    {
        var selected = _selected.CurrentlySelected;
        if (selected is null)
        {
            _attackUI.SetActive(false);
            return;
        }

        ;

        HandleSelectionCircle();
        // Skip if Attack is not selected or if Click is not detected
        if (_attackSelected.Value == 0 || !Input.GetMouseButtonDown(0)) return;

        var target = GetTarget();
        // Skip if target is not Humanoid
        if (1 << target.layer != attackAccuracy.HumanoidLayer.value) return;

        PrepareAttack(target);
    }


    private void HandleSelectionCircle()
    {
        _attackUI.SetActive(true);

        var selected = _selected.CurrentlySelected;

        if (selected is null) _attackSelected.Value = 0;
        _selectionCircle.SetActive(_attackSelected.Value != 0);

        if (_attackSelected.Value == 0) return;
        var scaleY = _selectionCircle.transform.localScale.y;
        _selectionCircle.transform.position = selected.transform.position;
        _selectionCircle.transform.localScale = new Vector3(_attack.Range * 2, scaleY, _attack.Range * 2);
    }

    private GameObject GetTarget()
    {
        var cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(cameraRay, out var hitTarget);
        return hitTarget.collider.gameObject;
    }

    private void PrepareAttack(GameObject target)
    {
        // Initialize Ray Parameters
        var posStart = _selected.CurrentlySelected.transform.position;
        var posEnd = target.transform.position;
        var direction = (posEnd - posStart).normalized;
        var distance = Vector3.Distance(posEnd, posStart);

        // Skip if "Distance" > "Attack Range"
        if (distance > _attack.Range) return;

        // Fire ray from "Selected" to "Target"
        var hits = Physics.RaycastAll(new Ray(posStart, direction), distance, _layersToHit);

        // Skip if no hits
        if (!(hits.Length > 0)) return;

        // Sort hits by distance
        Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));

        // Initialize "Attack Prompt" Parameters

        var accuracy = CalcAccuracy(hits);
        var targetStats = target.GetComponent<Enemy>().InstanceStatistics;
        var targetHP = targetStats.Health;
        var targetHPMax = targetStats.MaxHealth;


        // Show "Attack Prompt"
        AttackPrompt(hits, _attack.Name, accuracy, _attack.Damage, targetHP, targetHPMax, _action);
        _attackUI.SetActive(false);
    }


    private void AttackPrompt(RaycastHit[] hits, string name, float accuracy, float damage, float hp, float hpMax,
        Action<RaycastHit[], float> action)
    {
        // Instantiate "Prompt" --> Place GameObject in Canvas --> Get Script --> Execute Show() with parameters
        var prompt = Instantiate(_prefabAttackPrompt, new Vector3(0, 0, 0), Quaternion.identity);
        prompt.transform.SetParent(GameObject.Find("Canvas").transform, false);
        var promptScript = prompt.GetComponent<UIAttackPrompt>();
        promptScript.Show(hits, name, accuracy, damage, hp, hpMax, action);
    }

    private float CalcAccuracy(RaycastHit[] hits)
    {
        var accuracy = 1.0f;
        if (hits.Length == 1) return accuracy;
        for (var i = 0; i < hits.Length - 1; i++)
        {
            LayerMask objectLayer = 1 << hits[i].collider.gameObject.layer;
            if (objectLayer == attackAccuracy.HumanoidLayer.value) accuracy -= attackAccuracy.Humanoid;
            if (objectLayer == attackAccuracy.ObstaclesLayer.value) accuracy -= attackAccuracy.Obstacles;
            if (objectLayer == attackAccuracy.SolidLayer.value) accuracy -= attackAccuracy.Solid;
            if (accuracy == 0.0f) break;
        }

        return accuracy;
    }

    private void handleAttackUI()
    {
        var selected = _selected.CurrentlySelected;
        if (selected is null) return;

        var attackSet = selected.GetComponent<AttackInterface>().AttackSet();
        var actions = selected.GetComponent<AttackInterface>().Actions();

        // TODO
        // Implement disable attacks after attacks.
        // if (attackSet.Attacks == 0) _attackUI.SetActive(false);

        if (!_attackUI) return;


        var icons = _attackUI.GetComponentsInChildren<RawImage>();
        icons[1].texture = attackSet.AttackA.Icon;
        icons[2].texture = attackSet.AttackB.Icon;
        icons[3].texture = attackSet.AttackC.Icon;

        var text = _attackUI.GetComponentsInChildren<TextMeshProUGUI>();
        text[0].text = attackSet.AttackA.Name;
        text[1].text = attackSet.AttackB.Name;
        text[2].text = attackSet.AttackC.Name;

        var buttons = _attackUI.GetComponentsInChildren<Button>();
        if (buttons[0])
            buttons[0].onClick.AddListener(delegate
            {
                _action = actions[0];
                _attackSelected.Value = 1;
                _attack = attackSet.AttackA;
            });
        buttons[1].onClick.AddListener(delegate
        {
            _action = actions[1];
            _attackSelected.Value = 1;
            _attack = attackSet.AttackB;
        });
        buttons[2].onClick.AddListener(delegate
        {
            _action = actions[2];
            _attackSelected.Value = 1;
            _attack = attackSet.AttackC;
        });
    }

    protected void HandleTurn()
    {
        _attackSelected.Value = 0;
    }
}
using Humanoids;
using TMPro;
using UnityEngine;

public class UIHumanoidStatistics : MonoBehaviour
{
    [SerializeField] private SelectedObjectScriptable _selectedObject;
    [SerializeField] private ScriptableInteger _turn;
    private TMP_Text _attackRange;
    private TMP_Text _defense;
    private TMP_Text _dexterity;
    private TMP_Text _healthPoints;

    private HumanoidStatistics _humanoidStatistics;
    private TMP_Text _movementRange;

    private TMP_Text _name;

    private GameObject _panel;
    private TMP_Text _strength;

    private void Start()
    {
        _selectedObject.AddListener(UpdatePanel);

        _turn.AddListener(HidePanel);

        _panel = transform.GetChild(0).gameObject;

        _name = _panel.transform.Find("Name").GetComponent<TMP_Text>();
        _healthPoints = _panel.transform.Find("Health").gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        _strength = _panel.transform.Find("Strength").gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        _defense = _panel.transform.Find("Defense").gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        _dexterity = _panel.transform.Find("Dexterity").gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        _movementRange = _panel.transform.Find("Movement Range").gameObject.transform.GetChild(0)
            .GetComponent<TMP_Text>();
        _attackRange = _panel.transform.Find("Attack Range").gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
    }

    private void UpdatePanel()
    {
        if (_selectedObject.CurrentlySelected != null)
        {
            var _selectedHumanoid = _selectedObject.CurrentlySelected.gameObject.GetComponent<Humanoid>();

            if (_humanoidStatistics != null)
            {
                _humanoidStatistics.RemoveHealthListener(UpdateHealth);
                _humanoidStatistics.RemoveStrengthListener(UpdateStrength);
                _humanoidStatistics.RemoveDefenseListener(UpdateDefense);
                _humanoidStatistics.RemoveDexterityListener(UpdateDexterity);
                _humanoidStatistics.RemoveMovementRangeListener(UpdateMovementRange);
                _humanoidStatistics.RemoveAttackRangeListener(UpdateAttackRange);
            }

            _humanoidStatistics = _selectedHumanoid.GetStatistics();

            _name.text = _humanoidStatistics.Name;
            _healthPoints.text = _humanoidStatistics.Health.ToString();
            _strength.text = _humanoidStatistics.Strength.ToString();
            _defense.text = _humanoidStatistics.Defense.ToString();
            _dexterity.text = _humanoidStatistics.Dexterity.ToString();
            _movementRange.text = _humanoidStatistics.MovementRange.ToString();
            _attackRange.text = _humanoidStatistics.AttackRange.ToString();

            _humanoidStatistics.AddHealthListener(UpdateHealth);
            _humanoidStatistics.AddStrengthListener(UpdateStrength);
            _humanoidStatistics.AddDefenseListener(UpdateDefense);
            _humanoidStatistics.AddDexterityListener(UpdateDexterity);
            _humanoidStatistics.AddMovementRangeListener(UpdateMovementRange);
            _humanoidStatistics.AddAttackRangeListener(UpdateAttackRange);
        }
        else
        {
            HidePanel();
        }
    }

    private void UpdateHealth()
    {
        _healthPoints.text = _humanoidStatistics.Health.ToString();
    }

    private void UpdateStrength()
    {
        _strength.text = _humanoidStatistics.Strength.ToString();
    }

    private void UpdateDefense()
    {
        _defense.text = _humanoidStatistics.Defense.ToString();
    }

    private void UpdateDexterity()
    {
        _dexterity.text = _humanoidStatistics.Dexterity.ToString();
    }

    private void UpdateMovementRange()
    {
        _movementRange.text = _humanoidStatistics.MovementRange <= 0
            ? "0"
            : ((int)_humanoidStatistics.MovementRange).ToString();
    }

    private void UpdateAttackRange()
    {
        _attackRange.text = _humanoidStatistics.AttackRange.ToString();
    }

    private void HidePanel()
    {
        // TODO: Hide the panel.
    }
}
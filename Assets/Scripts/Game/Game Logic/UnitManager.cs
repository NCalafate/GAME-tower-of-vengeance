using Humanoids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] ScriptableInteger _alivePlayers;
    [SerializeField] ScriptableInteger _aliveEnemies;
    [SerializeField] SelectedObjectScriptable _selectedObject;
    [SerializeField] List<Humanoid> _playableCharacters = new List<Humanoid>();
    [SerializeField] GameObject _humanoidCardHolder;
    [SerializeField] GameObject _playerCardTemplate;
    [SerializeField] GameObject _portal;

    private List<Humanoid> _alreadyAssociated;

    void Start()
    {
        _alreadyAssociated = new List<Humanoid>();
        _alivePlayers.AddListener(DiedPlayer);
        _aliveEnemies.AddListener(DiedEnemy);
    }

    private void Update()
    {
        foreach (Humanoid character in _playableCharacters)
        {
            if(!_alreadyAssociated.Contains(character))
            {
                _alreadyAssociated.Add(character);

                Link(character);
            }
        }
    }

    /// <summary>
    /// Links a humanoid to an UI card.
    /// </summary>
    private void Link(Humanoid humanoid)
    {
        if (!_playableCharacters.Contains(humanoid))
        {
            _playableCharacters.Add(humanoid);
        }

        int positionX = 0;
        int positionY = 18;
        int positionZ = 0;
        if (_playableCharacters.IndexOf(humanoid) == 0)
        {
            positionX = 40;
        }
        else
        {
            positionX = 40 + (80 * _playableCharacters.IndexOf(humanoid));
        }

        Vector3 elementPosition = new Vector3(positionX, positionY, positionZ);

        GameObject card = Instantiate(_playerCardTemplate);
        card.transform.SetParent(_humanoidCardHolder.transform, false);

        card.transform.localScale = _playerCardTemplate.transform.localScale;
        card.transform.localPosition = elementPosition;  

        UIHumanoidCard humanoidCard = card.GetComponent<UIHumanoidCard>();
        humanoidCard.AssociateSelectionController(_selectedObject);
        humanoidCard.AssociateCharacter(humanoid);
    }

    private void DiedPlayer()
    {
        if(_alivePlayers.Value <= 0)
        {
            _portal.GetComponent<LoadScene>().Loss();
        }
    }

    private void DiedEnemy()
    {
        if (_aliveEnemies.Value <= 0)
        {
            _portal.GetComponent<LoadScene>().Win();
        }
    }
}

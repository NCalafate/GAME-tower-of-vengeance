using System.Collections;
using System.Collections.Generic;
using Humanoids;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIHumanoidCard : MonoBehaviour
{
    private GameObject _cameraRig;
    private SelectedObjectScriptable _selectionController;
    private HumanoidStatistics _characterInstance;
    private GameObject _character;
    private Image _image;
    private TMP_Text _killsText;
    private Slider _healthSlider;
    private Button _button;
    private RawImage _dead;
    private AudioSource _audioSource;

    private void Start()
    {
        _cameraRig = GameObject.Find("Camera Rig");
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(_character.GetComponent<Humanoid>().IsDead())
        {
            _dead.gameObject.SetActive(true);
        }
    }

    public void AssociateCharacter(Humanoid humanoid)
    {
        _character = humanoid.gameObject;
        _characterInstance = humanoid.GetStatistics();

        _image = transform.GetChild(0).GetComponent<Image>();
        // _killsText = transform.GetChild(1).GetComponent<TMP_Text>();
        _healthSlider = transform.GetChild(1).GetComponent<Slider>();
        _button = transform.GetChild(2).GetComponent<Button>();
        _dead = transform.GetChild(3).GetComponent<RawImage>();

        _button.onClick.AddListener(CardClick);

        Initialize();
    }

    public void AssociateSelectionController(SelectedObjectScriptable controller)
    {
        _selectionController = controller;
    }

    private void CardClick()
    {
        if (!_character.GetComponent<Humanoid>().IsDead())
        {
            _selectionController.Deselect();
            _selectionController.Select(_character);
            MoveCamera(_character.transform.position);
            _audioSource.Play();
        }
    }

    private void Initialize()
    {
        _image.sprite = ImageMapper.RetrieveHumanoidImageFromClass(_character.GetComponent<Humanoid>().GetClass());
        // _killsText.text = _characterInstance.Kills.ToString();

        _healthSlider.minValue = 0;
        _healthSlider.maxValue = _characterInstance.MaxHealth;
        _healthSlider.value = _characterInstance.Health;

        _characterInstance.AddHealthListener(HealthBarCurrentHealthUpdate);
        _characterInstance.AddMaxHealthListener(HealthBarMaxHealthUpdate);
    }

    private void HealthBarCurrentHealthUpdate()
    {
        _healthSlider.value = _characterInstance.Health;
    }

    private void HealthBarMaxHealthUpdate()
    {
        _healthSlider.maxValue = _characterInstance.MaxHealth;
    }

    private void MoveCamera(Vector3 position)
    {
        _cameraRig.GetComponent<CameraController>().SnapMove(position);
    }
}


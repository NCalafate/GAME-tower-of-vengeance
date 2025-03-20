using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITitleScreen : MonoBehaviour
{
    [SerializeField] private GameSettingsSO _gameSettingsSo;

    private AudioSource[] _audioSource;
    [SerializeField] private AudioClip _audioClipMusic;
    [SerializeField] private AudioClip _audioClipSfx;

    private RawImage _background;
    private readonly Color _filter = new(0.0f, 0.0f, 0.0f, 0.4f);

    private GameObject _mainMenu, _optionsMenu;
    private Slider[] _sliders;
    private Button _logger;


    private void Awake()
    {
        _audioSource = GetComponents<AudioSource>();
        _gameSettingsSo.AddListenerMusic(SetMusicVolume);
        _gameSettingsSo.AddListenerSfx(SetSfxVolume);
        _background = GameObject.Find("Background Filter").GetComponent<RawImage>();
        _mainMenu = GameObject.Find("Main Menu");
        _optionsMenu = GameObject.Find("Options Menu");
        _sliders = _optionsMenu.GetComponentsInChildren<Slider>();
        _logger = _optionsMenu.transform.GetChild(5).GetComponent<Button>();
        _logger.onClick.AddListener(ResetLogger);
    }

    private void Start()
    {
        _audioSource[0].clip = _audioClipMusic;
        _audioSource[0].Play();
        SetMusicVolume();
        SetSfxVolume();
        GotoMainMenu();
    }

    private void Update()
    {
        // Skip slider update if not in options menu
        if (!_optionsMenu.activeSelf) return;
        _gameSettingsSo.Music = _sliders[0].value;
        _gameSettingsSo.Sfx = _sliders[1].value;
    }

    private void ResetLogger()
    {
        FileActor actor = new FileActor();
        actor.DeleteAll();
    }

    public void GotoMainMenu()
    {
        SetBackgroundFilter(false);
        _mainMenu.SetActive(true);
        _optionsMenu.SetActive(false);
    }

    public void GotoNewGame()
    {
        _audioSource[1].PlayOneShot(_audioClipSfx);
        SceneManager.LoadScene(1);
        
    }

    public void GotoLoadGame()
    {
        if(PlayerPrefs.GetInt("Save") > 0 ){
            _audioSource[1].PlayOneShot(_audioClipSfx);
            SceneManager.LoadScene(PlayerPrefs.GetInt("Save")); 
        }
    }

    public void GotoOptions()
    {
        _audioSource[1].PlayOneShot(_audioClipSfx);
        SetBackgroundFilter(true);
        _mainMenu.SetActive(false);
        _optionsMenu.SetActive(true);
        _sliders[0].value = _gameSettingsSo.Music;
        _sliders[1].value = _gameSettingsSo.Sfx;
    }

    public void GotoOptionsBack()
    {
        _audioSource[1].PlayOneShot(_audioClipSfx);
        GotoMainMenu();
    }

    public void GotoExit()
    {
        _audioSource[1].PlayOneShot(_audioClipSfx);
        Application.Quit();
    }

    private void SetMusicVolume()
    {
        _audioSource[0].volume = _gameSettingsSo.Music;
    }

    private void SetSfxVolume()
    {
        _audioSource[1].volume = _gameSettingsSo.Sfx;
    }

    private void SetBackgroundFilter(bool inMenu)
    {
        var transparency = _filter;
        if (inMenu) transparency.a += 0.5f;
        _background.color = transparency;
    }

    public void OnDisable()
    {
        _gameSettingsSo.RemoveListenerMusic(SetMusicVolume);
        _gameSettingsSo.RemoveListenerSfx(SetSfxVolume);
    }
}
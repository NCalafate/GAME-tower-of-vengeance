using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPauseScreen : MonoBehaviour
{
    [SerializeField] private GameSettingsSO _gameSettingsSo;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClipSfx;

    private RawImage _background;
    private readonly Color _filter = new(0.0f, 0.0f, 0.0f, 0.4f);

    private GameObject _pauseMenu, _optionsMenu;
    private Slider[] _sliders;



    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _gameSettingsSo.AddListenerSfx(SetSfxVolume);
        _background = GameObject.Find("Background Filter").GetComponent<RawImage>();
        _pauseMenu = GameObject.Find("Pause Menu");
        _optionsMenu = GameObject.Find("Options Menu");
        _sliders = _optionsMenu.GetComponentsInChildren<Slider>();
        Time.timeScale = 0.0f;
        SetSfxVolume();
        GotoPauseMenu();
    }

    private void Update()
    {
        // Skip slider update if not in options menu
        if (!_optionsMenu.activeSelf) return;
        _gameSettingsSo.Music = _sliders[0].value;
        _gameSettingsSo.Sfx = _sliders[1].value;
    }

    public void GotoPauseMenu()
    {
        SetBackgroundFilter(false);
        _pauseMenu.SetActive(true);
        _optionsMenu.SetActive(false);
    }

    public void Continue()
    {
        Time.timeScale = 1.0f;
        Destroy(gameObject);
    }

    public void GotoOptions()
    {
        if (!_audioSource) return;
        _audioSource.PlayOneShot(_audioClipSfx);
        SetBackgroundFilter(true);
        _pauseMenu.SetActive(false);
        _optionsMenu.SetActive(true);
        _sliders[0].value = _gameSettingsSo.Music;
        _sliders[1].value = _gameSettingsSo.Sfx;
    }

    public void GotoOptionsBack()
    {
        _audioSource.PlayOneShot(_audioClipSfx);
        GotoPauseMenu();
    }

    public void GotoExit()
    {
        Time.timeScale = 1.0f;
        _audioSource.PlayOneShot(_audioClipSfx);
        SceneManager.LoadScene(0);
    }

    private void SetSfxVolume()
    {
        if (_audioSource != null)
        {
            _audioSource.volume = _gameSettingsSo.Sfx;
        }
    }

    private void SetBackgroundFilter(bool inMenu)
    {
        var transparency = _filter;
        if (inMenu) transparency.a += 0.5f;
        _background.color = transparency;
    }
}
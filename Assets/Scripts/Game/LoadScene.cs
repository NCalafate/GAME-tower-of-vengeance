using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    
    [SerializeField] private int durationLoadScreen;
    [SerializeField] private GameObject loadScreenPre;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pauseScreenPrefab;

    [SerializeField] private ScriptableInteger _turn;
    [SerializeField] private ScriptableInteger _intermission;
    [SerializeField] private ScriptableInteger _intermissionEnemy;
    [SerializeField] private ScriptableInteger _alivePlayers;
    [SerializeField] private ScriptableInteger _aliveEnemies;
    [SerializeField] private CommandsScriptable _commandList;
    [SerializeField] private CommandsScriptable _commandListEnemy;
    [SerializeField] private SelectedObjectScriptable _selected;

    private GameObject pauseScreenInstance;
    private int sceneIndex;
    private bool isPaused = false;

    private void Start()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        sceneIndex = activeScene.buildIndex;
        PlayerPrefs.SetInt("Save", sceneIndex);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Win();
        }
    }

     public void Win()
    {
        StartCoroutine(NextLevel());
    }

    private IEnumerator NextLevel()
    {
        GameObject loadingScreen = Instantiate(loadScreenPre, Vector3.zero, Quaternion.identity);

        yield return new WaitForSeconds(durationLoadScreen);

        WipeInformation();

        PlayerPrefs.SetInt("Save", sceneIndex + 1);
        SceneManager.LoadScene(sceneIndex + 1);
    }

    public void Loss()
    {
        StartCoroutine(LossGame());
    }
    
    private IEnumerator LossGame()
    {
        GameObject loadingScreen = Instantiate(gameOver, Vector3.zero, Quaternion.identity);

        yield return new WaitForSeconds(durationLoadScreen);

        WipeInformation();

        PlayerPrefs.SetInt("Save", 0);
        SceneManager.LoadScene(0);
    }

    private void PauseGame()
    {
        if (pauseScreenInstance == null)
        {
            pauseScreenInstance = Instantiate(pauseScreenPrefab, Vector3.zero, Quaternion.identity);
        }
        isPaused = true;
        Time.timeScale = 0.0f;
    }
    
    private void ResumeGame()
    {
        if (pauseScreenInstance != null)
        {
            Destroy(pauseScreenInstance);
        }

        isPaused = false;
        Time.timeScale = 1.0f;
    }

    private void WipeInformation()
    {
        _turn.StartingInteger();
        _intermission.StartingInteger();
        _intermissionEnemy.StartingInteger();
        _aliveEnemies.StartingInteger();
        _alivePlayers.StartingInteger();
        _commandList.StartingList();
        _commandListEnemy.StartingList();
        _selected.InitialSelection();
        Intermission.inIntermission = false;
    }
}

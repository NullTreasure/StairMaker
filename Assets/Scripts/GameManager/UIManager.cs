using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject nextLevel;
    [SerializeField] private GameObject restart;
    private void Start()
    {
        nextLevel.SetActive(false);
        restart.SetActive(false); 
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex +1;
        if (nextSceneIndex > SceneManager.sceneCount - 1)
        {
            nextSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    public void endGameButton(bool isWin)
    {
        restart.SetActive(true);
        nextLevel.SetActive(isWin);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneHandler
{

    private Vector3 defaultGravity = new Vector3(0,-20f,0);
    void Start() {
        
    }

    public void ReloadScene()
    {
        ResetAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(int buildIndex)
    {
        ResetAll();
        SceneManager.LoadScene(buildIndex);
    }

    private void ResetAll()
    {
        Physics.gravity = defaultGravity;
        UIHandler.UnPauseGame();
    }
}

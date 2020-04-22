using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    Button m_ResumeGame,m_newGame,m_credits,m_quit;
    [SerializeField]
    GameObject m_CreditView;
    bool m_creditRunning;

    private static bool paused;
    // Start is called before the first frame update
    void Start()
    {
        // Game will always be paused in this screen
        paused = true;
        if(m_ResumeGame != null) m_ResumeGame.onClick.AddListener(Resume);
        m_newGame.onClick.AddListener(LoadGame);
        if(m_credits != null) m_credits.onClick.AddListener(ShowCredit);
        m_quit.onClick.AddListener(QuitGame);
        //Credit won't be shown at start
        m_creditRunning = false;
        if(m_CreditView != null)m_CreditView.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_creditRunning)
            TakeInput();
    }

    void LoadGame()
    {
        SceneHandler sh =new SceneHandler();
        sh.LoadScene(1);
    }

    void ShowCredit()
    {
        m_CreditView.SetActive(true);
        m_creditRunning = true;
    }

    void TakeInput()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            m_creditRunning = false;
            m_CreditView.SetActive(false);
        }
    }

    void Resume()
    {
        UnPauseGame();
        // Deactivate the object
        gameObject.SetActive(false);
    }

    void QuitGame()
    {
        Application.Quit();
    }

    public static bool isPaused()
    {
        
        return paused;
    }

    public static void PauseGame()
    {
        paused = true;
    }

    public static void UnPauseGame()
    {
        paused = false;
    }
    
}

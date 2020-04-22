using UnityEngine;
using UnityEngine.UI;


public class Scorer : MonoBehaviour
{
    private int m_score,m_highScore;
    private string m_text,m_hText,fpsText;

    private Text m_TElement;

    // Start is called before the first frame update
    void Start()
    {
        m_score = 0;
        m_highScore = SBClass.GetHighScore();
        m_text = "Score: ";
        m_hText = "\nHighScore: ";
        fpsText = "\nFPS: ";
        m_TElement = GetComponent<Text>();
        print("Loaded");
        
    }

    // Update is called once per frame
    void Update()
    {
        if( m_score > m_highScore) m_highScore = m_score;
        m_TElement.text = m_text + m_score + m_hText + m_highScore + fpsText + (int)(1/Time.deltaTime) ;
    }

    public void SetScore()
    {
        m_score++;
    }

    public int GetScore()
    {
        return m_score;
    }

    public void SetHighScore()
    {
        SBClass.SetHighScore(m_score);
        //print("Setting high score: " + m_score);
        m_highScore = SBClass.GetHighScore();
    }
}

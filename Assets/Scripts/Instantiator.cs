using UnityEngine;

public class Instantiator : MonoBehaviour
{
    private GameObject m_bamboo;
    public GameObject instantiator;
    // Start is called before the first frame update
    private float m_time_counter = 0.0f;
    [SerializeField]
    private float m_time_dif=2.0f;
    [SerializeField]
    private GameObject m_bird;
    [SerializeField]
    private Scorer m_Scorer;
    private float m_gameSpeed;
    private bool paused;

    void Start()
    {
        m_gameSpeed = 1.00f;
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        // No action if game is paused
        if(UIHandler.isPaused()) return;

        m_time_counter += Time.deltaTime;
        if(m_time_counter > m_time_dif/m_gameSpeed)
        {
            m_time_counter = 0.0f;
            m_bamboo = GameObject.Instantiate(instantiator);

            float ylen = Random.Range(-1.7f,1.7f);
            m_bamboo.transform.position = m_bamboo.transform.position + new Vector3(0,ylen,0);

            BambooMotion bm = m_bamboo.GetComponent<BambooMotion>();
            bm.SetBird(m_bird);
            bm.SetScorer(m_Scorer);
        }

        if(m_Scorer.GetScore() == 20) m_gameSpeed = 1.15f;
        if( m_Scorer.GetScore() == 50 ) m_gameSpeed = 1.25f;

    }
}

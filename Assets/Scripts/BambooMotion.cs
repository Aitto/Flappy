using UnityEngine;


public class BambooMotion : MonoBehaviour
{
    private Vector3 m_dir = new Vector3(-1,0,0),newGravity;
    [SerializeField][Range(1,10)]
    private float m_velocity = 5.0f;
    [SerializeField]
    private AudioClip m_succesAudio;
    private AudioSource m_as;
    private GameObject m_bird;
    private Scorer scorer;
    private bool crossed,paused;
    private BirdMotion m_birdMotion;
    private float m_gameSpeed, ltime;
    private bool lerped;


    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        m_gameSpeed = 1.00f;
        crossed = false;
        m_as = GetComponent<AudioSource>();
        Physics.gravity = new Vector3(0,-20f,0f);
        
        print("Gravity: " + Physics.gravity);

        ltime = 0.0f;
        lerped = true;
    }

    // Update is called once per frame
    void Update()
    {
        // No action if game is paused
        if(UIHandler.isPaused()) return;


        // Return if bird is dead
        if(m_birdMotion.IsCollided()) return;

        if(this.transform.position.x < -30.0f)
        {
            Destroy(gameObject);
        }

        ChangePos();
        if(!crossed)
            IScore();

        if(scorer.GetScore() == 20 ) 
        {
            m_gameSpeed = 1.15f;
            lerped = false;
            newGravity = new Vector3(0,-25f,0);
            ltime = 0.0f;
            //Physics.gravity = new Vector3(0,-25f,0);
            //Physics.gravity = Vector3.Lerp(Physics.gravity, new Vector3(0,-25f,0),3f);
            m_birdMotion.SetGameSpeed(400);
        }
        if(scorer.GetScore() == 50 )
        {
            m_gameSpeed = 1.25f;
            lerped = false;
            newGravity = new Vector3(0,-30f,0);
            ltime = 0.0f;
            //Physics.gravity = new Vector3(0,-30f,0);
            //Physics.gravity = Vector3.Lerp(Physics.gravity, new Vector3(0,-30f,0),3f);
            m_birdMotion.SetGameSpeed(500);
        }

        if(!lerped)
        {
            ltime += (Time.deltaTime/3.0f);
            if( ltime > 1.0f ) 
                lerped = true;
            else 
                Physics.gravity = Vector3.Lerp(Physics.gravity, newGravity, ltime );
            
        }
        
        
    }

    void ChangePos()
    {
        
        float speed = m_velocity * Time.deltaTime;
        this.transform.position = this.transform.position + m_dir * speed *m_gameSpeed;
    }

    void IScore()
    {
        if(m_bird.transform.position.x + .5f > this.transform.position.x)
        {
            crossed = true;
            scorer.SetScore();
            m_as.PlayOneShot(m_succesAudio);
        }
    }

    public void SetBird(GameObject bird)
    {
        m_bird = bird;
        m_birdMotion = bird.GetComponent<BirdMotion>();
    }

    public void SetScorer(Scorer sc)
    {
        scorer = sc;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class BirdMotion : MonoBehaviour
{

    private Rigidbody m_rigidbody;
    [SerializeField]
    private float force = 1000.0f,m_gameSpeed;
    [SerializeField]
    private AudioClip m_jumpAudio,m_collideAudio;
    private AudioSource m_as;
    private bool colided, resumed;
    [SerializeField]
    private GameObject m_wing1,m_wing2;
    private float m_timer,ltime,newForce;
    private bool lerped;
    [SerializeField] private Scorer scorer;
    private Vector3 m_birdVelocity,m_gravity;
    [SerializeField] Button jump;

    // Start is called before the first frame update
    void Start()
    {
        resumed = true;

        m_rigidbody = GetComponent<Rigidbody>();
        colided = false;
        m_as = GetComponent<AudioSource>();

        m_timer = 0.0f;
        // Toogle between these two wings
        m_wing1.SetActive(true);
        m_wing2.SetActive(false);
        m_gameSpeed = 1.0f;
        lerped = true;

        jump.onClick.AddListener(Jump);
        
    }

    // Update is called once per frame
    void Update()
    {
        // No action if game is paused
        if( UIHandler.isPaused() ) {
            Physics.gravity = Vector3.zero;
            m_rigidbody.velocity = Vector3.zero;
            resumed = false;
            return;
        }
        if(!resumed){
            Physics.gravity = m_gravity;
            m_rigidbody.velocity = m_birdVelocity;
            resumed = true;
        }
        

        if(!colided)
        {
            ControlFly();
            AnimateFly();
        }

        if(!lerped)
        {
            ltime += Time.deltaTime/3.0f;
            if(ltime > 1 )
                lerped = true;
            else
                force = Mathf.Lerp(force,newForce,ltime);
        }
    }

    void ControlFly()
    {
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     m_rigidbody.velocity = new Vector3(0,0,0);
        //     m_rigidbody.AddForce(force * (new Vector3(0,1,0)) );
        //     m_as.PlayOneShot(m_jumpAudio);
        // }
        if(this.transform.position.y < - 5.0f || this.transform.position.y > 6.5f)
        {
            KillTheBird();
        }
        m_birdVelocity = m_rigidbody.velocity;
        m_gravity = Physics.gravity;
    }

    void Jump()
    {
        if(colided) return;
        
        m_rigidbody.velocity = new Vector3(0,0,0);
        m_rigidbody.AddForce(force * (new Vector3(0,1,0)) );
        m_as.PlayOneShot(m_jumpAudio);
    }

    void AnimateFly()
    {
        if(m_timer > 1.0f)
        {
            if(m_wing1.activeSelf)
            {
                m_wing1.SetActive(false);
            }else m_wing1.SetActive(true);

            if(m_wing2.activeSelf)
            {
                m_wing2.SetActive(false);
            }else m_wing2.SetActive(true);
            m_timer = 0.0f;
        }
        m_timer+= Time.deltaTime;
    }


    void OnCollisionEnter(Collision other)
    {
        KillTheBird();
    }

    void KillTheBird()
    {
        colided = true;
        this.transform.rotation = Quaternion.Euler(0,0,-60);
        
        Invoke("ReloadScene",2.0f);
        scorer.SetHighScore();
        m_as.PlayOneShot(m_collideAudio);
    }

    void ReloadScene()
    {
        SceneHandler sh =new SceneHandler();
        sh.ReloadScene();
    }

    public bool IsCollided()
    {
        return colided;
    }

    public void SetGameSpeed(int speed)
    {
        ltime = 0.0f;
        newForce = speed;
        lerped = false;
    }

}

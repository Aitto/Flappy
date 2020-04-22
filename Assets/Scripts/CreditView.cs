
using UnityEngine;
using UnityEngine.UI;

public class CreditView : MonoBehaviour
{
    public Scrollbar scrollbar;
    private float m_sValue;
    public float m_timeCredit;
    // Start is called before the first frame update
    void Start()
    {
        m_sValue = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_sValue = Mathf.Clamp(m_sValue,0.0f,1.0f);
        scrollbar.value = m_sValue;

        m_sValue-= Time.deltaTime/m_timeCredit;
        if(m_sValue <= Mathf.Epsilon) m_sValue =1;
    }
}

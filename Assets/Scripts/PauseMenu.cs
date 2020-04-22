using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject pmenu;
    [SerializeField]
    Button option;
    
    void  Start() {
        pmenu.SetActive(false);
        UIHandler.UnPauseGame();
        option.onClick.AddListener(Check);
    }
    // Update is called once per frame
    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.Escape))
    //     {
            // if(!pmenu.activeSelf)
            // {
            //     pmenu.SetActive(true);
            //     UIHandler.PauseGame();
            // }
            
    //         //else pmenu.SetActive(true);
            
    //     }
    // }

    void Check()
    {
        if(!pmenu.activeSelf)
        {
            pmenu.SetActive(true);
            UIHandler.PauseGame();
        }
    }
}

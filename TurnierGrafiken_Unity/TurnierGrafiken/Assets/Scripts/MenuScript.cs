using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    
    public GameObject overlayPanel;
    public GameObject hotkeyIndicator;
    public Toggle instaToggle;
    public GameObject normalLayout;
    public GameObject squareLayout;
    
    Index index;
    

    void Awake()
    {
        MenuPanelActive(true);
        index = this.gameObject.GetComponent<Index>();
    }

    public void InstaToggleChanged(){
        bool t = instaToggle.isOn;
        squareLayout.SetActive(t);
        normalLayout.SetActive(!t);
        if(t){
            index.screenshotScript.offX = 420;
        }else{
            index.screenshotScript.offX = 96;
        }
    }

    public bool IsInstagramPost(){
        return instaToggle.isOn;
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
            MenuPanelActive(!overlayPanel.activeSelf);
        }
    }

    void MenuPanelActive(bool t){
        overlayPanel.SetActive(t);
        hotkeyIndicator.SetActive(!t);
    }

    public void Quit(){
        Application.Quit();
    }
}

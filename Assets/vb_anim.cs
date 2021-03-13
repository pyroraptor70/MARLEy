using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Vuforia;
 
public class vb_anim : MonoBehaviour
{
 
    public GameObject vbBtnObj;
    public Animator objAni;
    public string actionAni;
    public string defaultAni;
    public bool debug;
    // Use this for initialization
    void Start()
    {
        vbBtnObj = GameObject.Find("VirtualButton");
        vbBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(Play);
        vbBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(Stop);
 
        objAni.GetComponent<Animator>();
    }
 
    public void Play(VirtualButtonBehaviour vb)
    {
        objAni.Play(actionAni);
        if(debug){Debug.Log("Button pressed");}
    }
 
    public void Stop(VirtualButtonBehaviour vb)
    {
        objAni.Play(defaultAni);
        if(debug){Debug.Log("Button released");}
    }
}
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Vuforia;
public class PourControl : MonoBehaviour
{
    public GameObject vbBtnObj;
    public LiquidControl lc;
    public Material material;
    // public Color color;
    private Chemical chemical;
    public float pourRate;
    public string chemicalName;
    private bool pouring;
    private float timeToGo;
    // Use this for initialization
    void Start()
    {
        vbBtnObj = GameObject.Find("VirtualButton");
        vbBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(Pour);
        vbBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(Stop);
        pouring=false;
        chemical = new Chemical(pourRate, new Vector4(0,0,0,0), chemicalName);
        // chemical.v=pourRate;
        // chemical.n=chemicalName;
        if(material.HasProperty("_Color")){chemical.c=material.color;}
        timeToGo=Time.fixedTime+1.0f;
    }
    void Pour(VirtualButtonBehaviour vb){pouring=true;}
    void Stop(VirtualButtonBehaviour vb){pouring=false;}
    void Update(){
        if (Time.fixedTime >= timeToGo) {
            if(pouring){lc.addLiquid(chemical);}
            timeToGo = Time.fixedTime + 1.0f;
        }
    }
    // public void changeChemical(int pourRate, Color c, string name){
    //     chemical.v=pourRate;chemical.c=c;chemical.n=name;
    //     Debug.Log("changed");    
    // }
    public void changeMaterial(Material m){
        if(m.HasProperty("_Color")){chemical.c=m.color;}
        chemical.n=m.ToString();
        Debug.Log("changed");
    }
    public void changeLiquid(LiquidControl nlc){lc=nlc;pouring=false;}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Chemical{
    public float v;
    public Color c;
    public string n;
    public Chemical(float volume, Color color, string name){
        v=volume;
        c=color;
        n=name;
    }
}
public class LiquidControl : MonoBehaviour
{
    // Creates a material from shader and texture references.
    // public GameObject cylinder;
    public Shader shader;
    public Texture texture;
    // public Color color;
    public float hMax;
    private Renderer rend;
    private float h;
    private List<Chemical> chemicals;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = new Material(shader);
        rend.material.mainTexture = texture;
        Color color=new Vector4(0,0,0,0);
        // foreach(Chemical c in chemicals){color+=c.c*c.v;}color=color/h;
        rend.material.color = color;
        h=transform.localScale.y;
    }
    public void addLiquid(Chemical chemical){
        if(h+chemical.v<=hMax){
            bool flag=true;
            foreach (Chemical c in chemicals)
            {
                if(c.n==chemical.n){c.v+=chemical.v;flag=false;}
            }
            if(flag){chemicals.Add(chemical);}
            h+=chemical.v;
            Color color=new Vector4(0,0,0,0);
            foreach(Chemical c in chemicals){color+=c.c*c.v;}color=color/h;
            // float a=color.a;
            // color=(color*h+c*d)/(h+d);
            // color.a=a;
            rend.material.color = color;
            transform.localScale += new Vector3(0,chemical.v,0);
            transform.position += new Vector3(0,chemical.v,0);
        }
    }
}
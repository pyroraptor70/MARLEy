using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chemical {

    public float volume;
    public Color color;
    public string name;

    public Chemical(float volume, Color color, string name) {
        this.volume = volume;
        this.color = color;
        this.name = name;
    }

    public override bool Equals(object obj)
    {
        if (obj is Chemical) {
            return this.Equals(obj as Chemical);
        } else if (obj is string) {
            return this.Equals(obj as string);
        } else {
            return false;
        }
    }

    public bool Equals(Chemical c)
    {
        return this.name == c.name;
    }

    public bool Equals(string s)
    {
        return this.name == s;
    }
}

public class LiquidControl : MonoBehaviour {

    // Creates a material from shader and texture references.
    // public GameObject cylinder;
    public Shader shader;
    public Texture texture;
    // public Color color;
    // TODO: What are h and hMax?
    public float hMax;
    private Renderer rend;
    private float h;
    private List<Chemical> chemicalsInFlask;

    void Start() {

        rend = GetComponent<Renderer>();
        rend.material = new Material(shader);
        rend.material.mainTexture = texture;
        Color color = new Vector4(0, 0, 0, 0);
        rend.material.color = color;
        h = transform.localScale.y;
        chemicalsInFlask = new List<Chemical>();

    }

    public void addLiquid(Chemical chemical) {

        if (h+chemical.volume <= hMax) {

            // Add to the flask's volume
            bool chemicalAlreadyPresent = false;

            foreach (Chemical c in chemicalsInFlask) {
                if (c == chemical) {
                    c.volume += chemical.volume;
                    chemicalAlreadyPresent = true;
                }
            }

            if (!chemicalAlreadyPresent) {
                Chemical c = new Chemical(chemical.volume, chemical.color, chemical.name);
                chemicalsInFlask.Add(c);
            }
            
            h += chemical.volume;


            //Perform any available reactions

            // Check which chemicals are in the flask and assign them if present
            Chemical sodiumHydroxide = null;
            Chemical hydrochloricAcid = null;
            Chemical water = null;
            foreach (Chemical c in chemicalsInFlask)
            {
                // TODO: Check these are really the names of the materials
                if (c.Equals("hydrochloricAcid")) {
                    hydrochloricAcid = c;
                }
                if (c.Equals("sodiumHydroxide")) {
                    sodiumHydroxide = c;
                }
                if (c.Equals("water"))
                {
                    water = c;
                }
            }

            // Reaction happens here
            if (chemicalsInFlask.Contains(sodiumHydroxide) && chemicalsInFlask.Contains(hydrochloricAcid)) {
                // If there is no water in the flask already, create some
                if (water == null)
                {
                    //TODO: What color value does water have?
                    water = new Chemical(0.0f, new Color(0, 0, 0, 0), "water");
                }
                // Take away small amounts of each of the reactants and add to the product of the reaction
                do {
                    sodiumHydroxide.volume -= 0.1f;
                    hydrochloricAcid.volume -= 0.1f;
                    water.volume += 0.2f;
                } while (sodiumHydroxide.volume > 0.1 && hydrochloricAcid.volume > 0.1);
            }

            // Calculate the color change
            Color color = new Vector4(0, 0, 0, 0);

            foreach (Chemical c in chemicalsInFlask) {
                color += c.color * c.volume;
            }

            color = color/h;
            rend.material.color = color;

            // Render the new height
            transform.localScale += Vector3.up * chemical.volume;
            transform.Translate(Vector3.up * chemical.volume);
        }

        Debug.Log(h);
    }

}
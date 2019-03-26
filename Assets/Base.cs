using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    public float tiberium = 0;

    public TextMeshPro text;

    public GameObject fighterPrefab;
    
    List<Fighter> Fighters = new List<Fighter>();


    public Color BaseColour;


    // Start is called before the first frame update
    void Start()
    {
        BaseColour = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);

        foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
            r.material.color = BaseColour;

            StartCoroutine(GainT());
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "" + tiberium;
        
        decision();
        
    }


    void decision() {


        if (tiberium >= 10f) {

           GameObject temp =  Instantiate(fighterPrefab);
            temp.transform.position = transform.position;
           // temp.get

            foreach (Renderer r in temp.GetComponentsInChildren<Renderer>()) {
                r.material.color = BaseColour;
            }

            temp.GetComponent<Fighter>().HomeBase = this.transform; 
            
            Fighters.Add(temp.GetComponent<Fighter>());


            tiberium -= 10f;

        }



    }
    
    



    IEnumerator GainT() {

        while (true) {
            yield return new WaitForSeconds(1f);

            tiberium++;
        }




    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("bullet")) {


            tiberium -= 0.5f;
            
            Destroy(other.gameObject,0.5f);

        }
    }


}

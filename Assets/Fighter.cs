using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Fighter : MonoBehaviour {


    public float Tiberium;

    public Transform HomeBase;

    public float AttackRange;


    private Transform TargetBase;

    public State currentState = State.seeking;

    public GameObject BulletPrefab;

    public enum State { 
    
    seeking,
    attacking,
    retreating,
    refuling
    
    }

    private void Start() {

        Base[] Bases = FindObjectsOfType<Base>();

        while (TargetBase == null) {


            int choice = Random.Range(0, Bases.Length);

            Base check = Bases[choice];

            if (check != HomeBase.GetComponent<Base>()) {

                TargetBase = check.transform;

            }


        }


        GetComponent<Arrive>().targetPosition = TargetBase.position; 


    }


    void Update() {

        switch (currentState) {
                
            
            case(State.seeking):


                if (Vector3.Distance(transform.position, TargetBase.position) <= AttackRange) {

                    currentState = State.attacking;

                    //GetComponent<Arrive>().enabled = false;

                    StartCoroutine(Shoot());


                }
                

                break;


            case (State.attacking) :


                if (Tiberium <= 0) {

                    currentState = State.retreating;

                  //  GetComponent<Arrive>().enabled = true;
                    GetComponent<Arrive>().targetPosition = HomeBase.position;



                }

//Debug.Log("I am attacking");

                break;
            
            case (State.retreating):

                if (Vector3.Distance(transform.position, HomeBase.position) < 10f && HomeBase.GetComponent<Base>().tiberium >= 7f) {

                    currentState = State.refuling;


                }

                break;
                

            case (State.refuling):


                currentState = State.seeking;

                GetComponent<Arrive>().targetPosition = TargetBase.position;

                HomeBase.GetComponent<Base>().tiberium -= 7f;

                Tiberium = 7;
                

                break;
            
            
            
            
        }
        
        
       



    }



    IEnumerator Shoot() {


        while (Tiberium > 0) {


            GameObject tempBullet = Instantiate(BulletPrefab);
            tempBullet.transform.position = transform.position;
            tempBullet.transform.rotation = transform.rotation;

            tempBullet.GetComponent<MeshRenderer>().material.color = HomeBase.GetComponent<Base>().BaseColour;//GetComponent<MeshRenderer>().material.color;

            Tiberium--;
            
            
            yield return new WaitForSeconds(0.2f);


        }


    }


}

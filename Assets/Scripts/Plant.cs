using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DEPRICATED
public class Plant : MonoBehaviour
{
    // Start is called before the first frame update
    private int age; //in days
    private int maturityAge; //days to be mature;
    public GameObject[] models;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public GameObject GetModel(){
        if (age<maturityAge)
        {
            int modelIndex = age*models.Length/maturityAge;
            return models[modelIndex];
        } else {
            return models[models.Length-1];
        }
    }
    
    public void SetType(int inputType, int inputAge){
        age = inputAge;
    }
}

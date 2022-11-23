using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//contains all variables used in saving and loading data for each tile
public class TileState : MonoBehaviour
{
    public int age;
    int type;
    int MaturityAge = 9;
    public bool canHarvest;
    
    public TileState(string stringified){
        //TODO parse stringified tilestate
        string[] split = stringified.Split('|');
        age = Int32.Parse(split[0]);
        type = Int32.Parse(split[1]);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        canHarvest = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public string Stringify(){
        //TODO stringify tilestate
        return age+"|"+type;
    }
    
    //create gameObject from tileState
    public GameObject InstantiateTile(Vector3 position){
        int plantIndex = age*1;
        GameObject tile = Instantiate(GridManager.plants[GetPlantModelIndex()], position, new Quaternion()); //TODO actually choose the correct plant to create.
        tile.GetComponent<TileBehaviour>().SetState(this);
        
        return tile;
    }
    
    private int GetPlantModelIndex(){
        int maturityAge=2; //days from seed to harvest
        int stages=0; //number of distinct models
        int startIndex=0; //index of first model of this type
        
        switch (type)
        {
            case -1: //Not Planted
                maturityAge = 2;
                stages = 1;
                startIndex = 0;
                age = 0;
                break;
            case 0: //Basic flower
                maturityAge = 9;
                stages = 3;
                startIndex = 1;
                break;
            case 1: //pumpkin
                maturityAge = 9;
                stages = 3;
                startIndex = 4;
                break;
            case 2: //???
                maturityAge = 9;
                stages = 3;
                startIndex = 7;
                break;
            default:
                return 0;
        }
        canHarvest = age>=maturityAge;
        return startIndex + Math.Min(age*(stages-1)/(MaturityAge-1), stages-1);
    }
    
    public void Harvest(){
        age = 0;
        float LostSeed = UnityEngine.Random.Range(0.0f,1.0f);
        if (LostSeed <0.3f)
        {
            type = -1;
        }
    }
    
}

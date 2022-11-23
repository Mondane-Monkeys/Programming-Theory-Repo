using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//
// Summary
//      contains all variables used in saving and loading data for each tile
public class TileState : MonoBehaviour
{
    public static TileState NewTileState(string stringified, int i, int j){
        string[] split = stringified.Split('|');
        int tempAge = Int32.Parse(split[0]);
        int tempType = Int32.Parse(split[1]);
        return NewTileState(tempAge, tempType, i, j);
    }
    
    public static TileState NewTileState(int i, int j){
        return new TileState(0, -1, i, j);
    }
    
    public static TileState NewTileState(int age, int type, int i, int j){
        switch (type)
        {
            case -1:
                return new TileState(i,j); //dirt pile, default tileState
            case 0:
                return new FlowerState(age, type, i, j);
            case 1:
                return new PumpkinState(age, type, i, j);
            default:
                return new TileState(age, type, i, j);
        }
    }
    
    
    
    public int age;
    public int type;
    public bool canHarvest;
    public GameObject plant;
    private Vector3 position;
    
    private float cellWidth = 1.2f;
    private float cellHeight = 1.2f;
    //
    // Summary:
    //     TileState is the base class from which every Unity script derives.
    public TileState(string stringified, int i, int j){
        //TODO parse stringified tilestate
        string[] split = stringified.Split('|');
        age = Int32.Parse(split[0]);
        type = Int32.Parse(split[1]);
        position = new Vector3(i*cellWidth, 0, j*cellHeight);
    }
    
    public TileState(int age, int type, int i, int j){
        this.age = age;
        this.type = type;
        position = new Vector3(i*cellWidth, 0, j*cellHeight);
    }
    
    public TileState(int i, int j){
        this.age = 0;
        this.type = -1;
        position = new Vector3(i*cellWidth, 0, j*cellHeight);
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
    public void InstantiateTile(){
        if (plant==null)
        {
            GameObject tile = Instantiate(GridManager.plants[GetPlantModelIndex()], position, new Quaternion());
            tile.GetComponent<PlantTileInterface>().SetState(this);
            plant = tile;
        }
        
        // plant.GetComponent<>();
    }
    
    //TODO improve this thing to actually be effective.
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
                maturityAge = 5;
                stages = 3;
                startIndex = 1;
                break;
            case 1: //pumpkin
                maturityAge = 12;
                stages = 3;
                startIndex = 4;
                break;
            case 2: //???
                maturityAge = 2;
                stages = 3;
                startIndex = 7;
                break;
            default:
                return 0;
        }
        canHarvest = age>=maturityAge;
        return startIndex + Math.Min(age*(stages-1)/(maturityAge), stages-1);
    }
    
    public void Harvest(){
        if (canHarvest)
        {
            FieldCanvas fCanvas = GameObject.Find("Canvas").GetComponent<FieldCanvas>();
            fCanvas.money+=GetFruitValue();
            
            Destroy(plant);
            age = 0;
            type = -1;
            plant = null;
            InstantiateTile();
            
        }
    }

    public void IncrementDay(){
        age++;
    }
    
    public void PlantSeed(int seedType){
        if (type == -1)
        {
            FieldCanvas fCanvas = GameObject.Find("Canvas").GetComponent<FieldCanvas>();
            if((fCanvas.money-=GetSeedPrice(seedType))<0){
                fCanvas.money+=GetSeedPrice(seedType);
                return;
            }
            type = seedType;
            age = 0;
            Destroy(plant);
            plant = null;
            InstantiateTile();
        }
    }
    
    public void Clicked(){
        if (canHarvest)
        {
            
        }
    }
    
    private int GetSeedPrice(int seedType){
        switch (seedType)
        {
            case -1: //none
                return 0;
            case 0: //flower
                return 40;
            case 1: //pumpkin
                return 80;
            case 2: //???
                return 120;
            default:
                return 50;
        }
    }
    
      private int GetFruitValue()
    {
        switch (type)
        {
            case -1: //none
                return 0;
            case 0: //flower
                return 90;
            case 1: //pumpkin
                return 230;
            case 2: //???
                return 250;
            default:
                return 50;
        }
    }
}

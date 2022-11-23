using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//
// Summary
//      contains all variables used in saving and loading data for each tile
public class TileState : MonoBehaviour
{
    public int age;
    public int type;
    public bool canHarvest;
    public GameObject plant;
    public Vector3 position;
    
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
            GameObject tile = Instantiate(PlantsEnumMethods.GetPlantModel(age, (PlantsEnum)type, this), position, new Quaternion());
            tile.GetComponent<PlantTileInterface>().SetState(this);
            plant = tile;
        }
        
        // plant.GetComponent<>();
    }
    
    //TODO improve this thing to actually be effective.
    
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

    public void PartialHarvest(int newAge){
        if (canHarvest)
        {
            FieldCanvas fCanvas = GameObject.Find("Canvas").GetComponent<FieldCanvas>();
            fCanvas.money+=GetFruitValue();
            
            age = newAge;
            
            Destroy(plant);
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

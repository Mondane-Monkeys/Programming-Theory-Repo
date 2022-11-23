using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum PlantsEnum
{
    dirt = -1,
    flower = 0,
    pumpkin = 1,
    other = 2
}

public class PlantsEnumMethods : MonoBehaviour{
    public static GameObject GetPlantModel(int age, PlantsEnum type, TileState state){
        int maturityAge=2; //days from seed to harvest
        int stages=0; //number of distinct models
        int startIndex=0; //index of first model of this type
        
        switch (type)
        {
            case PlantsEnum.dirt: //Not Planted
                maturityAge = 2;
                stages = 1;
                startIndex = 0;
                age = 0;
                break;
            case PlantsEnum.flower: //Basic flower
                maturityAge = 5;
                stages = 3;
                startIndex = 1;
                break;
            case PlantsEnum.pumpkin: //pumpkin
                maturityAge = 12;
                stages = 3;
                startIndex = 4;
                break;
            case PlantsEnum.other: //???
                maturityAge = 2;
                stages = 3;
                startIndex = 7;
                break;
        }
        
        state.canHarvest = age>=maturityAge;
        int ModelIndex = startIndex + Math.Min(age*(stages-1)/(maturityAge), stages-1);
        return GridManager.plants[ModelIndex];
    }
}

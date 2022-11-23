using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains behaviour for non-permenant behaviour...
public class PlantTileInterface : MonoBehaviour
{
    TileState state;
    Renderer plantRenderer;
    Color defaultColor;
    // Start is called before the first frame update
    void Start()
    {
        plantRenderer = GetComponent<Renderer>();
        defaultColor = plantRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnMouseEnter(){
        plantRenderer.material.color = Color.red;
    }
    
    private void OnMouseExit(){
        plantRenderer.material.color = defaultColor;
    }
    
    public void SetState(TileState inState){
        state = inState;
    }
    
    public void Clicked(int seedType){
        if (state.type>=0)
        {
            if (state.canHarvest)
            {
                state.Harvest();
            }
        } else {
            state.PlantSeed(seedType);
        }
    }
}

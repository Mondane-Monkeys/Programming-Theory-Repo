// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// //parent class of all other tile behaviour classes - 
// //return a TileState for saving
// //DEPRICATED
// public class TileBehaviour : MonoBehaviour
// {
//     private TileState state;
    
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
    
//     public void SetState(TileState inState){
//         state = inState;
//     }
    
//     public TileState GetState() {
//         return state;
//     }
    
//     //DEPRICATED
//     public virtual void IncrementDay(){
//         state.age++;
//     }
    
//     //DEPRICATED
//     public virtual bool Harvest(){
//         if (state.canHarvest)
//         {
//             state.Harvest();
//             Destroy(gameObject);
//             return true;
//         }
//         return false;
//     }
// }

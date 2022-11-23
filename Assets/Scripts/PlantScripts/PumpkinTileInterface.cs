using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinTileInterface : PlantTileInterface
{
   public override void Clicked(int seedType)
   {
        if (base.state.type>=0)
        {
            if (base.state.canHarvest)
            {
                base.state.PartialHarvest(9);
            }
        } else {
            base.state.PlantSeed(seedType);
        }
   }
}

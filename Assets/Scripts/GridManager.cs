using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;


//Handles location and state of all plants. -> Plants are stored in a 2d array of ENUMs
//Also initializes all plant GameObjects
public class GridManager : MonoBehaviour
{
    public static List<GameObject> plants;

    private const int GridSize = 3;
    private float cellWidth = 1;
    private float cellHeight = 1;
    public GameObject[,] plantGrid; // initially 3x3 of Plots (empty GameObject)

    // Start is called before the first frame update
    void Start()
    {
        //link prefabs to plants list
        LinkPrefabs();

        //Load GameState
        LoadScene();
    }

    // Update is called once per frame
    void Update()
    {
        //if user clicks, remove all harvestables
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HarvestAll();
        }
    }

    void LinkPrefabs()
    {
        plants = new List<GameObject>();
        //dirt (0)
        plants.Add((GameObject)Resources.Load("PlantPrefabs/Dirt Patch", typeof(GameObject)));
        
        //generic (1-3)
        plants.Add((GameObject)Resources.Load("PlantPrefabs/Seed", typeof(GameObject)));
        plants.Add((GameObject)Resources.Load("PlantPrefabs/Sprout", typeof(GameObject)));
        plants.Add((GameObject)Resources.Load("PlantPrefabs/Flower", typeof(GameObject)));
        
        //Pumpkin (4-6)
        plants.Add((GameObject)Resources.Load("PlantPrefabs/PumpkinSeed", typeof(GameObject)));
        plants.Add((GameObject)Resources.Load("PlantPrefabs/PumpkinSprout", typeof(GameObject)));
        plants.Add((GameObject)Resources.Load("PlantPrefabs/PumpkinFlower", typeof(GameObject)));


    }

    void LoadScene()
    {
        TileState[,] loadedData = IOInterface.LoadState();
        if (loadedData != null)
        {
            plantGrid = new GameObject[loadedData.GetLength(0), loadedData.GetLength(1)];

            //load plantGrid array
            for (int i = 0; i < plantGrid.GetLength(0); i++)
            {
                for (int j = 0; j < plantGrid.GetLength(1); j++)
                {
                    plantGrid[i, j] = loadedData[i, j].InstantiateTile(new Vector3(i * cellWidth, 0, j * cellHeight));
                }
            }
        }
        else
        {
            //default grid
            plantGrid = new GameObject[3, 3];
            for (int i = 0; i < plantGrid.GetLength(0); i++)
            {
                for (int j = 0; j < plantGrid.GetLength(1); j++)
                {
                    plantGrid[i, j] = (new TileState(i * 3 + "|" + 0)).InstantiateTile(new Vector3(i * cellWidth, 0, j * cellHeight));
                }
            }
        }
    }

    void SaveScene()
    {
        IOInterface.SaveState(plantGrid);
    }

    public void IncrementDay()
    {
        for (int i = 0; i < plantGrid.GetLength(0); i++)
        {
            for (int j = 0; j < plantGrid.GetLength(1); j++)
            {
                plantGrid[i, j].GetComponent<TileBehaviour>().IncrementDay();
            }
        }
        
        SaveScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void HarvestAll(){
        for (int i = 0; i < plantGrid.GetLength(0); i++)
        {
            for (int j = 0; j < plantGrid.GetLength(1); j++)
            {
                if(plantGrid[i, j].GetComponent<TileBehaviour>().Harvest()){
                    plantGrid[i, j] = PlantSeed(-1, i, j);
                }
            }
        }
    }
    
    public GameObject PlantSeed(int seedType, int col, int row){
        return (new TileState(seedType+"|0")).InstantiateTile(new Vector3(col * cellWidth, 0, row * cellHeight));
    }
}

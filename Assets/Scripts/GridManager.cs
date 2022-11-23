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
    public Canvas canvas;

    private const int GridSize = 6;
    private float cellWidth = 1;
    private float cellHeight = 1;
    
    public TileState[,] plantGrid; // initially 3x3 of Plots (empty GameObject)

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
        plantGrid = IOInterface.LoadGrid();
        if (plantGrid != null)
        {
            //Instantiate plants
            for (int i = 0; i < plantGrid.GetLength(0); i++)
            {
                for (int j = 0; j < plantGrid.GetLength(1); j++)
                {
                    plantGrid[i, j].InstantiateTile();
                }
            }
        }
        else
        {
            //default grid
            plantGrid = new TileState[GridSize, GridSize];
            for (int i = 0; i < plantGrid.GetLength(0); i++)
            {
                for (int j = 0; j < plantGrid.GetLength(1); j++)
                {
                    plantGrid[i, j] = new TileState(0 , -1, i, j);
                    plantGrid[i, j].InstantiateTile();
                }
            }
        }
    }

    void SaveScene()
    {
        FieldCanvas fCanvas = canvas.GetComponent<FieldCanvas>();
        IOInterface.SaveState(plantGrid, fCanvas.days, fCanvas.money);
    }

    public void IncrementDay()
    {
        for (int i = 0; i < plantGrid.GetLength(0); i++)
        {
            for (int j = 0; j < plantGrid.GetLength(1); j++)
            {
                plantGrid[i, j].IncrementDay();
            }
        }
        
        canvas.GetComponent<FieldCanvas>().days++;
        
        SaveScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void HarvestAll(){
        for (int i = 0; i < plantGrid.GetLength(0); i++)
        {
            for (int j = 0; j < plantGrid.GetLength(1); j++)
            {
                plantGrid[i, j].Harvest();
            }
        }
    }
    
    public void PlantAll(){
        for (int i = 0; i < plantGrid.GetLength(0); i++)
        {
            for (int j = 0; j < plantGrid.GetLength(1); j++)
            {
                plantGrid[i, j].PlantSeed(Random.Range(-1,2));
            }
        }
    }
    
    public void QuitGame(){
        SceneManager.LoadScene(0);
    }
}

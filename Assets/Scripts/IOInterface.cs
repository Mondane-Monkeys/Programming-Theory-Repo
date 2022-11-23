using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

//take an array of GameObjects and return a string
//tale a string and return an array of GameObjects
public class IOInterface
{
    private static string filename = "/savefile.txt";
    
    //take a grid of abstract tile objects, and stringify, and save
    public static void SaveState(GameObject[,] objects)
    {
        //stringify objects
        //get HW
        //forEach GO.stringify()
        int w = objects.GetLength(0);
        int h = objects.GetLength(1);

        string stringified = w + " " + h + " ";

        for (int i = 0; i < objects.GetLength(0); i++)
        {
            for (int j = 0; j < objects.GetLength(1); j++)
            {
                stringified += objects[i, j].GetComponent<TileBehaviour>().GetState().Stringify() + " ";
            }
        }
        //save to disk
        File.WriteAllText(Application.persistentDataPath + filename, stringified);
    }

    public static TileState[,] LoadState()
    {
        //load String from disk
        string filePath = Application.persistentDataPath + filename;
        if (File.Exists(filePath))
        {
            string stringified = File.ReadAllText(filePath);

            //getRows and Cols
            string[] split1 = stringified.Split(' ');
            TileState[,] state = new TileState[Int32.Parse(split1[0]), Int32.Parse(split1[1])];

            //Populate state
            int counter = 2;
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    state[i, j] = new TileState(split1[counter++]);
                }
            }
            return state;
        }
        return null;
    }



}

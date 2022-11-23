using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

//take an array of GameObjects and return a string
//tale a string and return an array of GameObjects

//style like:
//#rows, #cols, a|t a|t ... a|t
public class IOInterface
{
    private static string gridFilename = "/gridfile.txt";
    private static string valuesFilename = "/valuefile.txt";
    
    //take a grid of abstract tile objects, and stringify, and save
    public static void SaveState(TileState[,] tiles, int days, int money)
    {
        //stringify objects
        //get HW
        //forEach GO.stringify()
        int w = tiles.GetLength(0);
        int h = tiles.GetLength(1);

        string stringified = w + " " + h + " ";

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                stringified += tiles[i, j].Stringify() + " ";
            }
        }
        //save to disk
        File.WriteAllText(Application.persistentDataPath + gridFilename, stringified);
        File.WriteAllText(Application.persistentDataPath + valuesFilename, days+" "+money);
    }

    public static TileState[,] LoadGrid()
    {
        //load String from disk
        string filePath = Application.persistentDataPath + gridFilename;
        if (File.Exists(filePath))
        {
            string stringified = File.ReadAllText(filePath);
            string[] split = stringified.Split(' ');
            
            if (split.Length < 4 )
            {
                return null;
            }
            
            int counter = 0; //points to current substring
            
            //getRows and Cols
            TileState[,] state = new TileState[Int32.Parse(split[counter++]), Int32.Parse(split[counter++])]; 

            //Populate state
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    state[i, j] = new TileState(split[counter++], i, j);
                }
            }
            return state;
        }
        return null;
    }
    
    //get day number and money amount
    public static int[] LoadValues()
    {
         //load String from disk
        string filePath = Application.persistentDataPath + valuesFilename;
        if (File.Exists(filePath))
        {
            string stringified = File.ReadAllText(filePath);
            string[] split = stringified.Split(' ');
            int counter = 0;
            
            int[] values = {Int32.Parse(split[counter++]), Int32.Parse(split[counter++])};
            return values;
        }
        return null;
    }
}

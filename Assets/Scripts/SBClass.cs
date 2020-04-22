using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SBClass : MonoBehaviour
{
    private static string fileName = Application.persistentDataPath + "/HSJson.json";

    public static void SetHighScore(int socre)
    {
        print("From Set-->" + fileName);
        if(File.Exists(fileName))
        {
            string jsonText = File.ReadAllText(fileName);
        
            Board jsonBoard = JsonUtility.FromJson<Board>(jsonText);
            
            if(jsonBoard.HighScore < socre)
                jsonBoard.HighScore = socre;
            else return;

            jsonText = JsonUtility.ToJson(jsonBoard);

            File.WriteAllText(fileName,jsonText);
            
        }else CreateFile();

        //FileStream jStream = File.OpenRead(fileName);  
    }

    public static int GetHighScore()
    {
        print("From Get-->" + fileName);
        if(! File.Exists(fileName)) CreateFile();

        string jsonText = File.ReadAllText(fileName);
        
        Board jsonBoard = JsonUtility.FromJson<Board>(jsonText);
        return jsonBoard.HighScore;
    }

    private static void CreateFile()
    {
        print("From Create-->" + fileName);
        Board board = new Board();
        board.HighScore = 0;
        string jt = JsonUtility.ToJson(board);
        
        File.WriteAllText(fileName,jt);
    }
}


// Json classes must be serializable
[System.Serializable]
public class Board
{
    public int HighScore;
}

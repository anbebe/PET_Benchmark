using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WriteToCSV : MonoBehaviour
{
    private List<String> fileLines = new List<String>();
    
    // Start is called before the first frame update
    void Start()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Write(List<SpawnObjectInfo> spawnObjectList, List<int> patternOrder)
    {
        Debug.Log("Writing File");
        String sense = ExperimentManager.currentMode.ToString(); 
        int trialNumber = ExperimentManager.trialNumber;
        Guid userID = ExperimentManager.userGuid;
        String filename = Application.dataPath + "/Data/" + userID + "_Trial_" + trialNumber +"_"+ sense + ".csv";
        String header = "Pattern;ObjectIndex;ObjectSpawnPos;ObjectTargetPos;ObjectDirection;IsCollision;HitPlayer;PlayerScore;PlayerMoves;NewPlayerPos";
        TextWriter csvFile = new StreamWriter(filename, false);
        
        int pattern = 1;
        int objectIndex = 0;
        Vector3 objectSpawnPos = Vector3.zero;
        Vector3 objectTargetPos = Vector3.zero;
        Vector3 objectDirection = Vector3.zero;
        bool isCollision = false;
        bool hitPlayer = false;
        float playerScore = 50.0f;
        int playerMoves = 0;
        Vector3 newPlayerPos = Vector3.zero;
        
        for (int i = 0; i<spawnObjectList.Count; i++)
        {
            pattern = patternOrder[(i / 10)];
            playerScore = spawnObjectList[i].playerScore;
            objectIndex = i;
            objectSpawnPos = spawnObjectList[i].spawnPos;
            objectTargetPos = spawnObjectList[i].targetPos;
            objectDirection = (objectTargetPos - objectSpawnPos).normalized;
            isCollision = spawnObjectList[i].isCollisionObject;
            hitPlayer = spawnObjectList[i].hitPlayer;
            playerMoves = spawnObjectList[i].playerMovements;
            newPlayerPos = spawnObjectList[i].newPlayerPos;
            fileLines.Add(pattern.ToString() + ";" +objectIndex.ToString() + ";" + objectSpawnPos.ToString() + ";" +objectTargetPos.ToString() + ";" + objectDirection + ";" + isCollision.ToString() + ";" + hitPlayer.ToString() + ";" + playerScore.ToString() + ";" + playerMoves.ToString() + ";" + newPlayerPos.ToString());
        }
        
        csvFile.WriteLine(header);

        foreach (var line in fileLines)
        {
            csvFile.WriteLine(line);
        }
        fileLines.Clear();
        
        csvFile.Close();
    }
    
    public void DemoCSV()
    {
        Debug.Log("Writing Debug File");
        String sense = "Vision"; 
        int trialNumber = 0;
        String filename = Application.dataPath + "/Data/" + ExperimentManager.userGuid + "_Trial_" + trialNumber +"_"+ sense + ".csv";
        String header = "Pattern,ObjectIndex,ObjectSpawnPos,ObjectTargetPos,ObjectDirection,IsCollision,HitPlayer,PlayerScore,PlayerMoves,NewPlayerPos";
        TextWriter csvFile = new StreamWriter(filename, false);
        
        int pattern = 1;
        int objectIndex = 0;
        Vector3 objectSpawnPos = Vector3.zero;
        Vector3 objectTargetPos = Vector3.right;
        Vector3 objectDirection = (objectTargetPos - objectSpawnPos).normalized;
        bool isCollision = false;
        bool hitPlayer = false;
        float playerScore = 50.0f;
        int playerMoves = 0;
        Vector3 newPlayerPos = Vector3.zero;

        fileLines.Add(pattern.ToString() + "," +objectIndex.ToString() + "," + objectSpawnPos.ToString() + "," +objectTargetPos.ToString() + "," + objectDirection + "," + isCollision.ToString() + "," + hitPlayer.ToString() + "," + playerScore.ToString() + "," + playerMoves.ToString() + "," + newPlayerPos.ToString());
        fileLines.Add(pattern.ToString() + "," +objectIndex.ToString() + "," + objectSpawnPos.ToString() + "," +objectTargetPos.ToString() + "," + objectDirection + "," + isCollision.ToString() + "," + hitPlayer.ToString() + "," + playerScore.ToString() + "," + playerMoves.ToString() + "," + newPlayerPos.ToString());
        fileLines.Add(pattern.ToString() + "," +objectIndex.ToString() + "," + objectSpawnPos.ToString() + "," +objectTargetPos.ToString() + "," + objectDirection + "," + isCollision.ToString() + "," + hitPlayer.ToString() + "," + playerScore.ToString() + "," + playerMoves.ToString() + "," + newPlayerPos.ToString());

       // fileLines.Add(pattern.ToString() + "," + isCollision + "," + hitPlayer + "," + playerScore.ToString() + "," + playerMoved);
       // fileLines.Add(pattern.ToString() + "," + isCollision + "," + hitPlayer + "," + playerScore.ToString() + "," + playerMoved);
       // fileLines.Add(pattern.ToString() + "," + isCollision + "," + hitPlayer + "," + playerScore.ToString() + "," + playerMoved);
        
        csvFile.WriteLine(header);

        foreach (var line in fileLines)
        {
            csvFile.WriteLine(line);
        }
        fileLines.Clear();
        
        csvFile.Close();
    }
    
}

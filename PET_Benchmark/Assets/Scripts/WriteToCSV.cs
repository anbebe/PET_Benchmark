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

    public void Write(List<float> scores, List<int> patternOrder)
    {
        Debug.Log("Writing File");
        String sense = ExperimentManager.currentMode.ToString(); 
        int trialNumber = ExperimentManager.trialNumber;
        Guid userID = ExperimentManager.userGuid;
        String filename = Application.dataPath + "/Data/" + userID + "_Trial_" + trialNumber +"_"+ sense + ".csv";
        String header = "Pattern,PlayerScore";
        TextWriter csvFile = new StreamWriter(filename, false);
        int pattern = 0;
        float playerScore = -1;
        for (int i = 0; i<scores.Count; i++)
        {
            pattern = patternOrder[(i / 10)];
            playerScore = scores[i];
            fileLines.Add(pattern.ToString() + "," + playerScore.ToString());
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
        String header = "Pattern,IsCollision,HitPlayer,PlayerScore,PlayerMoved";
        TextWriter csvFile = new StreamWriter(filename, false);
        
        int pattern = 1;
        bool isCollision = false;
        bool hitPlayer = false;
        float playerScore = 50.0f;
        bool playerMoved = false;

        fileLines.Add(pattern.ToString() + "," + isCollision + "," + hitPlayer + "," + playerScore.ToString() + "," + playerMoved);
        fileLines.Add(pattern.ToString() + "," + isCollision + "," + hitPlayer + "," + playerScore.ToString() + "," + playerMoved);
        fileLines.Add(pattern.ToString() + "," + isCollision + "," + hitPlayer + "," + playerScore.ToString() + "," + playerMoved);
        
        csvFile.WriteLine(header);

        foreach (var line in fileLines)
        {
            csvFile.WriteLine(line);
        }
        fileLines.Clear();
        
        csvFile.Close();
    }
    
}

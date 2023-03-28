using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text guidText;
    [SerializeField] private TMP_Dropdown modeChoice1st;
    [SerializeField] private TMP_Dropdown modeChoice2nd;
    [SerializeField] private TMP_Dropdown modeChoice3rd;
    [SerializeField] private GameObject preTutorialCanvasContent;
    [SerializeField] private GameObject tutorialCanvasContent;
    
    //sets up correct menu content 
    void Start()
    {
        guidText.text = "GUID: " + ExperimentManager.userGuid.ToString();
        modeChoice1st.value = 0;
        modeChoice2nd.value = 1;
        modeChoice3rd.value = 2;
        tutorialCanvasContent.SetActive(false);
        preTutorialCanvasContent.SetActive(true);
    }
    
    //Sets modes, chances canvas content and begins tutorial
    public void StartTutorial()
    {
        //TODO: Ensure only unique modes
        //TODO: Start actual Tutorial
        setModeFromDropdown(0, modeChoice1st);
        setModeFromDropdown(1, modeChoice2nd);
        setModeFromDropdown(2, modeChoice3rd);
        
        Debug.Log(ExperimentManager.modeList[0].ToString()  + " " + ExperimentManager.modeList[1].ToString() + " " + ExperimentManager.modeList[2].ToString());
        ExperimentManager.StartExperiment();
        //ExperimentManager.AdvanceExperiment();
        //preTutorialCanvasContent.SetActive(false);
        //tutorialCanvasContent.SetActive(true);
    }
    
    //Sets the correct mode in the modeList in the ExperimentManager for the given index
    private void setModeFromDropdown(int index, TMP_Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                ExperimentManager.modeList[index] = ExperimentManager.ExperimentMode.Auditory;
                break;
            case 1:
                ExperimentManager.modeList[index] = ExperimentManager.ExperimentMode.Tactile;
                break;
            case 2:
                ExperimentManager.modeList[index] = ExperimentManager.ExperimentMode.Visual;
                break;
        }
    }
    
    //Loads main Experiment scene and sets current mode
    public void StartExperiment()
    {
        //ExperimentManager.currentMode = ExperimentManager.modeList[0];
        //ExperimentManager.Load(ExperimentManager.Scene.MainExperimentScene);
    }
    
}

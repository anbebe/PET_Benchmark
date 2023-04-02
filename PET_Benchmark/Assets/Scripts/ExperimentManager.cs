using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

/*
 * A Static Experiment manager, that acts as a Scene Loader, and also holds and manages most of the global data*
 */
public static class ExperimentManager
{ 
    //Public enumerator defining the available scenes. 
    public enum Scene
    {
        PreExperiment, ExperimentAuditory, ExperimentTactile, ExperimentVisual, TutorialAuditory, TutorialTactile, TutorialVisual, PostExperiment
    }

    //Public enumerator defining the potential modes of the experiment. 
    public enum ExperimentMode
    {
        Auditory, Tactile, Visual
    }

    public static ExperimentMode currentMode = ExperimentMode.Auditory;

    public static ExperimentMode[] modeList = new[]
        {ExperimentMode.Auditory, ExperimentMode.Tactile, ExperimentMode.Visual};
    public static Guid userGuid = System.Guid.NewGuid();
    //Todo set proper trial
    public static int modeIndex = -1;

    private static List<String> Scenes = new List<String>();

    public static List<int> patternOrder0 = new List<int>() { 1, 2, 3 };
    public static List<int> patternOrder1 = new List<int>() { 3, 1, 2 };
    public static List<int> patternOrder2 = new List<int>() { 2, 3, 1 };

    public static bool isTutorial = true; 


    //Use SceneManager to load a scene
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    //Set mode from string -> this way it is possible to choose it from the UI
    public static void SetExperimentMode(string mode)
    {
        currentMode = (ExperimentManager.ExperimentMode)System.Enum.Parse(typeof(ExperimentManager.ExperimentMode), mode);
        Debug.Log(currentMode);
    }

    public static void AdvanceExperiment()
    {
        if (Scenes.Count > 0)
        {
            if (Scenes.Count % 2 == 0)
            {
                modeIndex++;
                currentMode = modeList[modeIndex];
                isTutorial = true;
            }
            else
            {
                isTutorial = false;
            }
            SceneManager.LoadScene(Scenes[0]);
            Scenes.RemoveAt(0);
        }
        else
        {
            SceneManager.LoadScene(ExperimentManager.Scene.PostExperiment.ToString());
        }
    }

    public static void StartExperiment()
    {
        /*
         * Set new list with all scenes based on ExperimentMode[] order
         */
        for (int i = 0; i < modeList.Length; i++)
        {
            Scenes.Add("Tutorial" + modeList[i].ToString());
            Scenes.Add("Experiment" + modeList[i].ToString());
        }
        AdvanceExperiment();
    }




}
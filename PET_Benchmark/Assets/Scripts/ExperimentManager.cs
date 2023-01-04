using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * A Static Experiment manager, that acts as a Scene Loader, and also holds and manages most of the global data*
 */
public static class ExperimentManager
{ 
    //Public enumerator defining the available scenes. 
    public enum Scene
    {
        TutorialScene, MainExperimentScene
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
    



}
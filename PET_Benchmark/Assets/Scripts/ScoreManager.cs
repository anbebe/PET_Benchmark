using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
   private float totalScore;

   public float TotalScore
   {
      get => totalScore;
      set => totalScore = value;
   }

   public void ShowTotalScore()
   {
      Debug.Log("Total Score: " + Mathf.Round(totalScore));
   }
}

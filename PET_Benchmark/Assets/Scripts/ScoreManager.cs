using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
   private float totalScore;
   private List<float> individualScores = new List<float>();

   public float TotalScore
   {
      get => totalScore;
      set => totalScore = value;
   }
   public List<float> IndividualScores
   {
      get => individualScores;
   }

   public void ShowTotalScore()
   {
      Debug.Log("Total Score: " + totalScore);
   }

   public void addScore(float score)
   {
      totalScore += score;
      individualScores.Add(score);
   }
}

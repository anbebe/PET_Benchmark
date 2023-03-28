using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
   private float totalScore = 0f;
   private List<float> individualScores = new List<float>();

   private List<SpawnObjectInfo> spawnObjectList = new List<SpawnObjectInfo>();
   public float TotalScore
   {
      get => totalScore;
      set => totalScore = value;
   }
   public List<float> IndividualScores
   {
      get => individualScores;
   }
   public List<SpawnObjectInfo> SpawnObjectList
   {
      get => spawnObjectList;
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

   public void addScoreNew(float score, int playerMovements, bool isCollision, bool hitPlayer, Vector3 spawn, Vector3 target, Vector3 playerPos)
   {
      totalScore += score;
      SpawnObjectInfo newObject = new SpawnObjectInfo();
      newObject.playerScore = score;
      newObject.hitPlayer = hitPlayer;
      newObject.playerMovements = playerMovements;
      newObject.spawnPos = spawn;
      newObject.targetPos = target;
      newObject.newPlayerPos = playerPos;
      newObject.isCollisionObject = isCollision;
      
      spawnObjectList.Add(newObject);
   }
}

public class SpawnObjectInfo
{
   public float playerScore;
   public int playerMovements;
   public bool isCollisionObject;
   public bool hitPlayer;
   public Vector3 spawnPos;
   public Vector3 targetPos;
   public Vector3 newPlayerPos;
}

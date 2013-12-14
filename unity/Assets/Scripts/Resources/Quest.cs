using UnityEngine;
using System.Collections;

//Quests have one or more Checkpoints
//Checkpoints consist of one or more ordered Objectives
//Objectives consist of one or more unordered Tasks

public class Quest : MonoBehaviour
{
	public Checkpoint[] checkpoints;

	// Use this for initialization
	void Awake()
	{
		checkpoints[0].Activate();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void CompleteCheckpoint()
	{
		for(int i = 0; i < checkpoints.Length; i++)
		{
			if(!checkpoints[i].activeForQuest)
			{
				checkpoints[i].Activate();
				return;
			}
			else if(!checkpoints[i].completed)
			{
				return;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

//Quests have one or more Checkpoints
//Checkpoints consist of one or more ordered Objectives
//Objectives consist of one or more unordered Tasks

public class Quest : MonoBehaviour
{
	public Checkpoint[] checkpoints;
	bool questActive = false;

	// Update is called once per frame
	void Update () 
	{
		if(!questActive)
		{
			questActive = true;
			checkpoints[0].Activate();
		}
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

		GameObject.Find("questcomplete").GetComponent<SpriteRenderer>().enabled = true;
		GameObject.Find("portalwords").GetComponent<SpriteRenderer>().enabled = false;
		GameObject.Find("portalcursor").GetComponent<SpriteRenderer>().enabled = false;
	}
}

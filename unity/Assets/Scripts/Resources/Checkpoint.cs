using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour 
{
	public Objective[] objectives;
	public Quest checkpointOwner;
	public bool completed = false;
	public bool activeForQuest = false;

	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Activate()
	{
		this.activeForQuest = true;
		objectives[0].Activate();
	}

	public void CompleteObjective()
	{
		for(int i = 0; i < objectives.Length; i++)
		{
			if(!objectives[i].activeForQuest)
			{
				objectives[i].Activate();
				return;
			}
			else if(!objectives[i].completed)
			{
				return;
			}
		}

		completed = true;
		checkpointOwner.CompleteCheckpoint();
	}
}

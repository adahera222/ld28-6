using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour 
{
	public Task[] tasks;
	public Checkpoint objectiveOwner;
	public bool completed = false;
	public bool activeForQuest = false;


	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Activate()
	{
		this.activeForQuest = true;
		for(int i = 0; i < tasks.Length; i++)
		{
			tasks[i].Activate();
		}
	}

	public void CompleteTask()
	{
		for(int i = 0; i < tasks.Length; i++)
		{
			if(!tasks[i].completed)
			{
				return;
			}
		}

		completed = true;
		objectiveOwner.CompleteObjective();
	}
}

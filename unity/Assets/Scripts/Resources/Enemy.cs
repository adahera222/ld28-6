using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public float hp = 1;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(hp <= 0)
		{
			Task tsk = this.GetComponent<Task>();
			if(tsk && tsk.taskOwner && tsk.taskOwner.activeForQuest)
			{
				tsk.Complete();
			}

			GameObject.Find("mainball").audio.PlayOneShot(this.audio.clip);

			this.gameObject.SetActive(false);
		}
	}

	public void Damage(int dmg)
	{
		hp -= dmg;
	}
}

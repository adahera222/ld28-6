using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		GameObject player = GameObject.Find ("player");
		if(other.gameObject == player)
		{
			Task tsk = this.GetComponent<Task>();
			if(tsk)
			{
				player.audio.PlayOneShot(this.audio.clip, this.audio.volume);
				tsk.Complete ();
				this.gameObject.SetActive(false);
			}
		}
	}
}

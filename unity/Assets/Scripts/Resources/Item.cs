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
		if(other.gameObject == GameObject.Find ("player"))
		{
			Task tsk = this.GetComponent<Task>();
			if(tsk)
			{
				tsk.Complete ();
				this.gameObject.SetActive(false);
			}
		}
	}
}

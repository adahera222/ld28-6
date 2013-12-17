using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour 
{
	bool hit = false;

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
		GameObject ball = GameObject.Find("mainball");
		if(other.gameObject == ball)
		{
			ball.transform.position = this.transform.position;
			ball.rigidbody2D.isKinematic = true;

			GameObject player = GameObject.Find("player");
			player.transform.position = this.transform.position;
			player.rigidbody2D.isKinematic = true;

			if(!hit)
			{
				player.audio.PlayOneShot(this.audio.clip, this.audio.volume);
				hit = true;

				GameObject.Find ("portalhelper").GetComponent<Task>().Complete ();
			}
		}
	}
}

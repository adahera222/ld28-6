using UnityEngine;
using System.Collections;

public class MovePlayerToBall : MonoBehaviour 
{
	float stoppedTime = 0;
	float neededStoppedTime = 1;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameObject player = GameObject.Find("player");
		float dist = Vector2.Distance(this.transform.position, player.transform.position);
		float speed = this.rigidbody2D.velocity.magnitude;
		if(speed < .01 && dist  > .22  && !player.GetComponent<PlayerController>().isDead())
		{
			if(stoppedTime == 0)
			{
				stoppedTime  = Time.time;
			}

			if(Time.time - stoppedTime >= neededStoppedTime)
			{
				player.transform.position = this.transform.position + new Vector3(-.05f, .18f, 0);
				this.GetComponent<Ball>().canBeHit = true;
				stoppedTime = 0;
			}
		}
		else
		{
			stoppedTime = 0;
		}
	}
}

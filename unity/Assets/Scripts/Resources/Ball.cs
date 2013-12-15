using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
	public bool canBeHit;

	float sleepTime = 0;
	float neededSleepTime = .33f;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(this.rigidbody2D.velocity.magnitude < .05)
		{
			if(sleepTime == 0)
			{
				sleepTime = Time.time;
			}

			if(Time.time - sleepTime >= neededSleepTime)
			{
				this.rigidbody2D.velocity *= 0;
				this.rigidbody2D.Sleep();

				sleepTime = 0;
			}
		}
		else
		{
			sleepTime = 0;
			canBeHit = false;
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		Enemy en = coll.gameObject.GetComponent<Enemy>();

		if(en)
		{
			en.Damage(1);
		}

		audio.PlayOneShot(audio.clip);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log("trigger enter");
		this.rigidbody2D.drag = 1;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(this.rigidbody2D.velocity.magnitude < .25f)
		{
			this.rigidbody2D.velocity *= .75f;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		this.rigidbody2D.drag = .1f;
	}
}

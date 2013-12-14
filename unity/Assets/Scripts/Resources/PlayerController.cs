using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float maxSpeed;
	public float strikingPower;
	public float swingDuration;
	
	float hitForce = 0;
	float timeHitStarted = 0;

	float hitAxis = 1;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//float force = maxSpeed * this.rigidbody2D.mass * this.rigidbody2D.drag * Time.smoothDeltaTime;
		float axis = Input.GetAxis("Horizontal");
		if(axis < 0 && hitAxis > 0)
		{
			this.Flip();
		}
		else if(axis > 0 && hitAxis < 0)
		{
			this.Flip();
		}

		axis = Input.GetAxis("Fire1");
		GameObject ball = GameObject.Find("mainball");
		if(axis != 0 && ball.GetComponent<Ball>().canBeHit)
		{
			hitForce += Time.smoothDeltaTime * strikingPower;

			if(timeHitStarted == 0)
			{
				timeHitStarted = Time.time;
			}
			else if(Time.time - timeHitStarted > swingDuration)
			{
				this.hitBall();
			}
		}
		else if(hitForce > 0)
		{
			this.hitBall();
		}
	}

	void hitBall()
	{
		GameObject ball = GameObject.Find("mainball");
		
		Vector2 dir = new Vector2(1, 1).normalized;
		dir.Scale(new Vector2(hitForce * hitAxis, hitForce));

		ball.rigidbody2D.AddForce(dir);

		hitForce = 0;
		timeHitStarted = 0;
	}

	void Flip()
	{
		hitAxis *= -1;
		Vector3 scale = this.transform.localScale;
		scale.x *= -1;
		this.transform.localScale = scale;
	}
}

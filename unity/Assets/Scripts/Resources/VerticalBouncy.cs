using UnityEngine;
using System.Collections;

public class VerticalBouncy : MonoBehaviour 
{
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
		Ball ballcomp = ball.GetComponent<Ball>();
		if(ball == other.gameObject && ballcomp.canBounce)
		{
			//Debug.Log(ball.rigidbody2D.velocity);
			ballcomp.canBounce = false;
			ball.rigidbody2D.velocity = new Vector2(ball.rigidbody2D.velocity.x, 0);
			ball.rigidbody2D.AddForce(new Vector2((ball.rigidbody2D.velocity.x > 0) ? 2 : -2, 160));
			//Debug.Log(ball.rigidbody2D.velocity);
			this.GetComponent<Animator>().SetInteger("state", 1);
			this.audio.PlayOneShot(this.audio.clip);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		GameObject ball = GameObject.Find("mainball");
		if(ball == other.gameObject)
		{
			//Debug.Log ("vertical bouncy trigger exit");
		}
	}

	void EndBouncyAnimation()
	{
		this.GetComponent<Animator>().SetInteger("state", 0);
	}
}

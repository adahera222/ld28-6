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
		if(ball == other.gameObject)
		{
			Debug.Log(ball.rigidbody2D.velocity);
			ball.rigidbody2D.velocity = new Vector2(ball.rigidbody2D.velocity.x, 0);
			ball.rigidbody2D.AddForce(new Vector2((ball.rigidbody2D.velocity.x > 0) ? 1 : -1, 150));
			Debug.Log(ball.rigidbody2D.velocity);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		GameObject ball = GameObject.Find("mainball");
		if(ball == other.gameObject)
		{
			Debug.Log ("vertical bouncy trigger exit");
		}
	}
}

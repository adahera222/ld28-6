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

	bool clubSwitchDown = false;

	float baseCameraOrtho = 5;

	AudioClip swingAudio;

	// Use this for initialization
	void Start () 
	{
		swingAudio = Resources.Load<AudioClip>("swing");
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

		ClubBag bag = GameObject.Find("clubbag").GetComponent<ClubBag>();
		axis = Input.GetAxis("Vertical");
		if(axis != 0 && !clubSwitchDown)
		{
			clubSwitchDown = true;


			if(axis > 0)
			{
				bag.PreviousClub();
			}
			else
			{
				bag.NextClub();
			}
		}
		else if(axis == 0)
		{
			clubSwitchDown = false;
		}

		GameObject cam = GameObject.Find("Main Camera");
		axis = Input.GetAxis("MouseDown");
		if(axis != 0)
		{
			float mouseDragX = Input.GetAxis("Mouse X");
			float mouseDragY = Input.GetAxis("Mouse Y");
			Vector3 pos = cam.transform.position;
			pos.x -= mouseDragX;
			pos.y -= mouseDragY;
			cam.transform.position = pos;
		}

		axis = Input.GetAxis("Mouse ScrollWheel");
		if(axis != 0)
		{
			cam.GetComponent<Camera>().orthographicSize -= axis;
		}

		float ortho = cam.GetComponent<Camera>().orthographicSize;
		bag.gameObject.transform.position = cam.transform.position + new Vector3(ortho * 1.2f, ortho * -.9f, -cam.transform.position.z);
		float scale = ortho / baseCameraOrtho;
		bag.gameObject.transform.localScale = new Vector3(scale, scale, 1);
	}

	void hitBall()
	{
		GameObject ball = GameObject.Find("mainball");

		ClubBag bag = GameObject.Find("clubbag").GetComponent<ClubBag>();
		Vector2 dir = bag.CurrentClubVector();
		dir.Scale(new Vector2(hitForce * hitAxis, hitForce));

		ball.rigidbody2D.AddForce(dir);

		hitForce = 0;
		timeHitStarted = 0;

		audio.PlayOneShot(swingAudio);
	}

	void Flip()
	{
		hitAxis *= -1;
		Vector3 scale = this.transform.localScale;
		scale.x *= -1;
		this.transform.localScale = scale;
	}
}

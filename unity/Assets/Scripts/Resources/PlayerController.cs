﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float maxSpeed;
	public float strikingPower;
	public float swingDuration;

	int state = 0;
	
	float hitForce = 0;
	float timeHitStarted = 0;

	float hitAxis = 1;

	bool clubSwitchDown = false;

	float baseCameraOrtho = 5;

	AudioClip swingAudio;

	Animator playerAnimator;

	// Use this for initialization
	void Start () 
	{
		swingAudio = Resources.Load<AudioClip>("swing");

		playerAnimator = this.GetComponent<Animator>();
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
		Ball ballComp = ball.GetComponent<Ball>();
		if(axis != 0 && ballComp.canBeHit)
		{
			hitForce += Time.smoothDeltaTime * strikingPower;

			if(timeHitStarted == 0)
			{
				timeHitStarted = Time.time;
				state = 1;
				playerAnimator.SetInteger("state", 1);
			}
			else if(Time.time - timeHitStarted >= swingDuration)
			{
				state = 2;
				playerAnimator.SetInteger("state", 2);
			}
		}
		else if(hitForce > 0)
		{
			state = 2;
			playerAnimator.SetInteger("state", 2);
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
		if(axis != 0 && ballComp.canBeHit)
		{
			float mouseDragX = Input.GetAxis("Mouse X");
			float mouseDragY = Input.GetAxis("Mouse Y");
			Vector3 pos = cam.transform.position;
			pos.x -= mouseDragX;
			pos.y -= mouseDragY;
			cam.transform.position = pos;
		}

		axis = Input.GetAxis("Mouse ScrollWheel");
		if(axis != 0 && ballComp.canBeHit)
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

		audio.PlayOneShot(swingAudio);
	}

	void Flip()
	{
		hitAxis *= -1;
		Vector3 scale = this.transform.localScale;
		scale.x *= -1;
		this.transform.localScale = scale;
	}

	void setIdleState()
	{
		hitForce = 0;
		timeHitStarted = 0;
		state = 0;
		playerAnimator.SetInteger("state", 0);
	}
}

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float maxSpeed;
	public float strikingPower;
	public float swingDuration;
	public float hp = 1;

	int state = 0;
	
	float hitForce = 0;
	float timeHitStarted = 0;

	float hitAxis = 1;

	bool clubSwitchDown = false;

	float baseCameraOrtho;

	AudioClip swingAudio;

	Animator playerAnimator;

	//bool hasTeleport = true;

	// Use this for initialization
	void Start () 
	{
		swingAudio = Resources.Load<AudioClip>("swing");

		playerAnimator = this.GetComponent<Animator>();

		baseCameraOrtho = GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(hp <= 0 && state < 100)
		{
			this.Die ();
		}

		//movement
		float axis = Input.GetAxis("Horizontal");
		if(axis < 0 && hitAxis > 0)
		{
			this.Flip();
		}
		else if(axis > 0 && hitAxis < 0)
		{
			this.Flip();
		}

		//hit ball
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

		//clubs
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

		//reload level / confirm
		axis = Input.GetAxis("Confirm");
		if(axis != 0)
		{
			Application.LoadLevel(Application.loadedLevel);
		}

		//camera
		GameObject cam = GameObject.Find("Main Camera");
		float ortho = cam.GetComponent<Camera>().orthographicSize;
		axis = Input.GetAxis("MouseDown");
		if(axis != 0 && ballComp.canBeHit)
		{
			float mouseDragX = Input.GetAxis("Mouse X");
			float mouseDragY = Input.GetAxis("Mouse Y");
			Vector3 camPos = cam.transform.position;
			camPos.x -= mouseDragX;
			camPos.y -= mouseDragY;

			if(camPos.x < 8.18)
			{
				camPos.x = 8.18f;
			}
			if(camPos.x > 21.269)
			{
				camPos.x = 21.269f;
			}
			
			if(camPos.y < 5.66)
			{
				camPos.y = 5.66f;
			}
			if(camPos.y > 12.63)
			{
				camPos.y = 12.63f;
			}

			cam.transform.position = camPos;
		}

		/*axis = Input.GetAxis("Mouse ScrollWheel");
		if(axis != 0 && ballComp.canBeHit)
		{
			cam.GetComponent<Camera>().orthographicSize -= axis;
		}*/

		//ui
		if(Input.GetKeyDown (KeyCode.H))
		{
			GameObject help = GameObject.Find ("help");
			SpriteRenderer ren = help.GetComponent<SpriteRenderer>();

			ren.enabled = !ren.enabled;

			help.transform.position = this.transform.position + new Vector3(0, 2.1f, 0);
		}

		bag.gameObject.transform.position = cam.transform.position + new Vector3(ortho * 1.2f, ortho * -.9f, -cam.transform.position.z);
		float scale = ortho / baseCameraOrtho;
		bag.gameObject.transform.localScale = new Vector3(scale, scale, 1);

		//cheat move
		if(Input.GetMouseButtonDown(1))
		{
			Vector3 worldPos = cam.camera.ScreenToWorldPoint(Input.mousePosition);
			Vector3 pos = this.gameObject.transform.position;
			pos.x = worldPos.x;
			pos.y = worldPos.y;
			this.gameObject.transform.position = pos;

			ball.gameObject.transform.position = this.gameObject.transform.position;
			ball.gameObject.rigidbody2D.velocity = new Vector3(0, 0, 0);
		}
	}

	void hitBall()
	{
		GameObject ball = GameObject.Find("mainball");

		ClubBag bag = GameObject.Find("clubbag").GetComponent<ClubBag>();
		Vector2 dir = bag.CurrentClubVector();
		dir.Scale(new Vector2(hitForce * hitAxis, hitForce));

		ball.rigidbody2D.AddForce(dir);

		this.PlaySwing();
		ball.audio.PlayOneShot(ball.audio.clip);
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

	void PlaySwing()
	{
		audio.PlayOneShot(swingAudio);
	}

	void Die()
	{
		this.gameObject.layer = 31;

		state = 100;
		playerAnimator.SetInteger("state", 100);

		GameObject qf = GameObject.Find ("questfailedhelper");
		qf.transform.position = this.transform.position + new Vector3(0, 1, 0);

		GameObject.Find ("questfailed").GetComponent<SpriteRenderer>().enabled = true;
		GameObject.Find ("restartwords").GetComponent<SpriteRenderer>().enabled = true;
	}
	
	void Dying()
	{
		state = 101;
		playerAnimator.SetInteger("state", 101);
	}

	void OnGUI()
	{
		GUI.Label (new Rect(10, 10, 100, 20), "Press H for Help");

		string clubName = GameObject.Find("clubbag").GetComponent<ClubBag>().CurrentClubName();
		GUI.Label (new Rect(Screen.width - 110, Screen.height - 50, 100, 20), clubName);

		if(GUI.Button (new Rect(Screen.width - 150, 30, 140, 30), "Greenlight on Steam!"))
		{
			Application.OpenURL("http://steamcommunity.com/sharedfiles/filedetails/?id=204635252");
		}
	}

	public void Damage(int dmg)
	{
		hp -= dmg;
	}

	public bool isDead()
	{
		return (state >= 100);
	}
}

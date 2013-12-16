using UnityEngine;
using System.Collections;

public class Slime : Enemy 
{
	float shootDelay = 3;
	float shootTimer = 0;
	int shots = 3;
	float startAngle = -30;

	float shotSpeed = 100;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		shootTimer += Time.deltaTime;
		if(shootTimer >= shootDelay)
		{
			shootTimer = 0;

			int shotsMinusOne = shots - 1;
			float angleIncrement = Mathf.Abs(startAngle * 2) / ((shotsMinusOne > 0) ? shotsMinusOne : 1);
			float angle = startAngle;
			for(int i = 0; i < shots; i++)
			{
				GameObject slimeball = (GameObject)Instantiate(Resources.Load("slimeball"));
				slimeball.transform.position = this.transform.position;

				Vector2 vec = new Vector2(0, 1);
				if(angle != 0)
				{
					vec = Quaternion.Euler(0, 0, angle) * vec;
				}

				vec *= shotSpeed;

				slimeball.rigidbody2D.AddForce(vec);

				angle += angleIncrement;
			}
		}
	}
}

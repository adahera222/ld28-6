using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour 
{
	public float lifeTime = 2;
	public int damage = 1;
	float timeAlive;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeAlive += Time.deltaTime;
		if(timeAlive >= lifeTime)
		{
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		GameObject player = GameObject.Find("player");
		PlayerController pc = player.GetComponent<PlayerController>();
		if(coll.gameObject == player && !pc.isDead())
		{
			Destroy(this.gameObject);
			pc.Damage(damage);
		}
	}
}

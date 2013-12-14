using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public float hp = 1;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(hp <= 0)
		{
			Destroy(this.gameObject);
		}
	}

	public void Damage(int dmg)
	{
		hp -= dmg;
	}
}

using UnityEngine;
using System.Collections;

public class Task : MonoBehaviour 
{
	public string description;
	public GameObject target;
	public Objective owner;

	public bool completed = false;

	// Use this for initialization
	void Awake()
	{
		this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Activate()
	{
		this.gameObject.SetActive(true);
	}

	public void Complete()
	{
		completed = true;
		owner.CompleteTask();
	}
}

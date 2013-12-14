using UnityEngine;
using System.Collections;

public enum ClubTypes
{
	ClubTypeDriver = 0,
	ClubTypeWedge,
	ClubTypeMax
};

public class ClubBag : MonoBehaviour 
{
	ClubTypes currentClub = ClubTypes.ClubTypeDriver;
	Vector2[] clubVectors;
	string clubSpriteBaseName = "testclubs";
	Sprite currentClubSprite;

	// Use this for initialization
	void Start () 
	{
		clubVectors = new Vector2[(int)ClubTypes.ClubTypeMax];

		Vector3 vec = new Vector3(1, 0, 0);
		vec = Quaternion.Euler(0, 0, -30) * vec;
		clubVectors[(int)ClubTypes.ClubTypeDriver] = new Vector2(vec.x, vec.y).normalized;

		vec.x = 1;
		vec.y = 0;
		vec.z = 0;
		vec = Quaternion.Euler(0, 0, -70) * vec;

		clubVectors[(int)ClubTypes.ClubTypeWedge] = new Vector2(vec.x, vec.y).normalized;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void UpdateUI()
	{
		Object[] texs = Resources.LoadAll<Sprite>(clubSpriteBaseName);
		this.GetComponent<SpriteRenderer>().sprite = (Sprite)texs[(int)currentClub];
	}

	public void NextClub()
	{
		currentClub++;
		if(currentClub >= ClubTypes.ClubTypeMax)
		{
			currentClub = 0;
		}

		this.UpdateUI();
	}

	public void PreviousClub()
	{
		currentClub--;
		if(currentClub < 0)
		{
			currentClub = ClubTypes.ClubTypeMax - 1;
		}

		this.UpdateUI();
	}

	public Vector2 CurrentClubVector()
	{
		return clubVectors[(int)currentClub];
	}
}

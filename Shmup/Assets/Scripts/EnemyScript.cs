using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	private WeaponScript[] weapons;

	void Awake () 
	{
		weapons = GetComponentsInChildren<WeaponScript>();
	}


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(WeaponScript weapon in weapons) 
		{
			// Autofire
			if (weapon != null && weapon.CanAttack) 
			{
				weapon.Attack(true);
			}
		}
	}
}

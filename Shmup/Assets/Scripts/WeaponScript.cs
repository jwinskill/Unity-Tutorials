﻿using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

	public Transform shotPrefab;
	public float shootingRate = 0.25f;
	private float shootCooldown;

	// Use this for initialization
	void Start () 
	{
		shootCooldown = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (shootCooldown > 0) 
		{
			shootCooldown -= Time.deltaTime;
		}
	}

	public void Attack(bool isEnemy) 
	{
		if (CanAttack) {
			shootCooldown = shootingRate;

			// Create a new shot
			var shotTransform = Instantiate (shotPrefab) as Transform;

			// Assign position
			shotTransform.position = transform.position;

			// get the isEnemy property
			ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript> ();

			if (shot != null) {
				shot.isEnemyShot = isEnemy;
			}

			// Make the shot shoot to the right
			MoveScript move = shotTransform.gameObject.GetComponent<MoveScript> ();
			if (move != null) {
				move.direction = this.transform.right;
			}
		}
	}

	public bool CanAttack {
		get {
			return shootCooldown <= 0f;
		}
	}
}
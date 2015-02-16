using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public int hp = 1;
	public bool isEnemy = true;

	public void Damage(int damageCount)
	{
		hp -= damageCount;

		if (hp <= 0) 
		{
			Destroy(gameObject);
		}

	}

	void OnTriggerEnter2D (Collider2D otherCollider)
	{
		// Is this a shot?
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript> ();
		if (shot != null) 
		{
			// Avoid friendly fire
			if (shot.isEnemyShot != isEnemy) 
			{
				Damage(shot.damage);

				// Destroy the shot
				Destroy(shot.gameObject);
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class ScrollingScript : MonoBehaviour {

	/// <summary>
	/// Scrolling Speed
	/// </summary>
	public Vector2 speed = new Vector2(2, 2);

	/// <summary>
	/// Moving Direction
	/// </summary>
	public Vector2 direction = new Vector2(-1, 0);

	/// <summary>
	/// Movement should be applied to camera
	/// </summary>
	public bool isLinkedToCamera = false;

	/// <summary>
	/// 1 - Background is infinite
	/// </summary>
	public bool isLooping = false;

	/// <summary>
	/// 2 - List of children with a renderer
	/// </summary>
	private List<Transform> backgroundPart;

	// Use this for initialization
	void Start () 
	{
		if (isLooping) {
			// Get all the children of the layer with a renderer
			backgroundPart = new List<Transform> ();

			for (int i = 0; i < transform.childCount; i++) {
				Transform child = transform.GetChild (i);

				// Add only the visible children
				if (child.renderer != null) {
					backgroundPart.Add (child);
				}
			}
			backgroundPart = backgroundPart.OrderBy(t => t.position.x).ToList();
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Movement
		Vector3 movement = new Vector3 (speed.x * direction.x, speed.y * direction.y, 0);

		movement *= Time.deltaTime;
		transform.Translate(movement);

		// Move the camera
		if (isLinkedToCamera) 
		{
			Camera.main.transform.Translate (movement);
		}

		if (isLooping) {
			Transform firstChild = backgroundPart.FirstOrDefault();

			if (firstChild != null) {
				if (firstChild.position.x < Camera.main.transform.position.x) {
					if (firstChild.renderer.IsVisibleFrom(Camera.main) == false) {
						Transform lastChild = backgroundPart.LastOrDefault();
						Vector3 lastPosition = lastChild.transform.position;
						Vector3 lastSize = (lastChild.renderer.bounds.max - lastChild.renderer.bounds.min);

						firstChild.position = new Vector3(lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);

						backgroundPart.Remove(firstChild);
						backgroundPart.Add(firstChild);
					}
				}
			}
		}
	}
}

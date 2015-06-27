using UnityEngine;
using System.Collections;

public class DrawPool : MonoBehaviour {
	public Transform Pool;
	public Color color = Color.magenta;
	private Transform instantiatedPool;
	public bool PaintWithClickMouse = false;
	public float DestroyIn = 5F;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (PaintWithClickMouse) {
			if (!Input.GetMouseButton (0)) {
				return;
			}
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				Paint (hit.point, Quaternion.LookRotation (hit.normal), color); // Step back a little
			}
		} else {
			if (Input.GetMouseButtonDown(0)){
				Paint (transform.position, transform.rotation, color); // Step back a little
			}
		}

	}

	public void Paint(Vector3 location, Quaternion Rotation, Color color){
		instantiatedPool = Instantiate(Pool, new Vector3(location.x, location.y, location.z), Rotation) as Transform;

		Destroy (instantiatedPool.gameObject, DestroyIn);
	}


}

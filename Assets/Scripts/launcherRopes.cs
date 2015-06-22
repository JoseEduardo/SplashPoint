using UnityEngine;
using System.Collections;

public class launcherRopes : MonoBehaviour {
	public GameObject RopePrefab = null;
	private GameObject Rope = null;
	private bool Go = false;
	public float MaxRange = 5F;
	public float MaxRangeZ = 5F;
	private Vector3 GoToPos;
	private float t;
	private float factor = 1F;
	public float moveSpeed = 3f;
//	private float distance = 4.5F;

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire2")) {
			if(!Go){
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray ,out hit)){
					Rope = Instantiate(RopePrefab, new Vector3(transform.position.x, transform.position.y+0.5F, transform.position.z)  , Quaternion.identity) as GameObject;
					Debug.Log(hit.transform.tag);

					GoToPos = hit.point;
					MaxRangeZ = Rope.transform.localScale.z-MaxRange;
					Rope.transform.LookAt ( GoToPos );
					Go = true;
				}
			}
		}

		if (Go) {
			if(Rope.transform.localScale.z >= MaxRangeZ){
				Rope.transform.localScale = new Vector3 (Rope.transform.localScale.x, Rope.transform.localScale.y, Rope.transform.localScale.z-0.1F);
			}else{
				Rope.transform.position = GoToPos;
				Rope.transform.LookAt ( transform.position );
				GetComponent<Rigidbody>().AddExplosionForce(10.0F, GoToPos, 5.0F, 3.0F);
				Destroy( Rope );
				transform.LookAt ( GoToPos );
				Go = false;
				StartCoroutine(move(transform));
			}
		}

	}

	public IEnumerator move(Transform transform) {
		t = 0F;
		while (t < 1f) {
			t += Time.deltaTime * (moveSpeed) * factor;
			transform.position = Vector3.Lerp(transform.position, GoToPos, t);
			yield return null;
		}
	}

}

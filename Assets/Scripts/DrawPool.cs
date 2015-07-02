using UnityEngine;
using System.Collections;

public class DrawPool : MonoBehaviour {
	public Transform Pool;
	public Color color = Color.magenta;
	private Transform instantiatedPool;
	public bool PaintWithClickMouse = false;
	public float DestroyIn = 5F;
	public Light DirLight;
	private bool lighting = false;

	private Animator animator;
	public bool isStunned;

	//Warrior types
	public enum Warrior{Karate, Ninja, Brute, Sorceress};
	
	public Warrior warrior;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		//DirLight = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown( KeyCode.I )){
			if(isStunned){
				isStunned = false;
			}else{
				isStunned = true;
			}
		}

		if (!isStunned) {
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
				if (Input.GetButtonDown ("Fire1")) {
					animator.SetTrigger ("Attack1Trigger");
					if (warrior == Warrior.Brute)
						StartCoroutine (COStunPause (1.2f));
					else if (warrior == Warrior.Sorceress)
						StartCoroutine (COStunPause (1.2f));
					else
						StartCoroutine (COStunPause (.6f));

					StartCoroutine (COSPaint (.5f));
				}

				// AUMENTAR A LUZ QUE ENVOLVE O PERSONAGEM
				if (Input.GetButtonDown ("Fire2")) {
					isStunned = true;
					lighting = true;
				}

			}
		}else{
			//CONTROLE PARA AUMENTAR A LUZ QUE ENVOLVE O PERSONAGEM
			if(lighting){
				if(DirLight.range <= 250){
					DirLight.range += 5;
				}else{
					DirLight.range = 10;
					isStunned = false;
					lighting = false;
				}
			}
		}

	}

	public IEnumerator COSPaint(float pauseTime)
	{
		yield return new WaitForSeconds(pauseTime);
		Paint (transform.position, transform.rotation, color);
	}

	public IEnumerator COStunPause(float pauseTime)
	{
		isStunned = true;
		yield return new WaitForSeconds(pauseTime);
		isStunned = false;
	}

	public void Paint(Vector3 location, Quaternion Rotation, Color color){
		instantiatedPool = Instantiate(Pool, new Vector3(location.x, location.y, location.z), Rotation) as Transform;

		Destroy (instantiatedPool.gameObject, DestroyIn);
	}


}

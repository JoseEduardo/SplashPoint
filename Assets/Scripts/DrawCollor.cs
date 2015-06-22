using UnityEngine;
using System.Collections;

public class DrawCollor : MonoBehaviour {
	public Camera cam;
	private Texture2D clone;
	private Texture2D oldTexture = null;
	private Renderer rend = null;

	void Start() {
		//cam = GetComponent<Camera>();
	}

	void OnApplicationQuit() {
		if (oldTexture != null) {
			rend.sharedMaterial.mainTexture = oldTexture;
		}
	}

	void Update() {
		if (!Input.GetMouseButton (0)) {
			return;
		}
		
		RaycastHit hit;
		if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
			return;

		rend = hit.transform.GetComponent<Renderer>();
		MeshCollider meshCollider = hit.collider as MeshCollider;

		if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
			return;

		clone = Instantiate(rend.sharedMaterial.mainTexture) as Texture2D;
		if (oldTexture == null) {
			oldTexture = rend.sharedMaterial.mainTexture as Texture2D;
		}
		rend.sharedMaterial.mainTexture = clone;

		Vector2 pixelUV = hit.textureCoord;

		pixelUV.x *= clone.width;
		pixelUV.y *= clone.height;
		for (int y =0; y<60; y++) {
			for (int x = 0; x<60; x++) {
				clone.SetPixel ((int)pixelUV.x+x, (int)pixelUV.y+y, Color.magenta);
				
			}
		}

		clone.Apply();
	}
}
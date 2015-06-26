using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public  GameObject[] ListChamps;

	// Update is called once per frame
	public void StartGame () {
		//CHAMAR PROXIMA CENA E INSTANCIAR OS PLAYERS E MAP
		Instantiate(ListChamps[	GetComponent<RotateChamps>().IDChamp ], new Vector3(2.0F, 0, 0), Quaternion.identity);	
	}
}

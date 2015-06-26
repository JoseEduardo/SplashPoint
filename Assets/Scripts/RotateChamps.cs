using UnityEngine;
using System.Collections;

public class RotateChamps : MonoBehaviour {
	public Transform Champ1;
	public Transform Champ2;
	public Transform Champ3;
	public Transform Champ4;
	public int IDChamp = 0;

	public Transform ChampContainer;

	// Update is called once per frame
	public void RotateLeft () {
		ChampContainer.Rotate (0, ChampContainer.rotation.y + 90F, 0);

		if (IDChamp == 3) {
			IDChamp = 0;
		}else{
			IDChamp += 1;
		}

		Champ1.Rotate(0, Champ1.rotation.y-90F, 0);
		Champ2.Rotate(0, Champ2.rotation.y-90F, 0);
		Champ3.Rotate(0, Champ3.rotation.y-90F, 0);
		Champ4.Rotate(0, Champ4.rotation.y-90F, 0);
	}

	public void RotateRight () {
		ChampContainer.Rotate (0, ChampContainer.rotation.y - 90F, 0);

		if (IDChamp == 3) {
			IDChamp = 0;
		}else{
			IDChamp -= 1;
		}

		Champ1.Rotate(0, Champ1.rotation.y+90F, 0);
		Champ2.Rotate(0, Champ2.rotation.y+90F, 0);
		Champ3.Rotate(0, Champ3.rotation.y+90F, 0);
		Champ4.Rotate(0, Champ4.rotation.y+90F, 0);
	}


}
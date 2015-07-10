using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour {
	public  GameObject[] ListChamps;
	private int HeroSelected = -1;
	public string Name;
	public GameObject Player = null;
	public InputField IPServer;
	public InputField NamePlayer;
	public Text NetInfos;

	/*---------------------------*/
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		Debug.Log("This SERVER OR CLIENT has disconnected from a server");
	}
	
	void OnFailedToConnect(NetworkConnectionError error){
		Debug.Log("Could not connect to server: "+ error.ToString());
	}

	//Server functions called by Unity
	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player connected from: " + player.ipAddress +":" + player.port);
		SpawnPlayer();
	}
	
	void OnServerInitialized() {
		Debug.Log("Server initialized and ready");
	}
	
	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Player disconnected from: " + player.ipAddress+":" + player.port);
	}

	void OnFailedToConnectToMasterServer(NetworkConnectionError info){
		Debug.Log("Could not connect to master server: "+ info);
	}
	
	void OnNetworkInstantiate (NetworkMessageInfo info) {
		Debug.Log("New object instantiated by " + info.sender);
	}

	void OnLevelWasLoaded () {
		if(PlayerPrefs.GetString ("Player_type") == "host"){
			PlayerPrefs.SetString ("Player_type", "");
			SpawnPlayer();
		}
	}

	/*--------------------------*/

	void SpawnPlayer(){
		HeroSelected = PlayerPrefs.GetInt ("Player_Warrior");
		Debug.Log (HeroSelected);
		if (HeroSelected >= 0) {
			Name = PlayerPrefs.GetString ("Player_Name");
			PlayerPrefs.SetString("Player_Name", "");
			PlayerPrefs.SetInt("Player_Warrior", -1);
			Player = (GameObject)Network.Instantiate(ListChamps[HeroSelected].transform, new Vector3(2.0F, 0, 0), Quaternion.identity, 1);

			//Player = Instantiate(ListChamps[HeroSelected], new Vector3(2.0F, 0, 0), Quaternion.identity) as GameObject;
			Player.SetActive(true);
			GameObject.FindGameObjectsWithTag("Name")[0].GetComponent<TextMesh>().text = Name;			
			HeroSelected = -1;

			NetInfos.text = Network.player.ipAddress;
		}
	}

	public void StartGame () {
		if (NamePlayer.text != "") {
			PlayerPrefs.SetInt ("Player_Warrior", GetComponent<RotateChamps> ().IDChamp);
			PlayerPrefs.SetString ("Player_Name", NamePlayer.text); // obter o nome selecionado

			if (IPServer.text == "") {
				Network.InitializeServer (10, 35000, false);
				if (Network.peerType != NetworkPeerType.Disconnected) {
					PlayerPrefs.SetString ("Player_type", "host");
					Application.LoadLevel ("Principal");
				}
			}else{
				Network.Connect(IPServer.text, 35000);
				PlayerPrefs.SetString ("Player_type", "player");
				Application.LoadLevel ("Principal");
			}
		}

	}
}

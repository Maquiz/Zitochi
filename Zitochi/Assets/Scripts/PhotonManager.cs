using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonManager : MonoBehaviour {
    public GameObject player;
    [SerializeField] private GameObject lobbyCamera;
	// Use this for initialization
	void Start () {
        PhotonNetwork.ConnectUsingSettings ();
	}

    void OnJoinedLobby() {
        PhotonNetwork.JoinOrCreateRoom("Room", new Photon.Realtime.RoomOptions() { MaxPlayers = 2 }, Photon.Realtime.TypedLobby.Default);
    }

    void OnJoinedRoom() {
        PhotonNetwork.Instantiate(player.name, player.transform.position,Quaternion.identity ,0);

    }

}

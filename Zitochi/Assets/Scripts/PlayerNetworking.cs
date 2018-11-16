using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class PlayerNetworking : MonoBehaviour {

    [SerializeField] private GameObject PlayerCamera;
    [SerializeField] private MonoBehaviour[] scriptsToIgnore;

    private PhotonView photonView;
    
    // Use this for initialization
	void Start () {
        photonView = GetComponent<PhotonView>();
        Initialize();
	}

    void Initialize() {
        if (photonView.IsMine) {

        }
        else {
            PlayerCamera.SetActive(false);
            foreach (MonoBehaviour item in scriptsToIgnore) {
                item.enabled = false;
            }
        }
    }
}

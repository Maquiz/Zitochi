using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}

    public void worldGo(int w) {
        if (w == 1) {
            SceneManager.LoadScene("Rock World 1");
        }
    }
}

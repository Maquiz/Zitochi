using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
    public class Restarter : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player" || other.tag == "Team1")
            {
                other.transform.position =  new Vector3(22.3f, 30.2f, 0f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}

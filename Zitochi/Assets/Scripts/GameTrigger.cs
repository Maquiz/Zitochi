using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTrigger : MonoBehaviour {

    public Animator transitionAnim;
    bool sending;
    private void Start()
    {
        sending = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Team1" && sending == false) {
            DontDestroyOnLoad(collision.gameObject);
            sending = true;
          // transitionAnim.SetTrigger("end");
         //  SceneManager.LoadScene("Level3");
           //collision.gameObject.transform.position = new Vector3(22.3f, 6.2f, 0f);
            StartCoroutine(LoadScene(collision.gameObject));

        }
    }

    IEnumerator LoadScene(GameObject g) {
      
       
        transitionAnim.SetTrigger("end");
        SceneManager.LoadScene("Level3");
        g.transform.position = new Vector3(22.3f, 30.2f, 0f);
        yield return new WaitForSeconds(.1f);
        
        
       
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NxtScene : MonoBehaviour {

    [SerializeField]
    private float delay = 10f;
    [SerializeField]
    private string sceneNameToLoad;
    private float timeElapsed;

	// Update is called once per frame
	void Update () {
        timeElapsed += Time.deltaTime;

        if(timeElapsed > delay)
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
	}
}

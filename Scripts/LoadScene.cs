using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour {

    public void SceneLoader(int sceneindex)
    {
        SceneManager.LoadScene(sceneindex);
    }
		
}

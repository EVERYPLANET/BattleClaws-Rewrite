using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string destinationSceneName;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            loadChosenScene();
        }
    }

    public void loadChosenScene() 
    {
        SceneManager.LoadScene(destinationSceneName);
    }
}

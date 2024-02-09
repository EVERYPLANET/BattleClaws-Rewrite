using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string destinationSceneName;
    private string currentSceneName;
    private bool isSplash = false;

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName == "Splash")
        {
            isSplash = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isSplash && Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(loadChosenSceneWithDelay(destinationSceneName));
        }
    }

    public IEnumerator loadChosenSceneWithDelay(string SceneName) 
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(destinationSceneName);
    }

   
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneManager : MonoBehaviour
{
    
  
    public void OnClickStartButton(string SceneName)
    {
       
        SceneManager.LoadScene("NewScene");
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

    }
    public void OnClickEndButton(string SceneName)
    {

        Application.Quit();

    }




}

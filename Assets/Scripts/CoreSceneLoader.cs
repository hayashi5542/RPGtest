using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class CoreSceneLoader : MonoBehaviour
{
    
    private void Awake()
    {
        SceneManager.LoadScene("Core", LoadSceneMode.Additive);
    }
}

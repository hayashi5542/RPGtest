using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class MoveScene : MonoBehaviour
{
    public string MoveSceneName;
    public string PortalName = "Default";

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetString("PortalName", PortalName);
            PlayerPrefs.Save();
            SceneManager.LoadScene(MoveSceneName);
        }
    }
}

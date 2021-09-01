using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public string portalName = "Default";
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetString("PortalName", "Default") == portalName)
        {
            System.Type type = typeof(UnitController);
            UnitController target = (GameObject.FindObjectOfType(type) as UnitController);
            target.transform.position = transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

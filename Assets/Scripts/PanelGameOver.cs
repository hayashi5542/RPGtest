using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class PanelGameOver : MonoBehaviour
{
    private UnitController unitController;

    private void OnEnable()
    {
        System.Type type = typeof(UnitController);
        unitController = (GameObject.FindObjectOfType(type) as UnitController);
    }

    public void OnButtonRestart()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParamGauge : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    public void Init(int current, int max)
    {
        slider.maxValue = max;
        Set(current);
    }

    public void Set(int current)
    {
        slider.value = current;
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

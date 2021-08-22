using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;
using TMPro;
using UnityEngine.UI;

public class PanelStatus : MonoBehaviour
{
    public int keepPointNum;
    public int pointATKNum;
    public int pointVITNum;
    public int pointAGINum;
    public int pointLUKNum;
    public DataUnitParam unitParam;
    public Button getPointATK;
    public Button getPointVIT;
    public Button getPointAGI;
    public Button getPointLUK;
    public TextMeshProUGUI keepPoint;
    public TextMeshProUGUI pointATK;
    public TextMeshProUGUI pointVIT;
    public TextMeshProUGUI pointAGI;
    public TextMeshProUGUI pointLUK;


    private void OnEnable()
    {
        keepPointNum = DataManeger.Instance.unitParam.status;
        pointATKNum = DataManeger.Instance.unitParam.status_ATK;
        pointVITNum = DataManeger.Instance.unitParam.status_VIT;
        pointAGINum = DataManeger.Instance.unitParam.status_AGI;
        pointLUKNum = DataManeger.Instance.unitParam.status_LUK;
        keepPoint.text = keepPointNum.ToString();
        pointATK.text = pointATKNum.ToString();
        pointVIT.text = pointVITNum.ToString();
        pointAGI.text = pointAGINum.ToString();
        pointLUK.text = pointLUKNum.ToString();
    }
    public void OnButtonATKPoint()
    {
        if (keepPointNum > 0)
        {
            pointATKNum += 1;
            keepPointNum -= 1;
        }
        PointSet();
    }

    public void OnButtonVITPoint()
    {
        if (keepPointNum > 0)
        {
            pointVITNum += 1;
            keepPointNum -= 1;
        }
        PointSet();
    }
    public void OnButtonAGIPoint()
    {
        if (keepPointNum > 0)
        {
            pointAGINum += 1;
            keepPointNum -= 1;
        }
        PointSet();
    }

    public void OnButtonLUKPoint()
    {
        if (keepPointNum > 0)
        {
            pointLUKNum += 1;
            keepPointNum -= 1;
        }
        PointSet();
    }

    public void PointSet()
    {
        keepPoint.text = keepPointNum.ToString();
        pointATK.text = pointATKNum.ToString();
        pointVIT.text = pointVITNum.ToString();
        pointAGI.text = pointAGINum.ToString();
        pointLUK.text = pointLUKNum.ToString();
        keepPoint.text = keepPointNum.ToString();
    }

    public void PointDecision()
    {

    }
}


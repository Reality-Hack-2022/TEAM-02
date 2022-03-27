using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandTrackingManager : MonoBehaviour
{

    public List<GameObject> AreaList;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlipHideAreas()
    {
        foreach (GameObject g in AreaList)
        {
            g.transform.DOScale(0.1f, 0.5f);
        }
    }

    public void FlipShowAreas()
    {
        foreach (GameObject g in AreaList)
        {
            g.transform.DOScale(1f, 0.5f);
        }
    }
}

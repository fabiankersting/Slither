using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVSript : MonoBehaviour
{
    private GameObject staticImage;
    private void Start()
    {
        staticImage = transform.GetChild(0).gameObject;
    }

    //switches between a static noise image and turned off
    public void ChangeTVState()
    {
        staticImage.gameObject.SetActive(!staticImage.gameObject.activeSelf); 
        
    }
}

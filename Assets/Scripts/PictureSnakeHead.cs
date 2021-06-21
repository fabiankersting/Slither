using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureSnakeHead : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject picture;
    [SerializeField] private GameObject end;
    [SerializeField] private GameObject start;
    bool isInTrigger = false;

    private float range;

    private void Start()
    {
        range = Vector3.Magnitude (end.transform.position - start.transform.position);

    }
    private void Update()
    {

        Vector3 playerPos = player.transform.position - start.transform.position;
        float playerDistance = Vector3.Magnitude(playerPos);
        float playerDistancePercentage = playerDistance / (range / 100f);
        float rotateY = playerDistancePercentage * 0.7f +150 ;
        rotateY =  rotateY > 220 ?  220 : rotateY;
        rotateY = rotateY < 150 ? 150 : rotateY;
        this.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotateY, transform.localEulerAngles.z);
        
    }
}

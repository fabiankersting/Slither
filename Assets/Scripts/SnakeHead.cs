using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{

    public Transform Player;
    public float headMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        headMoveSpeed = 6;
    }

    // Update is called once per frame
    void Update()
    {
        var lookPos = Player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
    
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*headMoveSpeed);
    }
}

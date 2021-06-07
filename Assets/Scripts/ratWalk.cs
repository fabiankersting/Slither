using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratWalk : MonoBehaviour
{
    [SerializeField] private List<GameObject> checkPoints;
    private GameObject checkPointParent;
    private int currentCheckPoint = 0;
    private void Start()
    {
        checkPointParent = GameObject.FindGameObjectWithTag("CheckPoint");
        for(int i=0; i < checkPointParent.transform.childCount; i++)
        {
            checkPoints.Add(checkPointParent.transform.GetChild(i).gameObject);
        }
    }


    private void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position,checkPoints[currentCheckPoint].transform.position,0.3f *Time.deltaTime);

        Vector3 dirVec = this.transform.position - checkPoints[currentCheckPoint].transform.position;
        Vector3 newDirection = Vector3.RotateTowards(this.transform.forward, dirVec, 1f * Time.deltaTime, 1f * Time.deltaTime);
        this.transform.rotation = Quaternion.LookRotation(newDirection);
        if (this.transform.position.Equals(checkPoints[currentCheckPoint].transform.position))
        {
            currentCheckPoint++;
        }
        if(currentCheckPoint >= checkPointParent.transform.childCount)
        {
            currentCheckPoint = 0;
        }
    }
}

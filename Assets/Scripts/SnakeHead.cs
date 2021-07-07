using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    private GameObject player = null;
    private bool followPlayer = true;

    [SerializeField] private float headMoveSpeed = 6;
    [SerializeField] private GameObject rat = null;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (followPlayer)
        {
            var lookPos = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*headMoveSpeed);

            // lock rotation if player walks behind the snake
            if (this.transform.localEulerAngles.y < 30)
            {
                this.transform.localEulerAngles = new Vector3(this.transform.rotation.x,30,this.transform.rotation.z);
            }
        }
        else
        {
            var lookPos = rat.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * headMoveSpeed);
        }
    }

    public void ChangeSnakeFollowPlayer(bool state)
    {
        followPlayer = state;
    }
}

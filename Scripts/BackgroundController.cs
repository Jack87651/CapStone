using UnityEngine;

public class BackgroundController : MonoBehaviour
{

    public Transform player;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = player.transform.position + new Vector3(0, 0, 5);
    }
}
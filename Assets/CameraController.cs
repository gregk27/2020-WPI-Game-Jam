using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int minHeight = 0;
    public int maxHeight = 8;

    public GameObject player;

    public float cameraSpeed;
    private Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        targetPos.z = transform.position.z;
    }

    // Update is called once per frame
    void Update() {

        //update vertical pos when player is grounded
        if (player.GetComponent<PlayerController>().isGrounded) {
            if (minHeight <= player.transform.position.y) {
                if (player.transform.position.y <= maxHeight) {
                    targetPos.y = player.transform.position.y;
                } else {
                    targetPos.y = maxHeight;
                }
            } else {
                targetPos.y = minHeight;
            }

        }

        targetPos.x = player.transform.position.x;

        //Vector3 difference = 
        transform.position += (targetPos - transform.position) / cameraSpeed;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    Transform player;
    [SerializeField]
    float z_offset = -10f;
    [SerializeField]
    float y_offset = 3f;
    [SerializeField]
    float y_deadzone = -1f;

    [SerializeField]
    float smooth = 0.3f;

    float y_target;
    float x_target;
    float x_init;
    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        y_target = player.position.y;
        x_init = player.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if(player.position.x < -6)
            {
                x_target = x_init;
            }
            else
            {
                x_target = player.position.x;
            }
            if (player.position.y > y_deadzone || player.position.y < (transform.position.y -y_offset))
            {
                y_target = player.position.y + y_offset;
            }
            else
            {
                y_target = transform.position.y;
            }
            Vector3 targetPosition = new Vector3(x_target, y_target, player.position.z + z_offset);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smooth);
        }
    }
}

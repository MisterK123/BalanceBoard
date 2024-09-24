using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundMOvement : MonoBehaviour
{
    GameObject player;
    [SerializeField] float baseSpeed;
    private movement movement_script;

    void Start()
    {
        player = GameObject.Find("Player");
        movement_script = player.GetComponent<movement>();
    }

    void Update()
    {
        if(movement_script.running == true) {
            transform.position += new Vector3(0.5f * (baseSpeed + movement_script.velY), 0.088164f * (baseSpeed + movement_script.velY), 0);
            if (transform.position.x > 184.5f)
            {
                transform.position = new Vector3(-470f, -58.7f, -1.5658f);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    GameObject player;
    [SerializeField] float baseSpeed;
    private movement movement_script;
    float deathTimer;
    void Start()
    {
        player = GameObject.Find("Player");
        movement_script = player.GetComponent<movement>();
    }

    void Update()
    {
        deathTimer += Time.deltaTime;
        transform.position += new Vector3(0.5f*(baseSpeed+movement_script.velY), 0.088164f* (baseSpeed + movement_script.velY), 0);
        if(deathTimer > 11)
        {
            Destroy(gameObject);
        }
    }
}

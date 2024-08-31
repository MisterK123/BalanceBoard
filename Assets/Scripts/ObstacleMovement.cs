using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] float speed;
    float deathTimer;
    void Start()
    {
        
    }

    void Update()
    {
        deathTimer += Time.deltaTime;
        transform.position += new Vector3(0.5f*speed, 0.088164f*speed, 0);
        if(deathTimer > 4)
        {
            Destroy(gameObject);
        }
    }
}

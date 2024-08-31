using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject obstaclePrefab;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.5)
        {
            Instantiate(obstaclePrefab, new Vector3(-12.3f, 23.7f, Random.Range(-41f, 16f)), Quaternion.identity);
            timer = 0;
            Debug.Log("spawned");
        }
        
    }


}

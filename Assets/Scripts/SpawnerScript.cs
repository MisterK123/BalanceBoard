using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject obstaclePrefab;
    float timer;
    [SerializeField] float lowerRange;
    [SerializeField] float upperRange;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > speed)
        {
            Instantiate(obstaclePrefab, new Vector3(-139.85f, 1.76f, Random.Range(lowerRange, upperRange)), Quaternion.identity);
            timer = 0;
            Debug.Log("spawned");
        }
        
    }


}

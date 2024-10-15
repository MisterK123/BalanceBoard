using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject obstaclePrefab;
    float timer;
    [SerializeField] bool isMiddle = false;
    GameObject player;
    private movement movement_script;
    [SerializeField] float lowerRange;
    [SerializeField] float upperRange;
    [SerializeField] float speed;
    [SerializeField] bool active;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        movement_script = player.GetComponent<movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            timer += Time.deltaTime;

            if (isMiddle) {
                if (timer > (movement_script.spawnerSpeed)/2)
                {
                    Instantiate(obstaclePrefab, new Vector3(-139.85f, -1.76f, Random.Range(lowerRange, upperRange)), Quaternion.identity);
                    timer = 0;
                    Debug.Log("spawned");
                }
            }
            
            else if (timer > movement_script.spawnerSpeed)
            {
                Instantiate(obstaclePrefab, new Vector3(-139.85f, -1.76f, Random.Range(lowerRange, upperRange)), Quaternion.identity);
                timer = 0;
                Debug.Log("spawned");
            }
        }
    }


}

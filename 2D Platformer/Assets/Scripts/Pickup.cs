using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isGem;
    private bool isCollected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !isCollected)
        {
            if(isGem)
            {
                LevelManager.instance.gemsCollected++;

                isCollected = true;

                Destroy(gameObject);
            }
        }
    }
}

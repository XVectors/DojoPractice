using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{


    // 1
    void OnTriggerEnter(Collider other) 
    {
        //2
        if(other.name == "Player") 
        {
            Debug.Log("Player detected ­ attack!"); 
        }
    }
    // 3
    void OnTriggerExit(Collider other) 
    {
        // 4
        if(other.name == "Player") 
        {
            Debug.Log("Player out of range, resume patrol"); 
        }
    }
/*     // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    } */
}

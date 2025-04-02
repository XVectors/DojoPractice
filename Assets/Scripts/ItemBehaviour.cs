using UnityEditor.Build.Content;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public GameBehaviour gameManager; // Ссылка на GameBehaviour

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehaviour>();
    }

    void OnCollisionEnter(Collision collision) 
    {
       
        if(collision.gameObject.name == "Player") 
        { 
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Item collected!");

            gameManager.Items += 1;
        }
    }
}
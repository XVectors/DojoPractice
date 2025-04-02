using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    public float onScreenDelay = 3f; // Время, в течение которого пуля будет видна на экране

    void Start()
    {
        Destroy(this.gameObject, onScreenDelay); // Уничтожаем пулю через заданное время
    }

    void Update()
    {
        
    }
}

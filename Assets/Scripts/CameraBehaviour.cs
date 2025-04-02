using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Vector3 camOffset = new Vector3(0f, 1.2f, -2.6f); // Смещение камеры (X, Y, Z)
    private Transform target; // Цель (игрок)

    void Start()
    {
        target = GameObject.Find("Player").transform; // Находим игрока по имени
    }

    void LateUpdate() // Выполняется после движения игрока
    {
        transform.position = target.TransformPoint(camOffset); // Позиция камеры
        transform.LookAt(target); // Камера смотрит на игрока
    }
}
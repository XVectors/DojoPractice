using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Для навигации

public class EnemyBehaviour : MonoBehaviour
{
    public Transform player; 
    public Transform patrolRoute; // Патрульный маршрут
    public List<Transform> locations; // Патрульные точки
   

    private int locationIndex = 0; // Индекс текущей точки
    private NavMeshAgent agent; // Агент навигации
    private int health = 3; // Количество жизней врага

    public int enemyHealth
    {
        get { return health; } // Возвращаем количество жизней врага
        
        private set
        { 
        
            health = value; // Устанавливаем количество жизней врага
        
            if( health <= 0)
            {
                Destroy(this.gameObject); // Уничтожаем врага
                Debug.Log("Enemy destroyed!"); // Выводим сообщение о победе
            }
        }
      
    }


    void Start()
    {
       agent = GetComponent<NavMeshAgent>(); // Получаем компонент NavMeshAgent

        player = GameObject.Find("Player").transform; // Находим игрока по тегу

       InitializePatrolRoute(); // Инициализация патрульного маршрута

       MoveToNextPatrolPoint(); // Перемещаемся к следующей точке
    }

    void Update()
    {
        if(agent.remainingDistance < 0.2f && !agent.pathPending) // Проверяем, достигли ли мы точки
        {
            MoveToNextPatrolPoint(); // Перемещаемся к следующей точке
        }
    }



    void InitializePatrolRoute()
    {
        // Получаем все дочерние объекты патрульного маршрута
        foreach (Transform child in patrolRoute) 
        {
            locations.Add(child); // Добавляем каждую точку в список
        }
    }

    void MoveToNextPatrolPoint()
    {
        if(locations.Count == 0) return; // Если нет точек, выходим

        agent.destination = locations[locationIndex].position; // Устанавливаем цель для агента

        locationIndex = (locationIndex + 1) % locations.Count; // Переходим к следующей точке
        // Если достигли последней точки, начинаем с первой
    }
   
    void OnTriggerEnter(Collider other) 
    {
        
        if(other.name == "Player") 
        {
            agent.destination = player.position; // Устанавливаем цель для агента на игрока
            Debug.Log("Enemy detected, chasing"); 
        }

         if(other.gameObject.tag == "Bullet") 
        {
            enemyHealth -= 1; // Уменьшаем здоровье врага
            Debug.Log("Enemy hit! Health: " + enemyHealth); // Выводим сообщение о попадании
        }
    }
    
    void OnTriggerExit(Collider other) 
    {
        
        if(other.name == "Player") 
        {
            Debug.Log("Player out of range, resume patrol"); 
        }
    }

    void OnCollisionEnter(Collision collision) 
    {
       
    }
}

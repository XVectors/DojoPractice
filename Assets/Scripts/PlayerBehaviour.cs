using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Для работы с UI

public class PlayerBehaviour : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f; // Скорость прыжка
    public float distanceToGround = 0.1f; // Расстояние до земли
    public LayerMask groundLayer; // Слой земли
    public GameObject bullet;
    public float bulletSpeed = 100f;
    public AudioClip damageSound;
    public Image damageOverlay; // Ссылка на UI-элемент для эффекта
    public float fadeDuration = 1f; // Длительность эффекта

   

    private float vInput;
    private float hInput;
    private Rigidbody rb;
    private CapsuleCollider col;
    private GameBehaviour gameManager;
    private AudioSource audioSource;
    private Color overlayColor; // Текущий цвет наложения
    private bool isFading = false; // Флаг для контроля эффекта

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Получаем компонент Rigidbody
        col = GetComponent<CapsuleCollider>(); // Получаем компонент CapsuleCollider

        gameManager = GameObject.Find("GameManager").GetComponent<GameBehaviour>(); // Получаем ссылку на GameManager
        audioSource = GetComponent<AudioSource>(); // Получаем компонент AudioSource
        
        if (damageOverlay != null)
        {
            overlayColor = damageOverlay.color;
            overlayColor.a = 0; // Начальная прозрачность
            damageOverlay.color = overlayColor;
        }
    }

   
    void Update()
    {       
       // Debug.Log($"IsGrounded: {IsGrounded()} | Jump Key: {Input.GetKeyDown(KeyCode.Space)}");

        vInput = Input.GetAxis("Vertical") * moveSpeed; // Ввод: вперед/назад
        hInput = Input.GetAxis("Horizontal") * rotateSpeed; // Ввод: вращение

       

      /*   vInput = Input.GetAxis("Vertical") * moveSpeed;

        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime); */

    // Прыжок
        if(IsGrounded() &&  Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse); // Применяем силу вверх
        }
    }

    void FixedUpdate() // Для физики
    {
        //движение вперед/назад
        Vector3 movement = transform.forward * vInput * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        //вращение
        Quaternion rotation = Quaternion.Euler(0, hInput * Time.fixedDeltaTime, 0);
        rb.MoveRotation(rb.rotation * rotation);

        if (Input.GetMouseButtonDown(0)) 
         {
            // 3
            Vector3 spawnPosition = this.transform.position + this.transform.forward*0.5f;
            GameObject newBullet = Instantiate(bullet, spawnPosition, this.transform.rotation) as GameObject; // Создаем пулю в позиции игрока
            //this.transform.position + new Vector3 (1,0,0), this.transform.rotation) as GameObject;
            // 4
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody> ();
            // 5
            bulletRB.linearVelocity = this.transform.forward * bulletSpeed; //linearVelocity
         }
    }

    private bool IsGrounded()
    {
       Vector3 capsuleBottom = new Vector3 (col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);
       bool grounded = Physics.CheckCapsule(col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore); // Проверяем, касается ли капсула земли
        
        return grounded; // Возвращаем результат проверки
    }

   
    void OnDrawGizmos()  // Добавил отрисовку Gizmos для капсулы для наглядности ее работы
    {
        if (col == null) return; // Если коллайдер не найден - пропускаем

        // 1. Рассчитываем нижнюю точку капсулы (как в IsGrounded)
        Vector3 capsuleBottom = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);

        // 2. Рисуем Gizmos (только в редакторе Unity)
        Gizmos.color = Color.green; // Цвет зоны проверки

        // а) Два полушария (верх и низ капсулы)
        Gizmos.DrawWireSphere(col.bounds.center, distanceToGround);
        Gizmos.DrawWireSphere(capsuleBottom, distanceToGround);

        // б) Линии, соединяющие края полушарий (для наглядности)
        Gizmos.DrawLine(
            col.bounds.center + Vector3.right * distanceToGround,
            capsuleBottom + Vector3.right * distanceToGround
        );
        Gizmos.DrawLine(
            col.bounds.center - Vector3.right * distanceToGround,
            capsuleBottom - Vector3.right * distanceToGround
        );
    }


     void OnCollisionEnter( Collision collision) 
    {
                                        //Debug.Log($"Столкновение с: {collision.gameObject.name}, тег: {collision.gameObject.tag}");

        if(collision.gameObject.CompareTag("Enemy")) 
        {
            gameManager.HP -= 1; // Уменьшаем здоровье игрока
                                        //Debug.Log("Hit by enemy!");

            if (damageSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(damageSound); // Воспроизводим звук получения урона
            }

             if (damageOverlay != null && !isFading)
            {
                StartCoroutine(ShowDamageEffect()); // Запускаем красный экран
            }
            
        }
    }


    private IEnumerator ShowDamageEffect()
    {
        isFading = true;

        // Устанавливаем начальную непрозрачность
        overlayColor.a = 1f;
        damageOverlay.color = overlayColor;

        // Постепенно уменьшаем прозрачность
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            overlayColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            damageOverlay.color = overlayColor;
            yield return null;
        }

        // Убедимся, что прозрачность полностью исчезла
        overlayColor.a = 0f;
        damageOverlay.color = overlayColor;

        isFading = false;
    }

}




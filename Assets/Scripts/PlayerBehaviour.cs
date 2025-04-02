using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f; // Скорость прыжка
    public float distanceToGround = 0.1f; // Расстояние до земли
    public LayerMask groundLayer; // Слой земли
    public GameObject bullet;
    public float bulletSpeed = 100f;

    private float vInput;
    private float hInput;
    private Rigidbody rb;
    private CapsuleCollider col;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Получаем компонент Rigidbody
        col = GetComponent<CapsuleCollider>(); // Получаем компонент CapsuleCollider
    }

   
    void Update()
    {       
        Debug.Log($"IsGrounded: {IsGrounded()} | Jump Key: {Input.GetKeyDown(KeyCode.Space)}");

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


}

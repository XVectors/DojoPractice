using UnityEngine;
[RequireComponent(typeof(LineRenderer))]

public class ForwardVectorVisualizer : MonoBehaviour

   /* { public float arrowLength = 3f; // Длина стрелки

    private void OnDrawGizmos()
    {
        // Устанавливаем цвет стрелки
        Gizmos.color = Color.blue;

        // Начальная точка — позиция объекта
        Vector3 startPosition = transform.position;

        // Конечная точка — позиция + вектор forward умноженный на длину стрелки
        Vector3 endPosition = startPosition + transform.forward * arrowLength;

        // Рисуем линию
        Gizmos.DrawLine(startPosition, endPosition);

        // Рисуем "головку" стрелки
        Gizmos.DrawSphere(endPosition, 0.1f); 
    }
    */



{
    public float arrowLength = 3f; // Длина стрелки
    public float arrowHeadSize = 0.5f;

    private LineRenderer lineRenderer;

    private void Update()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 3; // Линия + "головка" стрелки
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        DrawArrow();
    }

    private void DrawArrow()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + transform.forward * arrowLength;

        // Основная линия
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);

        // "Головка" стрелки — небольшой отрезок под углом
        Vector3 arrowHeadDirection = Quaternion.Euler(0, 45, 0) * -transform.forward;
        Vector3 arrowHeadEnd = endPosition + arrowHeadDirection * arrowHeadSize;

        lineRenderer.SetPosition(2, arrowHeadEnd);
    }
}


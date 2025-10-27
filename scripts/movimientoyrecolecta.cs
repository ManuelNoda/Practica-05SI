using UnityEngine;

public class GazeTeleportToPoint : MonoBehaviour
{
    public float gazeTime = 2f;
    public float rayDistance = 30f;
    public LayerMask interactiveLayer; // Capa "Interactive"

    private GameObject currentGazedObject;
    private float timer = 0f;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Solo detecta objetos en la capa Interactive
        if (Physics.Raycast(ray, out hit, rayDistance, interactiveLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("point")) // Ahora buscamos puntos, no espada
            {
                if (hitObject != currentGazedObject)
                {
                    currentGazedObject = hitObject;
                    timer = 0f; // Reinicia el temporizador si cambia de objeto
                }

                timer += Time.deltaTime;

                if (timer >= gazeTime)
                {
                    TeleportToPoint(currentGazedObject);
                    currentGazedObject = null;
                    timer = 0f;
                }
            }
            else
            {
                ResetGaze();
            }
        }
        else
        {
            ResetGaze();
        }
    }

    void TeleportToPoint(GameObject point)
    {
        Transform player = transform.parent; // La cámara está dentro del jugador
        Vector3 targetPosition = point.transform.position;

        // Opcional: ajusta la altura del jugador (por si el punto está al ras del suelo)
        targetPosition.y = player.position.y;

        player.position = targetPosition;
        Debug.Log($"Teletransportado a: {point.name}");
    }

    void ResetGaze()
    {
        currentGazedObject = null;
        timer = 0f;
    }
}

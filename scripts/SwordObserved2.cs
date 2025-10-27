using UnityEngine;

public class SwordObserver2 : MonoBehaviour
{
    public GazeCollectSword gazeCollect;  // Referencia a la cámara (con el evento)
    public float moveSpeed = 5f;

    private bool isMoving = false;
    private Transform target;

    void Start()
    {
        gazeCollect.OnRecolectarEspada += OnSwordCollected;
    }

    void OnDisable()
    {
        gazeCollect.OnRecolectarEspada -= OnSwordCollected;
    }

    void OnSwordCollected(GameObject sword, Transform player)
    {
        // Solo reaccionamos si esta espada es la que fue mirada
        if (sword == gameObject)
        {
            target = player;
            isMoving = true;

            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true; // Desactivar física
        }
    }

    void Update()
    {
        if (!isMoving || target == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < 0.5f)
        {
            isMoving = false;
            gameObject.SetActive(false); // Se desactiva al llegar
        }
    }
}

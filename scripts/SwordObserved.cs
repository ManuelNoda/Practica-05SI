using UnityEngine;

public class SwordObserver : MonoBehaviour
{
    public GazeRecolectar gazeRecolectar;
    public float moveSpeed = 3f;

    private bool isMoving = false;
    private Transform target;

    void Start()
    {
        // Suscribimos la espada al evento del jugador
        gazeRecolectar.OnRecolectarEspadas += MoveToPlayer;
    }

    void OnDisable()
    {
        
        gazeRecolectar.OnRecolectarEspadas -= MoveToPlayer;
    }

    void MoveToPlayer(Transform player)
    {
        target = player;
        isMoving = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true; // Desactiva la física
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

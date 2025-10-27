using UnityEngine;

public class GazeRecolectar : MonoBehaviour
{
    public delegate void RecolectarEvent(Transform player);
    public event RecolectarEvent OnRecolectarEspadas;

    public float gazeTime = 2f;
    public float rayDistance = 10f;
    public LayerMask interactiveLayer;

    private GameObject currentGazedObject;
    private float timer = 0f;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, interactiveLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("recolectar"))
            {
                if (hitObject != currentGazedObject)
                {
                    currentGazedObject = hitObject;
                    timer = 0f;
                }

                timer += Time.deltaTime;

                if (timer >= gazeTime)
                {
                    // Lanza el evento con el transform del jugador
                    OnRecolectarEspadas?.Invoke(transform.parent);
                    timer = 0f;
                }
            }
            else ResetGaze();
        }
        else ResetGaze();
    }

    void ResetGaze()
    {
        currentGazedObject = null;
        timer = 0f;
    }
}

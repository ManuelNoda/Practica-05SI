using UnityEngine;

public class GazeCollectSword : MonoBehaviour
{
    public delegate void RecolectarEvent(GameObject sword, Transform player);
    public event RecolectarEvent OnRecolectarEspada;

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

            if (hitObject.CompareTag("Espada"))
            {
                if (hitObject != currentGazedObject)
                {
                    currentGazedObject = hitObject;
                    timer = 0f;
                }

                timer += Time.deltaTime;

                if (timer >= gazeTime)
                {
                    OnRecolectarEspada?.Invoke(hitObject, transform.parent);
                    timer = 0f;
                    currentGazedObject = null;
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

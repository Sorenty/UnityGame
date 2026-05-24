using UnityEngine;

public class BasketBallReset : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody rb;

    [Header("Optional")]
    public Transform respawnPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (respawnPoint != null)
            startPosition = respawnPoint.position;
        else
            startPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ResetBall();
        }

        if (transform.position.y < -5f)
        {
            ResetBall();
        }
    }

    public void ResetBall()
    {
        transform.position = startPosition;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
using UnityEngine;
using UnityEngine.AI;

public class NPCTestMove : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.SetDestination(target.position);
    }

    void Update()
    {
        // фиксируем Z чтобы NPC не улетал в глубину
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
using UnityEngine;
using UnityEngine.AI;

public class MonsterFollow : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField]
    Transform targetTransform;

    void Start()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (targetTransform != null && !!agent)
        {
            agent.SetDestination(targetTransform.position);
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class MonsterBehaviour : MonoBehaviour
{

    [Header("Monster Stalking Behaviour")]
    [Tooltip("The time it takes for the monster to teleport if it cannot find the player")]
    public float timeBeforeTeleport = 20f;

    [Space(10)]
    [Header("Monster Listening Values")]
    [Tooltip("The distance the monster can hear walking footsteps")]
    public float walkingDetectionListeningRadius = 10f;
    [Tooltip("The distance the monster can hear running footsteps")]
    public float runningDetectionListeningRadius = 20f;
    [Tooltip("The distance the monster can hear sprinting footsteps")]
    public float sprintingDetectionListeningRadius = 30f;


    [Space(10)]
    [Header("Object References")]
    public GameObject PlayerReference;

    NavMeshAgent agent;
    GameController gameController;

    void Start()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!gameController) gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        ListenToPlayerMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameController.LoseGame();
        }
    }

    private void GetRandomRoamingTarget()
    {
        // If the monster cannot find the player, it will try to roam around for a while until the teleport timer happens, then it will teleport and roam around again.
    }

    private void ListenToPlayerMovement()
    {
        // TODO: IMPLEMENT SOMETHING FOR LISTENING THROUGH WALLS
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerReference.transform.position);

        PlayerMovementState playerMovementState = PlayerReference.GetComponent<FirstPersonController>().GetCurrentMovementState;

        if (distanceToPlayer <= sprintingDetectionListeningRadius && playerMovementState == PlayerMovementState.SPRINT)
        {
            agent.SetDestination(PlayerReference.transform.position);
        }
        else if (distanceToPlayer <= runningDetectionListeningRadius && playerMovementState == PlayerMovementState.RUN)
        {
            agent.SetDestination(PlayerReference.transform.position);
        }
        else if (distanceToPlayer <= walkingDetectionListeningRadius && playerMovementState == PlayerMovementState.WALK)
        {
            agent.SetDestination(PlayerReference.transform.position);
        }
        else
        {
            GetRandomRoamingTarget();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, walkingDetectionListeningRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, runningDetectionListeningRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sprintingDetectionListeningRadius);
    }
}

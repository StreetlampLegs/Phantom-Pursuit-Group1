using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using StarterAssets;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using JetBrains.Annotations;
using System.Collections;

public class MonsterBehaviour : MonoBehaviour
{

    [Header("Monster Stalking Behaviour")]
    [Tooltip("The time it takes for the monster to teleport if it cannot find the player")]
    public float timeBeforeTeleport = 20f;
    [Space(10)]
    [Tooltip("The time it takes for the monster to increase in speed")]
    public float AggroSpeedIncreaseTime = 20f;
    [Tooltip("The increase in base speed which the monster increases by in m/s")]
    public float AggroSpeedIncreaseIncrement = 0.2f;

    [Space(10)]
    [Header("Monster Listening Values")]
    [Tooltip("The distance the monster can hear walking footsteps")]
    public float walkingDetectionListeningRadius = 10f;
    [Tooltip("The distance the monster can hear running footsteps")]
    public float runningDetectionListeningRadius = 20f;
    [Tooltip("The distance the monster can hear sprinting footsteps")]
    public float sprintingDetectionListeningRadius = 30f;


    [Space(10)]
    [Header("Monster Other Behaviour")]
    [Tooltip("Time it takes for the monster to wake up after flicking the first switch")]
    public float wakeupTime = 5f;

    [Space(10)]
    [Header("Monster Audio")]
    public AudioClip MonsterAwakeSound;
    public AudioClip MonsterAggroScreamSound;
    public List<AudioClip> MonsterFootstepSounds;

    [Space(10)]
    [Header("Object References")]
    public GameObject PlayerReference;

    NavMeshAgent agent;
    GameController gameController;
    AudioSource audioPlayer;

    private Vector3 roamTargetPosition = Vector3.zero;

    private float _aggroTimer;

    [Space(10), Header("Debug")]
    [SerializeField]
    bool isActive = false;

    void Start()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!PlayerReference) PlayerReference = GameObject.FindWithTag("Player");
        if (!gameController) gameController = GameObject.Find("GameController").GetComponent<GameController>();
        if (!audioPlayer) audioPlayer = GetComponent<AudioSource>();

        _aggroTimer = AggroSpeedIncreaseTime;
    }

    public void ActivateAI()
    {
        if (isActive) return;
        StartCoroutine(DoWakeUpSequence());
    }

    IEnumerator DoWakeUpSequence()
    {
        if (MonsterAwakeSound) audioPlayer.PlayOneShot(MonsterAwakeSound);
        yield return new WaitForSeconds(wakeupTime);
        isActive = true;
    }

    void Update()
    {
        if (isActive)
        {
            DoAggro();
            ListenToPlayerMovement();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(4);
            Debug.Log("game lost");
        }
    }

    private void GetRandomRoamingTarget()
    {
        // If the monster cannot find the player, it will try to roam around for a while until the teleport timer happens, then it will teleport and roam around again.
        if (agent.hasPath && agent.destination != null && agent.remainingDistance > 5f)
        {
            return;
        }

        roamTargetPosition = transform.position + Random.insideUnitSphere * 20f;
        agent.SetDestination(roamTargetPosition);
    }

    private void DoAggro()
    {
        if (_aggroTimer <= 0)
        {
            Debug.Log("Aggroed");
            if (MonsterAggroScreamSound)
            {
                audioPlayer.PlayOneShot(MonsterAggroScreamSound);
            }
            agent.speed += AggroSpeedIncreaseIncrement;
            _aggroTimer = AggroSpeedIncreaseTime;
        }
        else
        {
            _aggroTimer -= Time.deltaTime;
        }
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

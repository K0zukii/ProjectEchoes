using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyAnimation : MonoBehaviour
{
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int ScreamHash = Animator.StringToHash("Scream");
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;

    void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (agent == null) agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        animator.SetFloat(SpeedHash, agent.velocity.magnitude);
    }

    public void TriggerRoar()
    {
        animator.SetTrigger(ScreamHash);
    }

    public void TriggerAttack()
    {
        animator.SetTrigger(AttackHash);
    }
}

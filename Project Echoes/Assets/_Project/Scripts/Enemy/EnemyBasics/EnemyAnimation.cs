using UnityEngine;
[RequireComponent(typeof(Animator))]

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyAnimation : MonoBehaviour
{
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    [SerializeField] private Animator animator;
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;

    void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (agent == null) agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (animator != null && agent != null)
        {
            animator.SetFloat(SpeedHash, agent.velocity.magnitude);
        }
    }

    public void TriggerRoar()
    {
        animator.SetTrigger("Scream");
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("Attack");
    }
}

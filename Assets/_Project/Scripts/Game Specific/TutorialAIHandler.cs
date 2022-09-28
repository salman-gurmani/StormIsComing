using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialAIHandler : MonoBehaviour
{
    public static TutorialAIHandler Instance;
    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Animator animator;

    private TutorialTarget currentTarget;
    private bool canCutWood = true;


    private void Start()
    {
        Instance = this;
        animator.fireEvents = false;
    }

    public void MoveToNextTarget()
    {
        currentTarget = player.GetComponent<TutorialTargetController>().GetCurrentTarget();
        navAgent.SetDestination(currentTarget.transform.position);

    }

    private void Update()
    {
        if (navAgent.velocity.sqrMagnitude > 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Here1");
        if (other.CompareTag("WoodCuttingTarget"))
        {
            Debug.Log("Here2");
            CutWood();
        }
    }

    private void CutWood()
    {
        if (canCutWood)
            StartCoroutine(nameof(CallWoodCuttingAnimation));
    }

    private IEnumerator CallWoodCuttingAnimation()
    {
        canCutWood = false;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        canCutWood = true;
    }
}
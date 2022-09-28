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
    private bool canCollectMud = true;
    private bool canCollectIron = true;


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

        CheckDistance();
    }


    public void CheckDistance()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist > 2.5f)
        {
            navAgent.speed = 0;
        }
        else
        {
            navAgent.speed = 2f;
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
        //else if (other.CompareTag("MudCollectingTarget"))
        //{
        //    CollectMud();
        //}
        //else if (other.CompareTag("IronCollectingTarget"))
        //{
        //    CollectIron();
        //}
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

    private void CollectMud()
    {
        if (canCollectMud)
            StartCoroutine(nameof(CallMudCollectingAnimation));
    }

    private IEnumerator CallMudCollectingAnimation()
    {
        canCollectMud = false;
        animator.SetTrigger("Attack 2");
        yield return new WaitForSeconds(0.5f);
        canCollectMud = true;
    }
    private void CollectIron()
    {
        if (canCollectIron)
            StartCoroutine(nameof(CallIronCollectingAnimation));
    }

    private IEnumerator CallIronCollectingAnimation()
    {
        canCollectIron = false;
        animator.SetTrigger("Attack 3");
        yield return new WaitForSeconds(0.5f);
        canCollectIron = true;
    }
}
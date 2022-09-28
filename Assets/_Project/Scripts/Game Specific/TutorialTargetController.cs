using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTargetController : MonoBehaviour
{
    public static TutorialTargetController Instance;
    [SerializeField] GameObject tutorialArrow;

    private TutorialTarget[] targetsInThisLevel;
    private TutorialTarget currentTarget;
    public int currentTargetIndex = 0;

    private void Start()
    {
        Instance = this;
        targetsInThisLevel = FindObjectsOfType<TutorialTarget>();
        if (targetsInThisLevel.Length == 0)
            return;

        Array.Sort(targetsInThisLevel, delegate (TutorialTarget x, TutorialTarget y) { return x.orderIndex.CompareTo(y.orderIndex); });
        currentTargetIndex = 0;
        currentTarget = targetsInThisLevel[currentTargetIndex];
    }

    public TutorialTarget GetCurrentTarget()
    {
        return currentTarget;
    }

    public void SetTutorialCursor(bool _isActive)
    {
        tutorialArrow.SetActive(_isActive);
    }

    public void MoveToNextTarget()
    {
        currentTargetIndex++;
        currentTarget = targetsInThisLevel[currentTargetIndex];
    }

    private void Update()
    {
        if (!currentTarget)
            return;

        tutorialArrow.transform.LookAt(currentTarget.transform);
        if (Vector3.Distance(transform.position, currentTarget.transform.position) < 3f)
            tutorialArrow.GetComponentInChildren<MeshRenderer>().enabled = false;
        else
            tutorialArrow.GetComponentInChildren<MeshRenderer>().enabled = true;
    }
}
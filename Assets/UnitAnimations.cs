using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimations : MonoBehaviour
{
    public BattleUnit unit;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();        
        SetIdle(CellDirection.NE);
    }

    public void SetIdle(CellDirection lastDirection)
    {
        switch (lastDirection)
        {
            case CellDirection.N:
                animator.Play("Idle NE");
                break;
            case CellDirection.NE:
                animator.Play("Idle NE");
                break;
            case CellDirection.E:
                animator.Play("Idle SE");
                break;
            case CellDirection.SE:
                animator.Play("Idle SE");
                break;
            case CellDirection.S:
                animator.Play("Idle SW");
                break;
            case CellDirection.SW:
                animator.Play("Idle SW");
                break;
            case CellDirection.W:
                animator.Play("Idle NW");
                break;
            case CellDirection.NW:
                animator.Play("Idle NW");
                break;
            default:
                break;
        }
    }

    public void WalkSwitch(CellDirection direction)
    {
        switch (direction)
        {
            case CellDirection.N:
                animator.Play("Walk NE");
                break;
            case CellDirection.NE:
                animator.Play("Walk NE");
                break;
            case CellDirection.E:
                animator.Play("Walk SE");
                break;
            case CellDirection.SE:
                animator.Play("Walk SE");
                break;
            case CellDirection.S:
                animator.Play("Walk SW");
                break;
            case CellDirection.SW:
                animator.Play("Walk SW");
                break;
            case CellDirection.W:
                animator.Play("Walk NW");
                break;
            case CellDirection.NW:
                animator.Play("Walk NW");
                break;
            default:
                break;
        }
    }
}

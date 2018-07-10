using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatrolPointHolder
{
    public Vector3 position;
    public float delayTime;
}

public class AIController : MonoBehaviour
{

    //Why I'm using a Vector3: X & Y are normal Vector2, Z is wait time before moving to this point.
    public List<PatrolPointHolder> PatrolPointList;

    public float movementSpeed = 1.0f;

    //Defaulted to false, when true patroller stops patrolling until set to true.
    public bool interruptPatrol = false;

    Vector3 targetCurrent;
    Vector3 moveDirection;
    Rigidbody myRigidBody;

    int PatrolPointListIndex = 0;
    float patrolDelay = 0.0f;

    // Use this for initialization
    void Start ()
    {
        //ERROR CHECKING - Debug warning if PatrolPointList is empty
        if (PatrolPointList.Count == 0)
        {
            Debug.LogWarning(gameObject.name + " has no points assigned in PatrolPointList!");
            return;
        }

        //Initialization work
        myRigidBody = gameObject.GetComponent<Rigidbody>();
        NextTarget();
        UpdateMoveDirection();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        //ERROR CHECKING - Don't run Update() if PatrolPointList is empty.
        if (PatrolPointList.Count == 0)
        {
            return;
        }

        
        //Move patroller towards target if it isn't delayed/interrupted.
        if (!interruptPatrol && patrolDelay <= 0)
        {
            //Determine if patroller should track new target.
            if (IsCloseToTarget())
            {
                NextTarget();
            }
            //Move runner towards target.
            UpdateMoveDirection();
            myRigidBody.velocity = moveDirection.normalized * movementSpeed;
        }
        else
        {
            myRigidBody.velocity = Vector3.zero;
        }
        //Reduce patrolDelay timer
        if(patrolDelay > 0)
        {
            patrolDelay -= Time.deltaTime;
        }

    }

    public bool IsCloseToTarget()
    {
        //If patroller is closer to target than it's movement speed, return true.
        return Vector3.Distance(myRigidBody.position, targetCurrent) < 0.1;
    }

    public void NextTarget()
    {
        //Change targetCurrent position.
        Vector3 PatrolPoint = PatrolPointList[PatrolPointListIndex].position;
        targetCurrent = new Vector3(PatrolPoint.x,PatrolPoint.y, PatrolPoint.z);

        //Set the delay defined by previous target before moving to new target.
        int decrementedIndex = PatrolPointListIndex - 1;
        if (decrementedIndex < 0)
        {
            decrementedIndex = PatrolPointList.Count - 1;
        }
        patrolDelay = PatrolPointList[decrementedIndex].delayTime;

        //Set up main index counter for next time the method is called.
        PatrolPointListIndex++;
        if(PatrolPointListIndex >= PatrolPointList.Count)
        {
            PatrolPointListIndex = 0;
        }
        
    }
    public void UpdateMoveDirection()
    {
        moveDirection = targetCurrent - myRigidBody.position;
        myRigidBody.transform.forward = moveDirection;
    }
}
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


[RequireComponent(typeof(NavMeshAgent))]
public class StandardEnemy : MonoBehaviour
{
    [SerializeField] Vector2 minPosition;
    [SerializeField] Vector2 maxPosition;
    [SerializeField] float waitTime = 1;
    [SerializeField] float arrivalThreshold = 0.5f;

    private NavMeshAgent navAgent;

    private Vector3 movePosition;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(WaitBeforeMove(waitTime));
    }

    private void Update()
    {
        if (navAgent.remainingDistance <= arrivalThreshold)
        {
            StartCoroutine(WaitBeforeMove(waitTime));
        }
    }

    private void SetAgentDestination()
    {
        Vector3 lastPosition = movePosition;

        movePosition = GetRandomPosition();

        if (lastPosition.x == movePosition.x || lastPosition.z == movePosition.z)
        {
            movePosition = GetRandomPosition();
        }

        navAgent.SetDestination(movePosition);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 position = Vector3.zero;
        position.x = Random.Range(minPosition.x, maxPosition.x);
        position.y = transform.position.y;
        position.z = Random.Range(minPosition.y, maxPosition.y);

        return position;
    }

    private IEnumerator WaitBeforeMove(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        SetAgentDestination();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}

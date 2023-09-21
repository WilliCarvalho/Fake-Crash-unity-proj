using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class BossFightTrigger : MonoBehaviour
{
    public event Action OnEnterBossArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovementComponent>())
        {
            OnEnterBossArea?.Invoke();
        }
    }
}
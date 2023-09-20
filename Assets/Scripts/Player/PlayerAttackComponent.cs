using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerAttackComponent : MonoBehaviour
{
    private SphereCollider spherecollider;

    private void Awake()
    {
        spherecollider = GetComponent<SphereCollider>();
        spherecollider.enabled = false;
        PlayerManager.HandleAttackInput += AttackHandler;
    }

    private void AttackHandler(bool isAttacking)
    {
        if (!isAttacking) return;
        spherecollider.enabled = isAttacking;
        StartCoroutine(TurnAttackDetectionOff());
    }

    private IEnumerator TurnAttackDetectionOff()
    {
        yield return new WaitForSeconds(1f);
        spherecollider.enabled = false;
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Enemy")
        {
            Destroy(otherCollider.gameObject);
        }
    }
}
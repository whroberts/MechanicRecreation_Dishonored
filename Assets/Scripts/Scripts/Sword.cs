using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    CapsuleCollider _collider;

    [SerializeField] Transform _swordBase = null;
    [SerializeField] Transform _swordTip = null;
    [SerializeField] float _attackRange = 0.5f;
    [SerializeField] LayerMask _layersToHit;

    private void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
    }

    public void SwordAttack()
    {
        //Collider[] colliders = Physics.OverlapCapsule(_swordBase.position, _swordTip.position, _attackRange,_layersToHit);
        Collider[] colliders = Physics.OverlapSphere(_swordTip.position, _attackRange, _layersToHit);

        foreach (var hits in colliders)
        {
            Debug.Log("Hit" + hits.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_swordBase != null)
        {
            Gizmos.DrawWireSphere(_swordTip.position, _attackRange);
        }
    }
}

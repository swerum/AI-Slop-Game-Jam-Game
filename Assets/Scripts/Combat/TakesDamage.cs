using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BerserkPixel.StateMachine;
using System;

[RequireComponent(typeof(CollisionManager))]
public abstract class TakesDamage<T> : MonoBehaviour where T : StateMachine<T>
{
    [SerializeField] LayerMask[] _layerMasks;
    [SerializeField] GameObject _hitEffectPrefab;
    private T _stateMachine;
    private CollisionManager _collisionManager;

    void Start()
    {
        _stateMachine = GetComponent<T>();
        _collisionManager = GetComponent<CollisionManager>();
    }
    void Update()
    {
        if (_stateMachine.IsInvincible) return;
        foreach (Collider collider in _collisionManager.GetCollidingObjects())
        {
            CheckForHit(collider);
        }
    }
    private void CheckForHit(Collider col) {
        if (!IsInLayer(col.gameObject)) return;
        DoesDamage doesDamage = col.gameObject.GetComponent<DoesDamage>();
        if (!doesDamage || !doesDamage.isActive) return;
        Debug.Log("Damage Dealt to "+gameObject.name);
        _stateMachine.Hit(doesDamage.Damage);
        CreateHitEffect(col);
        
    }
    private bool IsInLayer(GameObject obj)
    {
        foreach (LayerMask mask in _layerMasks)
        {
            if ((mask & (1 << obj.layer)) != 0)
            {
                return true;
            }
        }
        return false;
    }
    private void CreateHitEffect(Collider col)
    {
        Vector3 closestPoint = col.ClosestPoint(transform.position);
        GameObject effect = Instantiate(_hitEffectPrefab);
        effect.transform.position = closestPoint;
    }
}

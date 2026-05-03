using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BerserkPixel.StateMachine;

// [RequireComponent(typeof(StateMachine<T>))]
public abstract class TakesDamage<T> : MonoBehaviour where T : StateMachine<T>
{
    [SerializeField] LayerMask[] _layerMasks;
    private T _stateMachine;

    void Start()
    {
        _stateMachine = GetComponent<T>();
    }
    private void OnTriggerEnter(Collider col) {
        if (!IsInLayer(col.gameObject)) return;
        DoesDamage doesDamage = col.gameObject.GetComponent<DoesDamage>();
        if (!doesDamage) return;
        _stateMachine.Hit(doesDamage.Damage);
        
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

}

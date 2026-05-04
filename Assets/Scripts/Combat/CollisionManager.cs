using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    List<Collider> _collidingObjects;
    Collider _collider;
    public List<Collider> GetCollidingObjects()
    {
        if (!_collider.enabled) return new List<Collider>();
        return _collidingObjects;
    }

    void Awake()
    {
        _collider = GetComponent<Collider>();
        _collidingObjects = new List<Collider>();
    }
    private void OnTriggerEnter(Collider col) {
        _collidingObjects.Add(col);
    }

    void OnTriggerExit(Collider col)
    {
        if (_collidingObjects.Contains(col))
        {
            _collidingObjects.Remove(col);
        }
    }

}

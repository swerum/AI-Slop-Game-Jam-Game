using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] LayerMask[] _layerMasks;
    List<GameObject> _collidingObjects;
    public List<GameObject> CollidingObjects { get {return _collidingObjects; }}

    void Start()
    {
        _collidingObjects = new List<GameObject>();
    }
    private void OnTriggerEnter(Collider col) {
        foreach (LayerMask mask in _layerMasks)
        {
            if ((mask & (1 << col.gameObject.layer)) != 0)
            {
                _collidingObjects.Add(col.gameObject);
                return;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (_collidingObjects.Contains(col.gameObject))
        {
            _collidingObjects.Remove(col.gameObject);
        }
    }

}

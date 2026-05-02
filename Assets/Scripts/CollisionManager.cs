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
    void OnCollisionEnter(Collision col)
    {
        if (_layerMasks.Contains(col.gameObject.layer ))
        {
            _collidingObjects.Append(col.gameObject);
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (_collidingObjects.Contains(col.gameObject))
        {
            _collidingObjects.Remove(col.gameObject);
        }
    }

}

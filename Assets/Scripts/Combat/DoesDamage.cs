using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DoesDamage : MonoBehaviour
{
    [SerializeField] private int _damage;
    public bool isActive = false;
    public int Damage {get { return _damage;}}
    void Start()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }
}

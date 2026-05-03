using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CreateLevelBounds : MonoBehaviour
{
    [SerializeField] float _wallThickness = 0.5f;
    [SerializeField] float _wallHeight = 20f;
    // Start is called before the first frame update
    void Start()
    {
        Bounds bounds = GetComponent<BoxCollider>().bounds;
        Vector3 min = new Vector2(bounds.min.x, bounds.min.z);
        Vector3 max = new Vector2(bounds.max.x, bounds.max.z);
        CreateBoundingWall(min, new Vector2(max.x, min.y+_wallThickness));
        CreateBoundingWall(min, new Vector2(min.x+_wallThickness, max.y));
        CreateBoundingWall(new Vector2(min.x, max.y-_wallThickness), max);
        CreateBoundingWall(new Vector2(max.x-_wallThickness, min.y), max);
    }
    private BoxCollider CreateBoundingWall(Vector2 min, Vector2 max)
    {
        GameObject obj = new GameObject("Bounding Wall");
        obj.transform.SetParent(transform);
        BoxCollider boxCollider = obj.AddComponent<BoxCollider>();
        Vector2 center = (min+max)/2;
        Vector2 size = max-min;
        Vector3 newCenter = new Vector3(center.x, 0, center.y);
        boxCollider.center = newCenter;
        boxCollider.size = new Vector3(size.x, _wallHeight, size.y);
        return boxCollider;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{

    public int rotationSpeed = 100;
    private Transform _itemTransform;

    // Start is called before the first frame update
    void Start()
    {
        _itemTransform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // It is recommended to always multiply movement or rotation speeds by Time.deltaTime to prevent differences in local systems
        _itemTransform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }
}

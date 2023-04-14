using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Transform tr;
    [SerializeField] private Transform followObject;
    [SerializeField] private Vector3 offset;

    private void Start() {
        this.tr = transform;
    }

    private void LateUpdate()
    {
        Vector3 calculatedPosition = new Vector3(followObject.position.x + offset.x, this.tr.position.y , followObject.position.z + offset.z);
        this.tr.position = calculatedPosition;
    }
}

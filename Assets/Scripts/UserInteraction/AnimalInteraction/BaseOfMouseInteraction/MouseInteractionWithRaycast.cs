using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteractionWithRaycast : MonoBehaviour
{

    protected LayerMask maskForInteract;

    protected virtual void Update() {
        if(Input.GetMouseButtonUp(0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, this.maskForInteract)){
                this.InteractedObject(hit.transform.gameObject);
            }
        }
    }

    protected virtual void InteractedObject(GameObject interactedObject){
        Debug.Log("Interacted Object is : " + interactedObject.name);
    }

}

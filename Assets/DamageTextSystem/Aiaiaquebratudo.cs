using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiaiaquebratudo : MonoBehaviour {

    public Transform parentTransform;

    private void Start()
    {
        //parentTransform = GetComponentInParent<Transform>().GetComponentInParent<Transform>();
        print(parentTransform.transform.position.x);

        Vector3 positionText = Camera.main.WorldToScreenPoint(parentTransform.transform.position);
        print(positionText.x + " " + positionText.y);
    }
}

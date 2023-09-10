using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalking : MonoBehaviour
{
    [SerializeField] GameObject NPCGameObject;
    [SerializeField] Transform NPCTransform;
    [SerializeField] Vector3 point1;
    [SerializeField] Vector3 point2;

    void Start()
    {
        point1 = new Vector3(NPCGameObject.transform.position.x + 1, NPCGameObject.transform.position.y, NPCGameObject.transform.position.z);
        point2 = new Vector3(NPCGameObject.transform.position.x, NPCGameObject.transform.position.y, NPCGameObject.transform.position.z);
    }

    void Update()
    {
        
    }

    void Move()
    {

    }


}

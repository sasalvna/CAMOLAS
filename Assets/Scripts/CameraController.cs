using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] 
    Transform Alvo;

    void LateUpdate()
    {
        transform.position = new Vector3 (Alvo.position.x, Alvo.position.y + 2f, transform.position.z);
    }
}
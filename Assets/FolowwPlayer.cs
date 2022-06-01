using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowwPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Transform playerTranSform;
    Vector3 deltaPos;
    void Start()
    {
        deltaPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTranSform.position + deltaPos;
    }
}

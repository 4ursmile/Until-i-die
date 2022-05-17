using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    private void Awake() {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

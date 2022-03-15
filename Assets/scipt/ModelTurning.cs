using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelTurning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void RotateMe(Vector3 direction)
    {
        direction = new Vector3(1, 0, 1);
        gameObject.transform.rotation = Quaternion.LookRotation(direction);
    }
    // Update is called once per frame
    void Update()
    {
        RotateMe(new Vector3(1, 0, 1));
    }
}

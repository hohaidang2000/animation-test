using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameoverscreen : MonoBehaviour
{
    public void Setup()
    {
        
        gameObject.SetActive(true);
        
    }
    public void Return()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
        
    }
}

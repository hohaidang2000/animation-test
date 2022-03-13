using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSequence : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform box1;
    public Transform box2;
    void Start()
    {
        //Random rnd = new Random();
        int num = 0;
        //int j = 2;
        int x = 0;
        int z = 0;
        int count = 0;
        for (int i = 20; i<= 100; i += 20)
        {
            for(int j = 20; j <=100; j+= 20)
            {   if (count < 500)
                {
                    num = Random.Range(0, 2);
                    x = Random.Range(i - 10, i);
                    z = Random.Range(j - 10, j);
                    if (num == 0)
                    {
                        Instantiate(box1, new Vector3(x, 0, -z), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(box2, new Vector3(x, 0, -z), Quaternion.identity);
                    }
                    count += 1;
                }
                
            }
            
        }
        Debug.Log(count);
            

           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

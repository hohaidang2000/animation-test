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
        for (int i = -230; i<= 230; i += 20)
        {
            for(int j = -230; j <=230; j+= 20)
            {   if(Mathf.Abs(i) == 10 && Mathf.Abs(j) == 10 )   
                {
                    continue;
                }
                if (count < 500)
                {
                    num = Random.Range(0, 2);
                    x = Random.Range(i - 8, i-2);
                    z = Random.Range(j - 8, j - 2);
                    if (num == 0)
                    {
                        Instantiate(box1, new Vector3(x, 0, z), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(box2, new Vector3(x, 0, z), Quaternion.identity);
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

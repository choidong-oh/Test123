using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tessst : MonoBehaviour
{
    public GameObject prefabs;
    public GameObject Tr;
    int count = 0;


    public void OnButton1()
    {
        var dd = Instantiate(prefabs, Tr.transform.position, Quaternion.identity);
        count += Direction(Vector3.up);
        //count += Direction(Vector3.right);  
        //count += Direction(Vector3.left);  
        //count += Direction(Vector3.down);  
        

    }

    private int Direction(Vector3 direction)
    {
        int count = 1;
        Vector3 startPosition = Tr.transform.position;

        for (int i = 0; i < 4; i++) 
        {
            Ray ray = new Ray(new Vector3(startPosition.x, startPosition.y+i*0.5f, startPosition.z),direction); 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 0.5f))
            {
                if (hit.collider.CompareTag("Black"))
                {
        Debug.Log("·¹ÀÌ¼Ø");
                    count++;
                }
                else
                {
                    break; 
                }
            }
            else
            {
                break; 
            }
        }

        return count;
    }



}

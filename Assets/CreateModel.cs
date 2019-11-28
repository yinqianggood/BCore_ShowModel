using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateModel : MonoBehaviour
{
    public GameObject go;
    public Transform parent;


    // Start is called before the first frame update
    void Start()
    {
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int z = 0; z < 3; z++)
                {
                    GameObject itemObj = Instantiate(go) as GameObject;
                    itemObj.transform.parent = parent;
                    itemObj.transform.localScale = Vector3.one;
                    itemObj.transform.localPosition = new Vector3(x, y, z);
                    itemObj.SetActive(true);
                }

            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

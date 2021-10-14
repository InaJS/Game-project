using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThisMono : MonoBehaviour
{
    public GameObject target;
    
    public void DestroyMe()
    {
        Destroy(target);   
    }
}

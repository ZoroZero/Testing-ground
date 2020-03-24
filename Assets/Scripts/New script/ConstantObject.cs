using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantObject : MonoBehaviour
{
    public static ConstantObject instance = null;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

}

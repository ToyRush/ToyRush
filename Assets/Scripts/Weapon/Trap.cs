using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traps
{  
    public abstract class Trap : MonoBehaviour
    {
        Rigidbody2D rb;
        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }
}


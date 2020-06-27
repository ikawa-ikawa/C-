using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea2 : MonoBehaviour
{
    int Flag;

    private void OnTriggerStay(Collider other)
    {
        if( ( other.CompareTag("Player") || other.CompareTag("Ally") ) && Flag == 0)
        {
            Flag = 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ally"))
        {
            Flag = 0;
        }
    }

    public int getFlag()
    {
        return Flag;
    }

    void Start()
    {
        Flag = 0;
    }

    void Update()
    {

    }
}

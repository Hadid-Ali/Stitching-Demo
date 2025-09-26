using System.Collections.Generic;
using UnityEngine;

public class NeedleTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> holes;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StitchPoint"))
        {
            if(!holes.Contains(other.gameObject)) 
                holes.Add(other.gameObject);
        }
    }
}

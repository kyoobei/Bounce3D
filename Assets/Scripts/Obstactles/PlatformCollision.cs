using UnityEngine;
using System;

public class PlatformCollision : MonoBehaviour
{
    public Action OnPlayerEnter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            OnPlayerEnter?.Invoke();
        }
    }
}

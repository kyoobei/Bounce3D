using UnityEngine;
using System;

public class PlatformCollision : MonoBehaviour
{
    public Action OnPlayerEnter;
    private Transform m_objectThatEntered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.SetParent(this.transform.parent, true);
            other.transform.localRotation = transform.localRotation;//Quaternion.identity;

            m_objectThatEntered = other.transform;
            OnPlayerEnter?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.parent = null;
            m_objectThatEntered = null;
        }
    }
    public void ForceRemoveParent()
    {
        if (m_objectThatEntered != null)
        {
            m_objectThatEntered.parent = null;
        }
    }
}

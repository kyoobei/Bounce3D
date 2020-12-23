using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] private int m_numberOfCopies = 0;
    [SerializeField] private GameObject m_objectToCopy;

    private List<GameObject> m_releasedCopies = new List<GameObject>();
    private Queue<GameObject> m_queueCopies = new Queue<GameObject>();

    private void Start()
    {
        for(int i = 0; i < m_numberOfCopies; i++)
        {
            GameObject go = Instantiate(m_objectToCopy);
            go.transform.SetParent(this.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            go.SetActive(false);
            m_queueCopies.Enqueue(go);
        }
    }
    public GameObject GetClone()
    {
        GameObject clone = null;
        if(m_queueCopies.Count > 0)
        {
            clone = m_queueCopies.Dequeue();
            clone.SetActive(true);
            m_releasedCopies.Add(clone);
        }
        else if(m_queueCopies.Count <= 0)
        {
            clone = Instantiate(m_objectToCopy);
            clone.transform.SetParent(this.transform);
            clone.transform.localPosition = Vector3.zero;
            clone.transform.localRotation = Quaternion.identity;
            clone.transform.localScale = Vector3.one;
            m_releasedCopies.Add(clone);
        }
        return clone;
    }
    public void ReturnClone(GameObject clone)
    {
        if (m_releasedCopies.Contains(clone))
        {
            m_releasedCopies.Remove(clone);
            clone.transform.localPosition = Vector3.zero;
            clone.transform.localRotation = Quaternion.identity;
            clone.transform.localScale = Vector3.one;
            clone.SetActive(false);
            m_queueCopies.Enqueue(clone);
        }
    }
    public void ReturnAllClone()
    {
        if(m_releasedCopies.Count <= 0)
        {
            return;
        }    
        for(int i = 0; i < m_releasedCopies.Count; i++)
        {
            m_releasedCopies.Remove(m_releasedCopies[i]);
            m_releasedCopies[i].transform.localPosition = Vector3.zero;
            m_releasedCopies[i].transform.localRotation = Quaternion.identity;
            m_releasedCopies[i].transform.localScale = Vector3.one;
            m_releasedCopies[i].SetActive(false);
            m_queueCopies.Enqueue(m_releasedCopies[i]);
            continue;
        }
    }
}

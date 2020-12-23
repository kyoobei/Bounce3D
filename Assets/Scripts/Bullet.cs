using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float TIMER_RETURN = 3f;
    [SerializeField] private float m_bulletStrength = 5f;
    [SerializeField] private Rigidbody m_bulletRigidBody;
    private Pooler m_pooler;
    public void SetPoolerForReturning(Pooler pooler)
    {
        m_pooler = pooler;
    }
    public void Shoot()
    {
        m_bulletRigidBody.AddForce(Vector3.forward * m_bulletStrength, ForceMode.Impulse);
        Invoke("Return", TIMER_RETURN);
    }
    private void Return()
    {
        m_pooler.ReturnClone(this.gameObject);
    }
}

using UnityEngine;

public class Bullet : MonoBehaviour, IDamageable
{
    Health healthy;
    int damageReduction;
    int damageGiven = 50;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            healthy = collision.gameObject.GetComponent<Health>();
            Damage(damageGiven);
        }
        Destroy(gameObject);
    }
    public void Damage(int damageGiven)
    {
        if (healthy.hasShield)
        {
            damageReduction = Random.Range(40, 50);
            damageGiven = damageReduction;
        }
        healthy.totalHealt -= damageGiven;

    }
}

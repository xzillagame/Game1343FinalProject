using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Damager : MonoBehaviour
{
    [SerializeField] private int Damage;
    public event UnityAction OnDamagerHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damaged = collision.GetComponent<Damageable>();
        if (damaged != null)
        {
            damaged.TakeDamage(Damage);
        }
        OnDamagerHit?.Invoke();
    }



    private void OnDisable()
    {
        OnDamagerHit = null;
    }


}

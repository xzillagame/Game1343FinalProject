using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int hp = 5;
    public int CurrentHP
    {
        get { return hp; }

        private set 
        {
            hp = value;

            if(hp <= 0)
            {
                WhenDied();
            }
        }
    }


    [SerializeField] private UnityEvent OnDied;
    [SerializeField] private UnityEvent OnDamaged;



    public void TakeDamage(int damage)
    {
        OnDamaged?.Invoke();
        CurrentHP -= damage;
    }

    private void WhenDied()
    {
        OnDied?.Invoke();
    }


}

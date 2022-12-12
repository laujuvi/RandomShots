using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class LifeController : MonoBehaviour
{
    public int maxLife;
    public int currentLife;
    public int CurrrentHealth { get => currentLife; set => currentLife = value; }

    public UnityEvent OnHealthChange = new UnityEvent();
    private void Update()
    {
      
    }
    public void InitializateLife()
    {
        currentLife = maxLife;
        gameObject.SetActive(true);
    }
    public void GetDamage(int damage)
    {
        currentLife -= damage;
        OnHealthChange.Invoke();
        Debug.Log("Damage: " + damage);

        if (currentLife <= 0)
        {
            currentLife = 0;
            OnHealthChange.Invoke();
            Death();
        }
    }
    public void Death()
    {
        gameObject.SetActive(false);
    }
    public float GetPorcentageHealth()
    {
        return (float)currentLife / maxLife;
    }

}

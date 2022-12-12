using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class BarHealthController : MonoBehaviour
{
    [SerializeField] private LifeController healthController;
    [SerializeField] private Image healthBar;



    private void Awake()
    {

        healthController.OnHealthChange.AddListener(OnHealthChangeHandler);


    }

    private void OnHealthChangeHandler()
    {
        healthBar.fillAmount = healthController.GetPorcentageHealth();
    }

}

using System.Collections;
using System.Collections.Generic;
using Behaviours;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public bool regen = false;

    [SerializeField] public Image healthBar;
    [SerializeField] public float Rate = 0.1f;
    [SerializeField] internal float waitTime = 0.1f;

    private Bar _bar;
    public Bar Bar {
        get{
            if(_bar == null){
                _bar = new Bar(0.1f);
                return _bar;
            }
            return _bar;
        }
    }

    public void DecreaseHealth() => Bar.Decrease(Rate);
    public void DecreaseHealth(float rateToDecrease) => Bar.Decrease(rateToDecrease);
    
    public void IncreaseHealth() => Bar.Increase(Rate); 
    public void IncreaseHealth(float rateToIncrease) => Bar.Increase(rateToIncrease); 
    
    void Update()
    {
        regen = Bar.IsRegenerating;
        Bar.Regen(Time.deltaTime, waitTime);
        healthBar.fillAmount = Bar.Value;
    }
}

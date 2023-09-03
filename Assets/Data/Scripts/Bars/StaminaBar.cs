using System.Collections;
using System.Collections.Generic;
using Behaviours;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public bool regen = false;

    [SerializeField] public Image staminaBar;
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

    public void DecreaseStamina() => Bar.Decrease(Rate);
    public void DecreaseStamina(float rateToDecrease) => Bar.Decrease(rateToDecrease);
    
    public void IncreaseStamina() => Bar.Increase(Rate); 
    public void IncreaseStamina(float rateToIncrease) => Bar.Increase(rateToIncrease); 
    
    void Update()
    {
        regen = Bar.IsRegenerating;
        Bar.Regen(Time.deltaTime, waitTime);
        staminaBar.fillAmount = Bar.Value;
    }
}

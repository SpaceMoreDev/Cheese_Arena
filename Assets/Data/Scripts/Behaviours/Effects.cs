using UnityEngine;


public enum EffectTypes{
    RestoreHealth,
    RestoreStamina,
    IncreaseAttack,
    DecreaseAttack

}

public class Effects
{
    delegate void Ability(float value, GameObject target);
    private Ability _activeAbility;
    
    public void RestoreHealth(float value, GameObject target)
    {
        target.GetComponent<HealthBar>().IncreaseHealth(value);
    }
    public void RestoreStamina(float value, GameObject target)
    {
        target.GetComponent<StaminaBar>().IncreaseStamina(value);
    }
    public void IncreaseAttack(float value, GameObject target)
    {

    }
    public void DecreaseAttack(float value, GameObject target)
    {

    }

    public Effects(EffectTypes effect)
    {
        switch(effect)
        {
            case EffectTypes.RestoreHealth:
                _activeAbility = new Ability(RestoreHealth);
                break;
            case EffectTypes.RestoreStamina:
                _activeAbility = new Ability(RestoreStamina);
                break;
            case EffectTypes.IncreaseAttack:
                _activeAbility = new Ability(IncreaseAttack);
                break;
            case EffectTypes.DecreaseAttack:
                _activeAbility = new Ability(DecreaseAttack);
                break;
        }
    }

    public void Activate(float value, GameObject target)
    {
        _activeAbility(value, target);
    }
}

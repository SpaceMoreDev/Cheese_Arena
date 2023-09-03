using System;

public enum SKILL_TYPE{
    STRENGTH,
    SPEED,
    INTELLECT
}
public class Skills{
    private int _strength = 1;
    private int _speed = 1;
    private int _intellect = 1;

    public event Action<SKILL_TYPE> AddedSkill;
    public event Action<SKILL_TYPE> RemoveSkill;

    public void IncreaseSkill(SKILL_TYPE skill)
    {
        switch(skill)
        {
            case SKILL_TYPE.STRENGTH:
                _strength++;
                break;
            case SKILL_TYPE.SPEED:
                _speed++;
                break;
            case SKILL_TYPE.INTELLECT:
                _intellect++;
                break;
        }        

        AddedSkill?.Invoke(skill);
    }
    
    public void DecreaseSkill(SKILL_TYPE skill)
    {
        switch(skill)
        {
            case SKILL_TYPE.STRENGTH:
                _strength--;
                break;
            case SKILL_TYPE.SPEED:
                _speed--;
                break;
            case SKILL_TYPE.INTELLECT:
                _intellect--;
                break;
        }   
        
        RemoveSkill?.Invoke(skill);

    }
}
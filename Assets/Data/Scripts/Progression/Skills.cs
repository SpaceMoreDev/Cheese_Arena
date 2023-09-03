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

    public int Strength{get=>_strength; set=>_strength=value;}
    public int Speed{get=>_speed; set=>_speed=value;}
    public int Intellect{get=>_intellect; set=>_intellect=value;}

    public event Action<SKILL_TYPE> AddedSkill;
    public event Action<SKILL_TYPE> RemoveSkill;

    public Skills(int strength = 0, int speed = 0, int intellect = 0)
    {
        _strength = strength;
        _speed = speed;
        _intellect = intellect;
    }

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

    public int GetSkillNum(SKILL_TYPE skill)
    {
        switch (skill)
        {
            case SKILL_TYPE.STRENGTH:
                return _strength;
            case SKILL_TYPE.SPEED:
                return _speed;
            case SKILL_TYPE.INTELLECT:
                return _intellect;
            default:
                break;
        }
        return -1;
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
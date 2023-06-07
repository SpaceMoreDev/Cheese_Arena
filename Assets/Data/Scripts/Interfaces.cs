public interface IActivites
{
    int ID {set; get;}
    void Activate(CharacterObject sender ,int id);
}

public interface IAbilities
{
    int AbilityID {set; get;}
    string AbilityName {set; get;}
    string AbilityDescription {set; get;}
    void StartAbility();
    void Update();
}
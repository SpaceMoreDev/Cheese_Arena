using System.Collections.Generic;
using System;
public class Quest
{
    private string _name;
    private bool _started = false;
    private bool _ended = false;
    private List<Quest> _listedQuests = new List<Quest>();

    // ------ Action events -------
    public event Action QuestCompleted;
    public event Action QuestStarted;

    public Quest(string name, bool state = false){
        this._name = name;
        this._started = state;
    }

    /// <summary>
    /// Add a quest to the current active QuestLine.
    /// </summary>
    /// <param name="quest"></param>
    public void Add(Quest quest)
    {
        this._listedQuests.Add(quest);
    }

    public void Remove(Quest quest)
    {
       this._listedQuests.Remove(quest);
    }

    public static void EndQuest( Quest quest )
    {
        if(quest._listedQuests.Count == 0){
            quest._ended = true;
            quest.QuestCompleted?.Invoke();
        }
    }

    public static void StartQuest( Quest quest )
    {
        quest._started = true;   
        quest.QuestStarted?.Invoke();
    }
}

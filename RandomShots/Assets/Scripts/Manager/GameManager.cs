using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
     public Character character;

    private List<ICommand> _events = new List<ICommand>();

    public void AddEventQueue(ICommand command) => _events.Add(command);

    void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }

    void Update()
    {
        if (character.isActiveAndEnabled == false)
        {
            SceneManager.LoadScene(4); 
         
        }
        foreach (var command in _events)
            command.Execute();

        _events.Clear();
    }
}

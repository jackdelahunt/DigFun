using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimedEvent 
{
    private static Dictionary<string, TimedEvent> timedEvents = new Dictionary<string, TimedEvent>();

    private Action action;
    private float time;
    private string name;
    private GameObject gameObject;

    private TimedEvent(Action action, float time, string name, GameObject gameObject) {
        this.action = action;
        this.time = time;
        this.name = name;
        this.gameObject = gameObject;
    }

    // creates and adds a new timed event to the timedEvents dictionary
    public static void create(Action action, float time, string name) {

        TimedEvent timedEvent;

        // if this timed event already exists then do not create a copy
        if(!timedEvents.TryGetValue(name, out timedEvent)) {
            
            // create a new game object with the mono hook script
            GameObject gameObject = new GameObject(name, typeof(MonoHook));

            // create the new timed event and give it a refrence to it's game object
            timedEvent = new TimedEvent(action, time, name, gameObject);

            // add this timed event to the timeEvents dictionary with it's name as the key
            timedEvents.Add(name, timedEvent);

            // get the action in the mono hook script and execute it 
            gameObject.GetComponent<MonoHook>().action = timedEvent.execute;
        }
    }

    // will decrease the time each frame until 0
    public void execute() {
        time -= Time.deltaTime;

        // when the time reaches zero then execute the action
        // remove the refrence from the timedEvents dictionary
        // and destroy the gmae object
        if(time < 0) {
            action();
            timedEvents.Remove(name);
            destroySelf();
        }
    }

    private void destroySelf() {
        UnityEngine.Object.Destroy(gameObject);
    }

    // mono script to call the execute function in the timed event
    public class MonoHook : MonoBehaviour {

        public Action action;

        private void Update() {
            if (action != null) 
                action();
        }
    }

}

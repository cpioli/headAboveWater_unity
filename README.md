# headAboveWater_unity
Clean version of the Unity2d Implementation of Head Above Water

This is an implementation of a prototype I developed in my graduate program. The focus wasn't to create any new gameplay, but to rebuild the mechanics using the features of Unity3D, most significantly Scriptable Objects.

<h2>Scriptable Objects</h2>

I used Scriptable Objects to encapsulate systems and relationships between objects. The approach I took was laid out by Ryan Hipple in his talk on the subject (summarized in an article [here](https://unity3d.com/how-to/architect-with-scriptable-objects)): to decouple complex systems and make it easier to control data outside of code by designers. Here are a few features I implemented

The idea is to assign every Unity Prefab a single MonoBehaviour script, and that script would act as a platform to access systems of scriptable objects

<h3>Player Movement State System</h3>

Using the State Pattern, I built a series of state classes that decoupled controller code as well as environment variables from the player-character. An instance of each state is created, and I input values to track what states they can branch off to, the current movement restrictions, and other features. In each Class's code, variables are updated for the Player's Sprite's State System. You can find the source code for these objects [here](https://github.com/cpioli/headAboveWater_unity/tree/master/Assets/Scripts/ScriptableObjects/StateSystems/PlayerMovement).

The benefit of this system is that it's easy to provide updates. I can isolate the code I have to modify to a single state without having to check a set of booleans, I can add a new feature (climbing, for instance) just by adding a few new lines of code. It's really, truly helpful. If I wanted to take this system to the next level, I would design a generic "state" ScriptableObject - no unique classes for individual states - then I would write a series of ScriptableObjects that would run the instructions I wrote up in code.

Note: a similar system was also used to handle [Game States](https://github.com/cpioli/headAboveWater_unity/tree/master/Assets/Scripts/ScriptableObjects/StateSystems/GameStates).

<h3>Event Management</h3>

I used the Observer pattern to force objects to subscribe to particular events in the `Awake()` method, as described by Ryan Hipple in his talk. The customization was incredibly handy, but his system required one EventListener script to listen to one single event, and made it difficult for complex objects to house a large number of these events and their inspectors would be difficult to navigate. So I made a few adjustments to suit my needs.

I changed GameEventListeners from MonoBehaviours to Scriptable Objects, then I created a script `GameEventListenerList` to house a list of all the events that GameObject would contain. That reduced the number of MonoBehaviours required for event listening to one per game object. Each GameEventListener was provided an additional string so the designer could provide an explicit label for an Event Listener. 

Going further, I created a script `CommonGameEventListenerList` to automatically add event listeners from any game object implementing methods from `ICommonGameEvents` in the `OnEnable()` method (and remove the event listeners in the `OnDisable()` method). `ICommonGameEvents` contains method signatures for common events such as Game Over, Level Begin, and Paused, so this automation technique reduces some of the "boilerplate point-and-click" in the designer's workload. The script was also placed inside a GameObject, so the designer can drag-and-drop it into any Parent GameObject that must respond to common events like 'LevelBegin' or 'GameOver'.

The programmer benefits from this system as well: if a designer adds the "CGELL GameObject" to a Parent GameObject that hasn't implemented `ICommonGameEvents`, the script will raise a warning in the Console, which remind the programmer to implement those methods. Additionally, methods can be added and removed easily.

You can find the Event classes [here](https://github.com/cpioli/headAboveWater_unity/tree/master/Assets/Scripts/ScriptableObjects/Events)

<h3>Optimized collision handler</h3>

COMING SOON! Small summary: I created a HashTable for "climbing collisions" when the Swimmer had to grab onto a ledge instead of asking the engine to handle all 30+ circle colliders.

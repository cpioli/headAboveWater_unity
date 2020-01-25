# headAboveWater_unity
Clean version of the Unity2d Implementation of Head Above Water. Developed in Unity3D version 2018.2.15f1.

This is an implementation of a prototype I developed in my graduate program. The focus wasn't to create any new gameplay, but to rebuild the mechanics using the features of Unity3D, most significantly Scriptable Objects.

<h2>Scriptable Objects</h2>

I used Scriptable Objects to encapsulate systems and relationships between objects to create a library of objects allowing a designer to rapidly prototype new ideas and systems. The approach I took was laid out by Ryan Hipple in his talk on Scriptable Objects (summarized in an article [here](https://unity3d.com/how-to/architect-with-scriptable-objects)): to decouple complex systems and make it easier to control data outside of code, making it more accessible by designers. Here are a few features I implemented.

<h3>Player Movement State System</h3>

Using the State Pattern, I built a series of state classes that decoupled controller code as well as environment variables from the player-character to create a State Machine for movement. An instance of each state is written in code (inheriting ScriptableObject), and then I created instances of them. In those instances I input values to track what states they can branch off to, the state's movement restrictions, the player's velocity, and other features. In each state's begin and end functions, variables are updated for the Player's Sprite's State System. You can find the source code for these objects [here](https://github.com/cpioli/headAboveWater_unity/tree/master/Assets/Scripts/ScriptableObjects/StateSystems/PlayerMovement).

The benefit of this system is that it's easy to update with bug fixes and new features. I can isolate the code I have to modify to a single state without having to check a set of booleans, I can add a new feature (climbing, for instance) just by adding a few new lines of code. It's really, truly helpful.

There is a way to improve this system for designers: because many of the instructions in the OnStateStart, OnStateExit, and ComputeVelocity methods are reused over time, I can encapsulate those into descendants of ScriptableObjects using the Delegate Pattern. Then the designer creates them and inserts them into a list that's run in either the Start, Exit, or Velocity methods.

Note: a similar system was also used to handle [Game States](https://github.com/cpioli/headAboveWater_unity/tree/master/Assets/Scripts/ScriptableObjects/StateSystems/GameStates).

<h3>Event Management</h3>

I used the Observer pattern in to construct a MonoBehaviour script named "GameEventListener" that, when attached to a GameObject, will subscribe it to an event in the MonoBehaviour's `Awake()` method, as described in the Unity3d article I linked above. Events were ScriptableObjects that kept a List of Listeners, and when you dropped an Event onto an instance of GameEventListener's UnityEvent object, you could call a method on any Script accessible from that GameObject. 

The customization was incredibly handy, but this system required one EventListener script to listen to one single event, making it difficult to navigate complex objects in the Inspector when large number of EventListeners exist. So I made a few adjustments to suit my needs: I changed GameEventListeners from MonoBehaviours to Scriptable Objects, then I created a script `GameEventListenerList` to house a list of all the events that GameObject would contain. Now all GameObjects only need one GameEventListenerList, and it's more user-friendly. Each GameEventListener was provided an additional string so the designer could add a label for each Event Listener. 

Going further, I wanted to minimize the workload on designers by automating a few common events across scripts, so I encapsulated common events into a pair of scripts: `ICommonGameEvents.cs` and `CommonGameEventListenerList.cs`. When a MonoBehaviour script implements `ICommonGameEvents`, it is granted access to several methods that modify the owning GameObject's parameters when common game events occur such as Game Over and Level Begin. It's sibling script, `CommonGameEventListenerList`, will search a GameObject for any scripts implementing `ICommonGameEvents`, and for each instance their common event methods will be registered with the corresponding events and respond automatically.

The programmer benefits from this system as well: if a designer adds `CommonGameEventListenerList.cs` to a GameObject that hasn't implemented `ICommonGameEvents`, the former script will raise a warning in the Console, enforcing reminders to implement common events before the game ships.

You can find the Event classes [here](https://github.com/cpioli/headAboveWater_unity/tree/master/Assets/Scripts/ScriptableObjects/Events)

<h2>Other Accomplishments</h2>

<h3>Optimized collision handler</h3>

For the addition of the "ledge hanging" feature, the Swimmer must attach themselves to a ledge, and when the player taps the jump button the swimmer climbs over the ledge. My biggest challenge was creating the conditions to trigger this event, and my solution was to attach `CircleCollider2d` objects to every left/right ledge tile in a given Tilemap. I felt this could easily become tedious if done by hand, so I created an automated solution: in a class `TileMapBehaviour`, I searched the TileMap for all ledge tiles, and when one is found, a Circle Collider 2d is retrieved from a pre-existing list and placed over the corresponding ledge tile. The designer is responsible for identifying ledge tiles, which is a `public List<Sprite>` in the script.

I optimized this system by minimizing the number of `CircleCollider2d` a level required: I used a hashtable to divide the level into "sections," where each section is 16 tiles wide. Each section has a list which will contain the coordinates to place all CircleCollider2D objects when the Swimmer enters that section (and reset the colliders to the origin point when the Swimmer leaves). When `TileMapBehaviour` is searching for ledge tiles in the Start() method, the position where a collider should be placed is inserted in a Dictionary<int, List<int, int>>, whose key is the Swimmer's current tile location divided by 16 (the width of each "section"). Doing so, levels only require 8 - 10 CircleCollider2d components maximum.

The code that performs this can be found [here](https://github.com/cpioli/headAboveWater_unity/blob/master/Assets/Scripts/Behaviours/TileMapBehaviour.cs)

## BackGround Information
This 2D platformer was created as a way to learn new design patterns and test my programming ability with new game mechanics.


## State Changing When Wall Jumping
{% include youtubePlayer.html id="CFnqkuUrwbM" %}
This video demonstrates the wall jumping mechanic in Azer the Well Kept Lie. Each time the player's animation changes to be attached to the wall you are able to visually see the change from its current state to the WallSlide state. When you are in the WallSlide state there are certain restrictions put in place, such as being unable to attack or run. These restrictions as well as what to do on enter, exit, and update of the state is managed by a class which inherits from the abstract State class.

```csharp
public abstract class State
    {
        protected StateMachine stateMachine;

        protected State(StateMachine _stateMachine)
        {
            stateMachine = _stateMachine;
        }

        public virtual void Enter()
        {

        }

        public virtual void HandleInput()
        {

        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }
```

What manages which state is the current state, as well as changing states and running the Enter() and Exit() functions is the StateMachine class. This class is instantiated on different Unity MonoBehaviours that will be utilizing states. Examples are the Player Script and most enemy scripts.

```csharp
public class StateMachine
    {
        public State CurrentState { get; private set; }

        private Dictionary<Type, State> availableStates;

        public void SetStates(Dictionary<Type, State> _availableStates)
        {
            availableStates = _availableStates;
        }

        public void Initialize(Type startingState)
        {
            CurrentState = availableStates[startingState];
            CurrentState.Enter();
        }

        public void ChangeState(Type nextState)
        {
            CurrentState.Exit();
            CurrentState = availableStates[nextState];
            CurrentState.Enter();
        }
    }
```



## Combat and Enemies
{% include youtubePlayer.html id="4_ou2wtyHuA" %}
This video demonstrates the combat capabilities in my game as well as a some enemies. If you watch carefully in the first part of the video you will notice that the enemy is roaming until the player gets into view. Each enemy has its own roam and aggro states as well as components that make itself up. Below is the Enemy class that each enemy object in Unity will have. For each enemy there are also special controller classes that each one has. Each controller class is a composition of multiple other classes(components). Each of these components work together without inherently interacting directly with each other, therefore it is a one way dependency in which the controller classes functionality is dependent on its components.

```csharp
[System.Serializable]
    public class Enemy
    {
        public enum EnemyTypes
        {
            RaizeLeaper,
            Slime,
        }

        [field: SerializeField] public int Health { get; private set; }

        [field: SerializeField] public int Dmg { get; private set; }

        [field: SerializeField] public EnemyTypes EnemyType { get; private set; }

        public bool InAggro { get; set; }

        public Enemy(int _dmg, EnemyTypes _enemyType, int _health)
        {
            Dmg = _dmg;
            EnemyType = _enemyType;
            Health = _health;
        }
    }

    public interface IEnemy
    {
        Enemy ParentEnemy { get; }
    }
```



## Pub Sub Aggregator System used with Pressure Plates
{% include youtubePlayer.html id="r28y2OuftRo" %}
This video demonstrates how the Pub Sub pattern utilizing an Aggretator was implemented to have pressure plates that lifted doors. The Pub Sub design pattern is used to reduce dependencies between publishers and subscribers, the aggregator allows even less dependency. So much so that the publishers do not need to know of what subscribers there are. Instead they just invoke any actions that have been subcribed. Each action needs a given message as its parameter and the publishers will pass their message object when invoking them. There cannot be multiple Aggregators and so we create a Unity singleton.

```csharp
public class EventAggregator : MonoBehaviour
{
    private readonly Dictionary<Type, IList> subscribers = new Dictionary<Type, IList>(); // Dictionary that contains all the subscribers with the key representing the message type

    public static EventAggregator SingleInstance { get; private set; }


    private void Awake()
    {
        if(SingleInstance == null)// Unity Singleton
        {
            SingleInstance = this;
        }
    }




    public void Publish<T>(T message)
    {
        ///<summary>
        /// Publishes all the subscribers in the list from the key of the same
        /// type as the message.
        /// The list is generated by casting the Generic IList to a List of Subscriptions
        /// whose type correlates to the message type T.
        ///</summary>
        
        IList actionList;

        if (subscribers.ContainsKey(typeof(T)))
        {
            actionList = new List<Subscription<T>>(subscribers[typeof(T)].Cast<Subscription<T>>());

            if (actionList.Count > 0)
            {
                foreach (Subscription<T> sub in actionList)
                {
                    sub.Action?.Invoke(message);
                }
            }
            else
                Debug.Log("No Sub Found");
        }
        else
            throw new NullReferenceException();
    }



    public Subscription<T> Subscribe<T>(Action<T> action)
    {
        ///<summary>
        /// Given an action whose parameter is an object of type T, this
        /// function will generate a subscription of type T and add it to an
        /// existing or new list in the subscribers dictionary.
        /// This function returns the subscription in case it needs to be removed
        /// by the subscriber.
        ///</summary>
        
        Subscription<T> subscription = new Subscription<T>(action, SingleInstance);

        if(subscribers.TryGetValue(typeof(T), out IList actionList))
        {
            actionList.Add(subscription);
        }
        else
        {
            actionList = new List<Subscription<T>> { subscription };
            subscribers.Add(typeof(T), actionList);
        }

        return subscription;
    }



    public void Unsubscribe<T>(Subscription<T> subscription)
    {
        if (subscribers[typeof(T)].Contains(subscription))
        {
            int indexToRemove = subscribers[typeof(T)].IndexOf(subscription);

            subscribers[typeof(T)].RemoveAt(indexToRemove);
        }
    }


    public void ClearAllSubscriptions()
    {
        subscribers.Clear();
    }
}
```

The first block of code below is the message that can be passed into the Subscriber, and second is the Subscriber itself. As you can see the Subscriber has a action to invoke which takes in a specific message. In this case the message is a OpenDoorEventType object. We only need to subscribe this once even though there are two doors in the level because there are two different publishers each passing a different message for the subscriber's action to run. Each message contains a different door.

```csharp
public class OpenDoorEventType : MonoBehaviour
{
    [field: SerializeField] public float GravityChange { get; set; } = 0f;
    [field: SerializeField] public Rigidbody2D DoorRb { get; set; } = null; // The door's physics manager
}
```

```csharp
public class OpenDoorSubscriber : MonoBehaviour
{
    private Subscription<OpenDoorEventType> subscription;

    private void Start()
    {
        subscription = EventAggregator.SingleInstance.Subscribe<OpenDoorEventType>(OpenDoor);
    }

    private void OpenDoor(OpenDoorEventType openDoorEvent)
    {
        openDoorEvent.DoorRb.gravityScale = openDoorEvent.GravityChange;
    }
}
```

In Azer The Well Kept Lie an example of a publisher is the pressure plate you can see working in the Pub Sub video. The pressure plate will simply detect any colliders entering its trigger using the Unity function OnTriggerEnter() and run certain Subscribers in accordance to either given or not given objects in the Unity editor. The Code below is the pressure plate Publisher.

```csharp
public class OpenDoorStopBarrierOnPressurePlate : MonoBehaviour
{
    // Objects given or not given through the Unity Editor
    [SerializeField] private OpenDoorEventType doorEventType = null; 
    [SerializeField] private DisableGameObjectEventType disableObjectType = null;

    public bool isPressed = false;

    private float doorEventTypeOpenSpeed = 0;

    private void Awake()
    {
        doorEventTypeOpenSpeed = doorEventType.GravityChange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isPressed)
        {
            isPressed = true;
            if (doorEventType != null)
            {
                doorEventType.GravityChange = doorEventTypeOpenSpeed;
                EventAggregator.SingleInstance.Publish(doorEventType); // publishes subscribers of type OpenDoorEventType
            }
            if(disableObjectType != null)
                EventAggregator.SingleInstance.Publish(disableObjectType); // publishes subscribers of type DisableGameObjectEventType
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(isPressed)
        {
            isPressed = false;
            if (doorEventType != null)
            {
                doorEventType.GravityChange = 1;
                EventAggregator.SingleInstance.Publish(doorEventType);
            }
        }
    }
}
```

## Composition with Entities
In the folder **Assets/Scripts/Entites/EntityComponents** you can see all of the components that make up the general entity. These components were created based on the use of Composition. Where each entity will have a controller that contains multiple of these components. The combination of these components will make up a majority of what the entity can do. The use of Composition allowed the reusability of a big portion of the code. For example instead of rewriting the code for ground checking for each entity, by creating a component out of it I was able to use the same code for every entity without having to rewrite. Below is the CheckEntityGrounded component which any entity that needs to know when it is grounded will instantiate in their controller.

```csharp
public class CheckEntityGrounded
    {
        private readonly Rigidbody2D rb;
        private readonly LayerMask floor;
        private readonly BoxCollider2D boxCollider;
        private readonly float distance;

        public bool IsGrounded { get; set; }

        public Action OnGrounded { get; set; }
        public Action WhenNotGrounded { get; set; }

        public CheckEntityGrounded(Rigidbody2D rb, LayerMask floor, BoxCollider2D entityBoxCollider, float _distance)
        {
            this.rb = rb;
            this.floor = floor;
            boxCollider = entityBoxCollider;
            distance = _distance;
        }

        public void GroundCheck()
        {
            if (rb.velocity.y <= 0)
            {
                RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, distance, floor);

                if (hit)
                {
                    IsGrounded =  true;
                    OnGrounded?.Invoke();
                }
                else
                {
                    WhenNotGrounded?.Invoke();
                    IsGrounded = false;
                }
            }
        }
    }
```


## Unit Testing
I was able to utilize Unity's unit testing to create scripts that allowed me to test the functionality of other scripts. The use of unit testing is to make sure that certain important pieces of code that work, stay working and are not touched during the programming process. Not only this, but it allows me to find bugs easier because I can simply run a unit test to understand what already works and what doesn't. This narrows down my search making the most tedius part of programming a much easier task. Below is an example of a unit test for the health manager, which was important to create because many bugs are involved with health management.

```csharp
public class HealthManagerTests
    {
        [Test]
        public void Player_HealthManager_HitNegative_Exception()
        {
            PlayerHealthManager healthManager = new GameObject().AddComponent<PlayerHealthManager>();
            healthManager.SetMaxHealth();

            Assert.Throws<ArgumentOutOfRangeException>(() => healthManager.Hit(-1));
        }

        [Test]
        public void Player_HealthManager_HitTest()
        {
            PlayerHealthManager healthManager = new GameObject().AddComponent<PlayerHealthManager>();
            healthManager.SetMaxHealth();
            healthManager.MaxHeal();

            int maxHealth = healthManager.CurrentHealth;

            healthManager.Hit(1);

            Assert.IsTrue(healthManager.CurrentHealth == maxHealth - 1);
        }

        [Test]
        public void Enemy_HealthManager_HitTest()
        {
            EnemyHealthManager healthManager = new GameObject().AddComponent<EnemyHealthManager>();
            healthManager.SetMaxHealth();
            healthManager.MaxHeal();

            int maxHealth = healthManager.CurrentHealth;

            healthManager.Hit(2);

            Assert.IsTrue(healthManager.CurrentHealth == maxHealth - 2);
        }

        [Test]
        public void Enemy_HealthManager_HitNegative_Exception()
        {
            EnemyHealthManager healthManager = new GameObject().AddComponent<EnemyHealthManager>();
            healthManager.SetMaxHealth();

            Assert.Throws<ArgumentOutOfRangeException>(() => healthManager.Hit(-1));
        }

        [Test]
        public void HealthManager_HealTest()
        {
            PlayerHealthManager healthManager = new GameObject().AddComponent<PlayerHealthManager>();
            healthManager.SetMaxHealth();
            healthManager.MaxHeal();

            healthManager.Hit(1);

            int tempHealth = healthManager.CurrentHealth;

            healthManager.Heal(1);

            Assert.IsTrue(healthManager.CurrentHealth == tempHealth + 1);
        }

        [Test]
        public void HealthManager_HealWithMaxHealthTest()
        {
            PlayerHealthManager healthManager = new GameObject().AddComponent<PlayerHealthManager>();
            healthManager.SetMaxHealth();
            healthManager.MaxHeal();

            int maxHealth = healthManager.CurrentHealth;

            healthManager.Heal(1);

            Assert.IsTrue(healthManager.CurrentHealth == maxHealth);
        }

        [Test]
        public void HealthManager_HealNegative_Exception()
        {
            PlayerHealthManager healthManager = new GameObject().AddComponent<PlayerHealthManager>();
            healthManager.SetMaxHealth();

            Assert.Throws<ArgumentOutOfRangeException>(() => healthManager.Heal(0));
        }
    }
```

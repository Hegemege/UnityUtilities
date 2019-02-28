# UnityUtilities

A set of C# scripts I commonly use and find helpful in Unity, especially during game jams.

-   FloatExtensions.cs

    -   `Map(x1, x2, y1, y2)` for mapping a float from one range to another

-   ForceCameraAspectRatio.cs

    -   MoboBehaviour that can be attached to any GameObject with a Camera component to force the aspect ratio of the Camera component. Adds pillarbox or letterbox depending on the given aspect ratio and the aspect ratio of the game window.

-   GenericComponentPool.cs

    -   Generic MonoBehaviour for a pool of GameObjects with given type T component. Caches the `GetComponent<T>()` call that is usually used with pooled GameObjects when the object is created, and uses the cached component when the object is reused from the bool.
    -   Returns a wrapper object of type `ComponentWrapper<T>` containing
        -   `public T component`
        -   `public GameObject gameObject`
    -   Usage example:

        ```csharp
        BulletPool.cs
        ...
        public class BulletPool : GenericComponentPool<BulletController> {}
        ...

        BulletController.cs
        ...
        public class BulletController : MonoBehaviour {
            public Vector3 Velocity;
            public float Damage;

            private void OnCollisionEnter(Collision other) {
                gameObject.setActive(false);
                // Also deal damage etc
            }

            private void FixedUpdate() {
                transform.position += Velocity * Time.fixedDeltaTime;
            }
        }
        ...

        BulletManager.cs
        ...
        private BulletPool _bulletPool;
        void Awake() {
            _bulletPool = GetComponent<BulletPool>();
        }
        ...
        public void FireBullet(Vector3 origin, Vector3 direction, float velocity, float damage) {
            var pooledBulletWrapper = _bulletPool.GetPooledObject();

            var bulletObject = pooledBulletWrapper.gameObject;
            bulletObject.transform.position = origin;

            var bulletController = pooledBulletWrapper.component;
            bulletController.Damage = damage;
            bulletController.Velocity = direction.normalized * velocity;
        }
        ```

-   GenericManager.cs

    -   Abstract base class to be inherited from when creating singleton managers. If a singleton of the class already exists, new objects will be immediately destroyed when `InitializeSingleton()` is called. However, the Awake call will be finished by the MonoBehaviour

        -   Usage

            ```csharp
            public class GameManager : GenericManager<GameManager>
            {
                void Awake()
                {
                    if (!InitializeSingleton(this)) return;

                    // Your logic
                }
            }
            ```

*   ListExtensions.cs

    -   List shuffling using a Fisher-Yates shuffle
        -   `List<T>.Shuffle()` uses the `UnityEngine.Random` generator
        -   `List<T>.Shuffle(System.Random)` uses the given `System.Random` generator instance.

*   ModFix.cs

    -   `ModFix.Mod(int x, int m)`, returns the Euclidean definition of a modulo of the two values. C# native a % b is the remainder. Negative divider is not well-defined, but negative dividend works properly. Can be used for wrapping around a list where you jump multiple steps over the end/start of the list.

*   MonoBehaviourExtensions.cs

    -   Adds `this.Log(params object[])` to all MonoBehaviours. This allows a much easier way to `Debug.Log()` multiple values on the same line. Works much like `console.log()` in JavaScript by separating each parameter with a comma.

        -   Usage example:

        ```csharp
        // Normally you'd write
        var yeet = Random.Range(0f, 1f) > 0.5f;
        Debug.Log("Today is a " + (yeet ? "great" : "terrible") + " day!");
        // Now you can write
        this.Log("Today is a", yeet ? "great" : "terrible", "day!");

        // Works for any values, really (because object type has a .ToString())
        this.Log("test", 1, "5", Vector3.one);
        // >>> test, 1, 5, (1,0, 1,0, 1,0)
        ```

    -   Supports a custom delimiter by setting the static string `MonoBehaviourExtensions.MultiLogDelimiter`.

*   PoolableParticleSystem.cs

    -   MonoBehaviour for an easily pooled ParticleSystem. Use in conjuction with `GenericComponentPool<PoolableParticleSystem>`.
        -   The particle system must be configured to Run on Awake.
        -   Plays the particle system when activated, and returns itself to the pool after the particle system has stopped playing. Does not return the system to the pool if the ParticleSystem is set to loop, unless manually stopped. Clears the particle system upon inactivating.
        -   Can be used simply in a fire-and-forget fashion.

*   RandomAudioClip.cs

    -   MonoBehaviour for an easily pooled AudioSource. Use in conjunction with `GenericComponentPool<RandomAudioClip>`
        -   Plays a random audio clip from the given list. When finished, returns to the object pool.
        -   Can be used simply in a fire-and-forget fashion.

# License

See LICENSE

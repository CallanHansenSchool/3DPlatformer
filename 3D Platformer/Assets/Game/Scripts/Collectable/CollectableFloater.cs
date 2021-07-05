// Floater v0.0.2
// by Donovan Keith
//
// [MIT License](https://opensource.org/licenses/MIT)

// Slightly modified version

using UnityEngine;
using System.Collections;

// Makes objects float up & down while gently spinning.
public class CollectableFloater : MonoBehaviour
{
    // User Inputs
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    [SerializeField] private float minRandomTurnSpeed = 50;
    [SerializeField] private float maxRandomTurnSpeed = 75;

    [SerializeField] private float minRandomFrequency = 1;
    [SerializeField] private float maxRandomFrequency = 1.6f;

    private Collectable collectable;
    // Use this for initialization
    void Start()
    {
        collectable = GetComponent<Collectable>();

        degreesPerSecond = Random.Range(minRandomTurnSpeed, maxRandomTurnSpeed + 1);

        int _randomChoice = Random.Range(0, 2);

        if (_randomChoice == 0)
        {
            frequency = Random.Range(-minRandomFrequency, -maxRandomFrequency);          
        }
        else if(_randomChoice == 1)
        {
            frequency = Random.Range(minRandomFrequency, maxRandomFrequency);
        } else
        {
            Debug.LogError("Random choice chosen was out of range!");
        }
  
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        if (!collectable.PlayerIsNear)
        {
            // Float up/down with a Sin()
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;


            transform.position = tempPos;
        }
    }
}
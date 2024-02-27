using System.Collections.Generic;
using UnityEngine;

// Define a class named AIUtilityObject that extends MonoBehaviour
public class AIUtilityObject : MonoBehaviour
{
	// Define a serializable class for Effector parameters
	[System.Serializable]
	public class Effector
	{
		public AIUtilityNeed.Type type;  // Type of utility need affected by this effector
		[Range(-2, 2)] public float change;  // Change in the utility need value
	}

	// Parameters section
	[Header("Parameters")]
	[SerializeField] public Effector[] effectors;  // Array of effectors affecting utility needs
	[SerializeField, Tooltip("Time to use object")] public float duration;  // Duration of object usage
	[SerializeField, Tooltip("Animation to play when using")] public string animationName;  // Animation to play during usage

	[SerializeField] public Transform target;

	// UI section
	[Header("UI")]
	[SerializeField, Tooltip("Radius to detect agent")] float radius = 5;  // Radius for agent detection
	[SerializeField] LayerMask agentLayerMask;  // Layer mask for agents
	[SerializeField] AIUIMeter meterPrefab;  // Prefab for the UI meter
	[SerializeField] Vector3 meterOffset;  // Offset for the UI meter position

	public float score { get; set; }  // Score representing the utility of the object

	AIUIMeter meter;  // Reference to the UI meter
	Dictionary<AIUtilityNeed.Type, float> registry = new Dictionary<AIUtilityNeed.Type, float>();  // Dictionary to store effectors

	void Start()
	{
		// Create meter UI at run-time
		meter = Instantiate(meterPrefab, GameObject.Find("Canvas").transform);

		// Set meter properties
		meter.name = name;
		meter.text = name;
		meter.position = transform.position + meterOffset;

		// Set effectors array into dictionary for easy access
		foreach (var effector in effectors)
		{
			registry[effector.type] = effector.change;
		}
	}

    private void Update()
    {
        meter.visible = false; // hide meter by default

        // show object meter if near agent
        var colliders = Physics.OverlapSphere(transform.position, radius, agentLayerMask);
        if (colliders.Length > 0)
        {
            // check colliders for utility agent 
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out AIUtilityAgent agent))
                {
                    // set meter alpha based on distance to agent (fade-in)
                    float distance = 1 - Vector3.Distance(agent.transform.position, transform.position) / radius;
                    score = agent.GetUtilityScore(this);
                    meter.alpha = distance;
                    meter.visible = true;
                }
            }
        }
    }

    void LateUpdate()
	{
		// Update meter value and position in late update for smoother UI updates
		meter.value = score;
		meter.position = transform.position + meterOffset;
	}

	// Get the change in utility need value for a specific type
	public float GetNeedChange(AIUtilityNeed.Type type)
	{
		return registry.TryGetValue(type, out float value) ? value : 0f;
	}

	// Check if the object has an effector for a specific utility need type
	public bool HasNeedType(AIUtilityNeed.Type type)
	{
		return registry.ContainsKey(type);
	}
}
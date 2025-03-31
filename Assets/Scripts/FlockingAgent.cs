using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlockingAgent : MonoBehaviour
{
	[SerializeField] private float neighborDetectionRange = 4f;
	[SerializeField] private float maxSpeed = 10f;

    [SerializeField] private Vector3 velocity = Vector3.forward;
    public Vector3 Velocity { get => velocity; }

    private Vector3 desiredVelocity = Vector3.zero;

    private List<FlockingBehaviour> behaviours;
    public List<FlockingBehaviour> Behaviours
    {
        get => behaviours;
        set
        {
            behaviours ??= value;
        }
    }

    private Flock relatedFlock;
	public Flock RelatedFlock
	{
		get => relatedFlock;
		set
		{
			if (!relatedFlock) relatedFlock = value;
		}
    }

    private readonly List<FlockingAgent> neighbors = new();

    private void Awake()
	{
		var neighborDetectionCollider = gameObject.AddComponent<SphereCollider>();
		neighborDetectionCollider.isTrigger = true;
        neighborDetectionCollider.radius = neighborDetectionRange;
    }

	private void Update()
	{
		foreach (var behaviour in behaviours)
		{
            desiredVelocity += behaviour.Execute(neighbors, this) * (maxSpeed * behaviour.Weight);
        }

        var changeInDirection = (desiredVelocity - velocity) * Time.deltaTime;
        velocity += changeInDirection;

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        transform.position += velocity * Time.deltaTime;
		transform.forward = velocity;
	}

	private void LateUpdate()
	{
		desiredVelocity = Vector3.zero;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<FlockingAgent>(out var agent)) neighbors.Add(agent);
	}

	private void OnTriggerExit(Collider other)
	{
        if (other.TryGetComponent<FlockingAgent>(out var agent)) neighbors.Remove(agent);
    }

	public void Init(List<FlockingBehaviour> behaviours, Flock relatedFlock)
	{
		this.behaviours = behaviours;
		this.relatedFlock = relatedFlock;
	}
}

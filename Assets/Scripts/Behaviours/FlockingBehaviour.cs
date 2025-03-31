using System.Collections.Generic;
using UnityEngine;

public abstract class FlockingBehaviour : ScriptableObject
{
	public float Weight = 1f;

	public abstract Vector3 Execute(List<FlockingAgent> neighbors, FlockingAgent agent);
}

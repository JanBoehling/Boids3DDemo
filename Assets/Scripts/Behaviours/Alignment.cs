using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boids/Alignment")]
public class Alignment : FlockingBehaviour
{
	public override Vector3 Execute(List<FlockingAgent> neighbors, FlockingAgent agent)
	{
        if (neighbors.Count == 0) return Vector3.zero;

        var alignment = Vector3.zero;

        foreach (var neighbor in neighbors)
        {
            alignment += neighbor.Velocity;
        }

        alignment /= neighbors.Count;
        return alignment.normalized;
    }
}

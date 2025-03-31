using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boids/Separation")]
public class Separation : FlockingBehaviour
{
    public override Vector3 Execute(List<FlockingAgent> neighbors, FlockingAgent agent)
    {
        if (neighbors.Count == 0) return Vector3.zero;

        var direction = Vector3.zero;

        foreach (var neighbor in neighbors)
        {
            var directionToAgent = neighbor.transform.position - agent.transform.position;
            direction += directionToAgent / directionToAgent.sqrMagnitude;
        }

        direction /= neighbors.Count;
        return -direction.normalized;
    }
}

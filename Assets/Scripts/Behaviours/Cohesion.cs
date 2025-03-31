using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boids/Cohesion")]
public class Cohesion : FlockingBehaviour
{
    public override Vector3 Execute(List<FlockingAgent> neighbors, FlockingAgent agent)
    {
        if (neighbors.Count == 0) return Vector3.zero;

        var center = Vector3.zero;

        foreach (var neighbor in neighbors)
        {
            center += neighbor.transform.position;
        }

        center /= neighbors.Count;
        center -= agent.transform.position;

        return center.normalized;
    }
}

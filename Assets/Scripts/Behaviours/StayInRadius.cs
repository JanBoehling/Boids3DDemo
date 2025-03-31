using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boids/Stay in radius")]
public class StayInRadius : FlockingBehaviour
{
    [SerializeField] private Vector3 center = Vector3.zero;
    [SerializeField] private bool stayAroundFlockObject = true;
    [SerializeField] private float radius;

    public override Vector3 Execute(List<FlockingAgent> neighbors, FlockingAgent agent)
    {
        if (stayAroundFlockObject) center = agent.RelatedFlock.transform.position;

        var offset = center - agent.transform.position;
        float t = offset.magnitude / radius;

        if (t < .9f) return Vector3.zero;

        return offset * (t * t);
    }
}

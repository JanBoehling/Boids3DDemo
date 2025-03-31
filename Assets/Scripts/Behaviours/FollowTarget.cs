using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boids/FollowTarget")]
public class FollowTarget : FlockingBehaviour
{
    public override Vector3 Execute(List<FlockingAgent> neighbors, FlockingAgent agent)
    {
        return (FindObjectOfType<Flock>().transform.position - agent.transform.position).normalized;
    }
}

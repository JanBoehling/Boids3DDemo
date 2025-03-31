using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boids/FollowMouse")]
public class FollowMouse : FlockingBehaviour
{
    private Camera cam;

    public override Vector3 Execute(List<FlockingAgent> neighbors, FlockingAgent agent)
    {
        if (!cam) cam = Camera.main;

        return (agent.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
    }
}

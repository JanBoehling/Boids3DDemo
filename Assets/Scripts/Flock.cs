using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public List<FlockingAgent> members;

    [Space]
	[SerializeField] private FlockingAgent agentPrefab;
    [Space]
	[SerializeField] private float spawnRadius = 20f;
	[SerializeField] private uint agentCount = 10;
    [Space]
    [SerializeField] private List<FlockingBehaviour> behaviours;
    public List<FlockingBehaviour> Behaviours => behaviours;

	private void Start()
	{
        Spawn();
    }

	private void Spawn()
	{
        for (int i = 0; i < agentCount; i++)
        {
            var position = Random.onUnitSphere * spawnRadius + transform.position;
            var rotation = Random.rotation;

            var agent = Instantiate(agentPrefab, position, rotation, transform);
            agent.Init(behaviours, this);

            members.Add(agent);
        }
    }
}

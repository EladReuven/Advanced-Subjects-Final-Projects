using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{
    [SerializeField] public SOweapon Weapon;

    private void Start()
    {
        StartCoroutine(DelayedTeleport());
    }

    private IEnumerator DelayedTeleport()
    {
        yield return null; // Wait for one frame

        TeleportToClosestNavMeshPoint();
    }

    private void TeleportToClosestNavMeshPoint()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
    }
}

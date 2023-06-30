using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask _walkableLayer;
    [SerializeField] private Transform _movementDecal;

    private NavMeshAgent _agent => GetComponent<NavMeshAgent>();
    private Camera _camera => Camera.main;
    private Animator _anim => GetComponentInChildren<Animator>();

    private const string _anim_Walk = "Walk";

    private void Start()
    {
        // Teleport the player to the closest point on the NavMesh
        TeleportToClosestNavMeshPoint();
        _agent.enabled = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position to determine the destination
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _walkableLayer))
            {
                // Move the player towards the clicked position
                MoveToPosition(hit.point);

                // Move the movement decal to the clicked position
                MoveDecalToPosition(hit.point);
            }
        }

        // Check if the player is moving
        bool isMoving = _agent.velocity.magnitude > 0.1f;

        // Set the walking animation state
        _anim.SetBool(_anim_Walk, isMoving);
    }

    private void TeleportToClosestNavMeshPoint()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            transform.position = hit.position;

            // Move the movement decal to the player's position
            MoveDecalToPosition(hit.position);
        }
    }

    private void MoveToPosition(Vector3 destination)
    {
        _agent.SetDestination(destination);
    }

    private void MoveDecalToPosition(Vector3 position)
    {
        _movementDecal.position = position;
    }
}

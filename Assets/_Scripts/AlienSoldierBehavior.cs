using UnityEngine.AI;
using UnityEngine;

public class AlienSoldierBehavior : MonoBehaviour
{
    public NavMeshAgent alienSoldier;
    [SerializeField] Transform player;

    private void Start()
    {
        alienSoldier = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        alienSoldier.SetDestination(player.position);
    }
}

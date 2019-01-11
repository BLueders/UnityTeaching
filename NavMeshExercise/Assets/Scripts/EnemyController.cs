using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyController : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent agent;

    void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
    }

    void Update() {
        // Move to next Patrol Point using the NavMeshAgent
    }
}

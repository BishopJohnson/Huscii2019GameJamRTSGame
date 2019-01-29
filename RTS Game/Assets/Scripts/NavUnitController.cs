using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavUnitController : MonoBehaviour
{
    #region Fields

    public int player { get; private set; }

    private NavMeshAgent myAgent;

    #endregion

    #region Events

    public delegate void SelectUnitAction(GameObject unit, int player);

    public static event SelectUnitAction OnSelection;

    #endregion

    #region Methods

    public float EstimatedTimeToArrival()
    {
        //calculates unit's ETA to destination
        if (myAgent.hasPath)
        {
            return myAgent.remainingDistance / myAgent.speed;
        }

        return -1.0f;
    }

    public void Move(Vector3 destination)
    {
        myAgent.SetDestination(destination);
    }

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        player = 0; //TODO: ...
        myAgent = GetComponent<NavMeshAgent>();
    }

    private void OnMouseDown()
    {
        OnSelection(gameObject, player);
    }

    #endregion
}

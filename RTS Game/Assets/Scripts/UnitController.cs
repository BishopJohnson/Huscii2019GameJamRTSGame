using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(NavMeshAgent))]
public class UnitController : MonoBehaviour
{
    #region Fields

    /* The current target of the unit.
     */
    public GameObject target;

    /* The NavMeshAgent attached to the unit.
     */
    private NavMeshAgent myAgent;

    /* 
     */
    private Turrent myTurrent;

    /* 
     */
    private Unit myUnit;

    #endregion

    #region Events

    /* 
     * 
     * param: unit
     * param: player
     */
    //public delegate void SelectUnitAction(GameObject unit, int player);

    /* 
     */
    //public static event SelectUnitAction OnSelection;

    #endregion

    #region Methods
    
    private void EngageUnit(GameObject unit)
    {

    }
    
    /* Calculates the ETA of the unit to its current destination.
     * 
     * return: float - The ETA to the current destination.
     */
    public float EstimatedTimeToArrival()
    {
        //calculates unit's ETA to destination
        if (myAgent.hasPath)
        {
            return myAgent.remainingDistance / myAgent.speed;
        }

        return -1.0f;
    }

    /* Orders the unit to move to the given coordinate.
     * 
     * param: destination - The coordinate the unit will travel to.
     */
    public void Move(Vector3 destination)
    {
        myAgent.SetDestination(destination);
    }

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        //myTurrent = GetComponent<Turrent>();
        myUnit = GetComponent<Unit>();
    }

    /*
    private void OnMouseDown()
    {
        OnSelection(gameObject, player);
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        //checks if object is in the units layer
        if (obj.layer == gameObject.layer)
        {
            if (obj.GetComponent<Unit>().player != myUnit.player)
            {
                EngageUnit(obj);
            }
        }
    }

    #endregion
}

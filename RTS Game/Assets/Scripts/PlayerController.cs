using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Fields

    /* The player's number.
     */
    public int player; //{ get; private set; }

    /* The units currently selected by the player.
     */
    public GameObject[] units; //{ get; private set; }

    /* The unit layer.
     * 
     * TODO: change to a readonly field.
     */
    public LayerMask unitMask;

    /* The terrain layer.
     * 
     * TODO: change to a readonly field.
     */
    public LayerMask terrainMask;

    /* The player's camera.
     */
    private Camera myCamera;

    /* The number of units currently selected.
     */
    private int unitsSize;

    /* The default number of units allowed to be selected.
     */
    private readonly int DEFAULT_UNITS_LIMIT = 20;

    #endregion

    #region Methods

    /* Adds a unit to the player's selection.
     * 
     * param: unit - The unit being added to the list of selected units.
     * return: bool - Returns true if the unit was added, false otherwise.
     */
    private bool AddUnit(GameObject unit)
    {
        if (unit.GetComponent<UnitController>() == null)
        {
            return false;
        }

        if (unitsSize < units.Length)
        {
            for (int i = 0; i < unitsSize; i++)
            {
                if (unit == units[i])
                {
                    return false;
                }
            }

            units[unitsSize++] = unit;

            return true;
        }

        return false;
    }

    /* Removes the unit(s) from selection that are in the given indices.
     * 
     * param: start - The starting index (inclusive).
     * param: end - The ending index (exclusive).
     * return: GameObject[] - The unit(s) that were removed from the command list.
     */
    private GameObject[] RemoveUnit(int start, int end)
    {
        if (start >= 0 && start < end && end <= units.Length)
        {
            GameObject[] removedUnits = new GameObject[end - start];

            //transfers removed units to the new array
            for (int i = 0; i < removedUnits.Length; i++)
            {
                removedUnits[i] = units[i + start];
                units[i + start] = null;
                unitsSize--;
            }

            //removes null pocket in units array
            int j = start;
            for (int i = 0; i < unitsSize - start; i++)
            {
                units[j++] = units[i + end];
                units[i + end] = null;
            }

            return removedUnits;
        }

        return null;
    }

    /* 
     * 
     * param: unit
     * param: unitPlayer
     */
    private void SelectUnit(GameObject unit, int unitPlayer)
    {
        if (player == unitPlayer)
        {
            AddUnit(unit);
        }
    }

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        player = 0; //TODO: ...
        units = new GameObject[DEFAULT_UNITS_LIMIT];
        myCamera = GetComponent<Camera>();
        unitsSize = 0;
    }

    private void OnEnable()
    {
        //UnitController.OnSelection += SelectUnit;
    }

    private void OnDisable()
    {
        //NavUnitController.OnSelection -= SelectUnit;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //checks if raycast hit an object in the units layer mask to deselect units
            if (!Physics.Raycast(ray, out hit, 100, unitMask))
            {
                RemoveUnit(0, unitsSize);
            }
            else
            {
                GameObject unit = hit.collider.gameObject;

                if (unit.GetComponent<Unit>().player == player)
                {
                    AddUnit(unit);
                }
            }
        }

        //issues order to current units
        if (Input.GetButtonDown("Fire2"))
        {
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //checks if raycast hit a terrain object in the terrain layer mask
            if (Physics.Raycast(ray, out hit, 100, terrainMask))
            {
                //Debug info.
                //Debug.Log("Hit: " + hit.collider.name + ", Position: " + hit.point);

                foreach (GameObject unit in units)
                {
                    if (unit != null)
                    {
                        UnitController controller = unit.GetComponent<UnitController>();

                        /* Add additional orders for the player to issue.
                         */
                        controller.Move(hit.point);
                    }
                }
            }
        }
    }

    #endregion
}

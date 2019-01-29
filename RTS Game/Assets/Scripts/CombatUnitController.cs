using UnityEngine;

public class CombatUnitController : MonoBehaviour
{
    #region Fields

    public GameObject target;

    public Turrent turrent;

    //public float radius;

    //private CapsuleCollider triggerArea;

    #endregion
    
    #region Methods
    
    #endregion
    
    #region MonoBehaviour
    
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        //checks if other is in the units layer
        if (obj.layer == gameObject.layer)
        {
            
        }
    }

    #endregion
}

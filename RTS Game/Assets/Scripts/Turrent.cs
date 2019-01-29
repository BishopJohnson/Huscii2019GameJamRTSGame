using UnityEngine;

public class Turrent : MonoBehaviour
{
    #region Fields

    public GameObject target { get; set; }

    public GameObject muzzleTip;

    private bool tracking;

    #endregion

    #region Methods

    public void Fire()
    {
        
    }

    public void ToggleTracking()
    {
        tracking = !tracking;
    }

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        tracking = false;
    }

    private void Update()
    {
        
    }

    #endregion
}

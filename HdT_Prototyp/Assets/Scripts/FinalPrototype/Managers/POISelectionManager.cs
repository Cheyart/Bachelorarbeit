using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/** @class POISelectionManager coordinates the selection of the POIs in the scene.
 */
public class POISelectionManager : MonoBehaviour
{

    private POIHandler[] _POIs; /** List of all the POIs inside the virtual scene*/

    private SessionManager _sessionManager; /** Session Manager*/
    private POIMenuManager _POIMenuManager; /** POI Menu Manager*/
    private ModeManager _modeManager; /** Mode Manager */

    [SerializeField]
    private OffScreenPointer _offScreenPointer; /** Off-screen Pointer GUI element*/

    [SerializeField]
    private Color _idleColor; /** POI color when it is not selected*/

    [SerializeField]
    private Color _selectedColor; /** POI color when it is selected */


    void Awake()
    {
        SetManagers();
    }

    /** Sets the manager references
     */
    private void SetManagers()
    {
        _sessionManager = SessionManager.Instance;
        _POIMenuManager = _sessionManager.POIMenuManager;
        _modeManager = _sessionManager.ModeManager;
    }

    /** Finds all the POIs in the virtual scene and sets them up
     */
    public void SetupPOIs()
    {
        _POIs = FindObjectsOfType<POIHandler>(true);

        if (_modeManager == null)
        {
            SetManagers();
        }
        foreach (POIHandler poi in _POIs)
        {
            poi.Setup(this, _idleColor, _modeManager.ARCamera);
        }
    }

    /** Coordinates the selection of a POI
     * @param idOfSelectedPOI ID of the POI which was selected
     */
    public void SelectPOI(int idOfSelectedPOI)
    {
        if (IsActivePOI(idOfSelectedPOI))
        {
            return;
        }

        foreach (POIHandler poi in _POIs)
        {
            if (poi != null && poi.Content.Id == idOfSelectedPOI)
            {
                poi.SetColor(_selectedColor);
                poi.ShowBillboard(false);

                _POIMenuManager.OpenMenu(poi.Content);
                _sessionManager.ActivePOI = poi.Content;
                _modeManager.PoiIsSelected = true;

                _offScreenPointer.Target = poi.OffscreenTarget;
            }
            else
            {
                poi.SetColor(_idleColor);
                poi.ShowBillboard(true);
            }
        }
    }

    /** Deselects the currently selected POI
     */
    public void DeselectCurrentPOI()
    {
        foreach (POIHandler poi in _POIs)
        {
            if (IsActivePOI(poi.Content.Id))
            {
                poi.SetColor(_idleColor);
                poi.ShowBillboard(true);
            }
        }
        _sessionManager.ActivePOI = null;
        _offScreenPointer.Target = null;
        _modeManager.PoiIsSelected = false;

    }

    /** Checks if a specific POI is currently being selected
     * @param POIId ID of the POI for which the check will be conducted
     */
    private bool IsActivePOI(int POIId)
    {
        return (_sessionManager.ActivePOI != null && POIId == _sessionManager.ActivePOI.Id);
    }


}

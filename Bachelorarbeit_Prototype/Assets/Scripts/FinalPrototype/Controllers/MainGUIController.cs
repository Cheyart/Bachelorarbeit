using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class MainGUIController controls the activation/deactivation of the overall GUI
 */
public class MainGUIController : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _topBar; /** Menu Top Bar */

    [SerializeField]
    private GameObject _poiMenu; /** POI menu */

    [SerializeField]
    private GameObject _bottomBar; /** Menu Bottom bar */

    public float TopBarHeight { get; private set; } /** Top Bar height*/

    public float BottomBarHeight { get; private set; } /** Bottom Bar height*/


    private void Start()
    {
        _topBar.SetActive(false);
        _poiMenu.SetActive(false);
        _bottomBar.SetActive(false);

        TopBarHeight = _topBar.GetComponent<RectTransform>().rect.height;
        BottomBarHeight = _bottomBar.GetComponent<RectTransform>().rect.height;
    }

    /** Shows the main GUI elements
     */
    public void ShowMainGUI()
    {
        _topBar.SetActive(true);
        _bottomBar.SetActive(true);
        _poiMenu.SetActive(true);
    }

}

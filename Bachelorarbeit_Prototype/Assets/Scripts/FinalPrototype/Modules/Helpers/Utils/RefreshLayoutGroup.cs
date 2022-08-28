using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** @class RefreshLayoutGroup This class refreshes the LayoutGroup at the end of a frame (if needed)
 */
[RequireComponent(typeof(LayoutGroup), typeof(RectTransform))]
public class RefreshLayoutGroup : MonoBehaviour
{
    private LayoutGroup _layoutGroup; /** LayoutGroup Component which will be refreshed*/

    private bool _needsRefresh; /** value which indicates if the layout group needs to be refreshed for the current frame*/
    public bool NeedsRefresh {
        get { return _needsRefresh; }
        set {
            _needsRefresh = value;

            if (_needsRefresh)
            {
                StartCoroutine(Refresh());
                _needsRefresh = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _layoutGroup = GetComponent<LayoutGroup>();
    }

    /**
     * Refreshes the layout group at the end of the frame*/
    IEnumerator Refresh()
    {
         _layoutGroup.enabled = false;
         yield return new WaitForEndOfFrame();
         _layoutGroup.enabled = true;
  
    }

 
}

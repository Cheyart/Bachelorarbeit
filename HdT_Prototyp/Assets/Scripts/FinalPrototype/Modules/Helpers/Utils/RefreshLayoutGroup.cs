using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** @class RefreshLayoutGroup This class refreshes the LayoutGroup at the end of a frame (if needed)
 */
[RequireComponent(typeof(LayoutGroup), typeof(RectTransform))]
public class RefreshLayoutGroup : MonoBehaviour
{
    private LayoutGroup _layoutGroup;

    private bool _needsRefresh;
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

    IEnumerator Refresh()
    {
         _layoutGroup.enabled = false;
         yield return new WaitForEndOfFrame();
         _layoutGroup.enabled = true;
  
    }

 
}

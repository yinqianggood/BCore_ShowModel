using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrameWork;
public class Begin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.ShowUI(ProConst.MainModelUI);
    }

}

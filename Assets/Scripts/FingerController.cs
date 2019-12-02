using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum FingerIE
{
    zero,
    OneFinger,
    TwoFinger,
}
public class RotateModel : MonoBehaviour
{

    public Vector3 initialRot;
    public Vector3 initialSca;
    public static RotateModel instance;
    IEnumerator ie;
    FingerIE finger_num = FingerIE.zero;
    void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount == 0)
        {
            Debug.Log("== 0");
            if (finger_num != FingerIE.zero)
            {
                StopCoroutine(ie);
                ie = null;
                finger_num = FingerIE.zero;
            }
        }
        else if (Input.touchCount == 1)
        {
            Debug.Log("== 1");
            if (finger_num != FingerIE.OneFinger)
            {
                if (ie != null)
                {
                    StopCoroutine(ie);
                }
                ie = IMonitorMouseOneFinger();
                StartCoroutine(ie);
                finger_num = FingerIE.OneFinger;
            }
        }
        else if (Input.touchCount == 2)
        {
            Debug.Log("== 2");
            if (finger_num != FingerIE.TwoFinger)
            {
                if (ie != null)
                {
                    StopCoroutine(ie);
                }
                ie = IIMonitorMouseTwoFinger();
                StartCoroutine(ie);
                finger_num = FingerIE.TwoFinger;
            }
        }

    }
    /// <summary>
    /// 一根手指控制转动
    /// </summary>
    /// <returns></returns>
    IEnumerator IMonitorMouseOneFinger()
    {

        Touch oneFingerTouch;
        while (true)
        {
            oneFingerTouch = Input.GetTouch(0);
            if (oneFingerTouch.phase == TouchPhase.Moved)
            {
                Vector2 deltaPos = oneFingerTouch.deltaPosition;
                transform.Rotate(-Vector3.forward * deltaPos.x * 0.2f, Space.World);
                transform.Rotate(-Vector3.left * deltaPos.y * 0.2f, Space.World);
            }
            yield return 0;
        }
    }
    /// <summary>
    /// 两个手指控制缩放
    /// </summary>
    /// <returns></returns>
    IEnumerator IIMonitorMouseTwoFinger()
    {
        Touch firstOldTouch;
        Touch secondOldTouch;
        Touch firstNewTouch;
        Touch secondNewTouch;
        float oldDistance;
        float newDistance;
        while (true)
        {
            firstOldTouch = Input.GetTouch(0);
            secondOldTouch = Input.GetTouch(1);
            oldDistance = Vector2.Distance(firstOldTouch.position, secondOldTouch.position);
            yield return 0;
            firstNewTouch = Input.GetTouch(0);
            secondNewTouch = Input.GetTouch(1);
            newDistance = Vector2.Distance(firstNewTouch.position, secondNewTouch.position);
            if (oldDistance > newDistance && this.transform.localScale.x > 0.25f)
            {
                this.transform.localScale -= Vector3.one * 0.1f;
            }
            else if (oldDistance < newDistance && this.transform.localScale.x < 2f)
            {
                this.transform.localScale += Vector3.one * 0.1f;
            }
        }
    }
    /// <summary>
    /// 复位
    /// </summary>
    public void ResetRot()
    {
        this.transform.localEulerAngles = initialRot;
        this.transform.localScale = initialSca;
    }
}
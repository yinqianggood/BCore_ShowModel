
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFrameWork;
public class MainModelUI : BaseUI
{
    public RawImage modelImg;
    public Slider floorSlider;
    public Text txtFloorCount;
    public Button btnShowCloths;
    public Button btnShowFloorCount;
    public Button btnCreateBuild;
    public GameObject buildStyleParent;
    public GameObject buildStyleItem;
    public Transform modelParent;
    public GameObject[] prefabs;
    public float modelPadding = 12f;
    private int mPrefabCount;
    private int minFloor = 4;




    void Awake()
    {
        //定义窗口性质 (默认数值)
        currentUIType.type = UIFormType.Normal;
        currentUIType.mode = UIFormShowMode.Normal;
        currentUIType.lucenyType = UIFormLucenyType.Lucency;

        //注册按钮
        //RigisterBtnOnClick("btnLogin", go =>
        //{
        //    OpenUI(ProConst.SelectUI);
        //});
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //点击创建
    public void OnBtnBuildClick()
    {
        if (mPrefabCount <= 0) return;
        modelParent.gameObject.GetComponent<Drag>().enabled=false;
        ClearChild(modelParent);
        StartCoroutine(DelayBuild(prefabs[0]));

    }

    IEnumerator DelayBuild(GameObject go)
    {
        for (int i = 0; i < mPrefabCount; i++)
        {
            GameObject itemObj = Instantiate(go) as GameObject;
            itemObj.transform.parent = modelParent;
            itemObj.transform.localScale = Vector3.one;
            itemObj.transform.localPosition = new Vector3(0, modelPadding * i, 0);
            itemObj.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        modelParent.gameObject.GetComponent<Drag>().enabled = true;
    }
    private void ClearChild(Transform parent)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Transform t = parent.GetChild(i);
            Destroy(t.gameObject);
        }
        modelParent.transform.localEulerAngles = Vector3.zero;
    }

    public void OnBtnResetClick()
    {
        ClearChild(modelParent);
       // btnsParent.gameObject.SetActive(false);
    }
    //选择层数
    public void OnSliderValueChange(float value)
    {
        txtFloorCount.text = "层数:" + value * minFloor;
        mPrefabCount = (int)value;
    }

    //放大缩小
    public void OnBtnZoomClick(bool isIn)
    {
        if (isIn)
        {
            if (modelParent.localScale.x >= 0.2f) return;
            modelParent.localScale = modelParent.localScale * 1.1f;
        }
        else
        {
            if (modelParent.localScale.x < 0.05f) return;
            modelParent.localScale = modelParent.localScale * 0.9f;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFrameWork;
using WJExcelDataClass;
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
    public GameObject mItemStylePrefab;
    public Transform mItemStyleParent;
    public Text txt_ModelName;
    public GameObject go2DView;
    public GameObject goModelDesc;
    public Text txtNameTitle;
    public Text txtModelDesc;
    public GameObject goPriceDesc;
    public Text txtPriceDesc;
    public GameObject goClothView;
    public GameObject txtClothDesc;







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
        InitData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitData()
    {
        DataManager DM = new DataManager();
        DM.LoadAll();
        Dictionary<int, ModelItem> dic = DM.p_Model.Dict;
        List<MyItemModel> mItemModelList = new List<MyItemModel>();
        foreach (var item in dic)
        {
            ModelItem mi = item.Value;
            MyItemModel myItemModel = new MyItemModel(mi, mItemStylePrefab, mItemStyleParent);
            myItemModel.callBack = OnBuildStyleClick;
            mItemModelList.Add(myItemModel);
        }
       
    }
    private ModelItem mModelData;//当前旋转的模型数据.
    //点击了楼型选择.
    private void OnBuildStyleClick(ModelItem item)
    {
        mModelData = item;
        txt_ModelName.text = item.modelName;
    }
    //点击了模型名称.
    public void OnBtnModelNameClick()
    {
        goModelDesc.SetActive(true);
        txtNameTitle.text = string.IsNullOrEmpty(mModelData.modelName) ? " ": mModelData.modelName;
    }
    //点击了2D平面展示
    public void OnBtn2DClick()
    {
        go2DView.SetActive(true);

    }
    public void OnBtnClothClick()
    {
        goClothView.SetActive(true);
    }
    public void OnBtnPriceClick()
    {
        goPriceDesc.SetActive(true);
    }


    //点击创建
    public void OnBtnBuildClick()
    {
        if (mPrefabCount <= 0) return;
        modelParent.transform.parent.gameObject.GetComponent<Drag>().enabled=false;
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
        modelParent.transform.parent.gameObject.GetComponent<Drag>().enabled = true;
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
    public void OnBtnMoreClick()
    {
        Application.OpenURL("http://www.broad.com/");
    }
}

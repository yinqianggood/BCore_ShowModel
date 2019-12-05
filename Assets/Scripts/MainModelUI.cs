
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFrameWork;
using WJExcelDataClass;
using System;

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
    public Text txtPrice;







    void Awake()
    {
        //���崰������ (Ĭ����ֵ)
        currentUIType.type = UIFormType.Normal;
        currentUIType.mode = UIFormShowMode.Normal;
        currentUIType.lucenyType = UIFormLucenyType.Lucency;

        //ע�ᰴť
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
        mModelData = mItemModelList[0].ItemData;
    }
    private ModelItem mModelData;//��ǰ��ת��ģ������.
    //�����¥��ѡ��.
    private void OnBuildStyleClick(ModelItem item)
    {
        modelParent.transform.parent.gameObject.GetComponent<Drag>().enabled = false;
        ClearChild(modelParent);
        mModelData = item;
        txt_ModelName.text = item.modelName;
    }
    //�����ģ������.
    public void OnBtnModelNameClick()
    {
        goModelDesc.SetActive(true);
        txtNameTitle.text = string.IsNullOrEmpty(mModelData.modelName) ? " ": mModelData.modelName;
    }
    //�����2Dƽ��չʾ
    public void OnBtn2DClick()
    {
        go2DView.SetActive(true);

    }
    public void OnBtnClothClick()
    {
        goClothView.SetActive(true);
    }
    private void GetPrice()
    {
        float sum = mModelData.price1 + mModelData.price2 + mModelData.price3 + mModelData.price4 + mModelData.price5 + mModelData.price6 + mModelData.price7 + mModelData.price8;
        float totalSum = sum * mModelData.area * totalFloor;
        Math.Round(Convert.ToDecimal(45.367), 2, MidpointRounding.AwayFromZero);
        if (totalSum > 100000000f)
        {
            strSumPrice = Math.Round(Convert.ToDecimal(totalSum / 100000000f), 2, MidpointRounding.AwayFromZero) + "��";
        }
        else if (totalSum > 10000000f)
        {
            strSumPrice = Math.Round(Convert.ToDecimal(totalSum / 10000000f), 2, MidpointRounding.AwayFromZero) + "ǧ��";
        }
        else if (totalSum > 1000000f)
        {
            strSumPrice = Math.Round(Convert.ToDecimal(totalSum / 1000000f), 2, MidpointRounding.AwayFromZero) + "����";
        }
    }
    private string strSumPrice;
    public void OnBtnPriceClick()
    {
        goPriceDesc.SetActive(true);
        

        string str = "�����ҹ���                                                                 " + mModelData.price1.ToString() + "\n"
                    + "����ṹ����                                                             " + mModelData.price2.ToString() + "\n"
                    + "Χ���ṹ                                                                      " + mModelData.price3.ToString() + "\n"
                    + "װ��װ��                                                                      " + mModelData.price4.ToString() + "\n"
                    + "���簲װ����                                                             " + mModelData.price5.ToString() + "\n"
                    + "ůͨ�ŷ�                                                                      " + mModelData.price6.ToString() + "\n"
                    + "���⹤��                                                                      " + mModelData.price7.ToString() + "\n"
                    + "����Ԥ�Ʒ�                                                                  " + mModelData.price8.ToString() + "\n" + "\n" 
                    + "�ܼ�                                                                              " + strSumPrice;
        txtPriceDesc.text = str;
    }


    //�������
    public void OnBtnBuildClick()
    {
        if (mPrefabCount <= 0) return;
        modelParent.transform.parent.gameObject.GetComponent<Drag>().enabled=false;
        ClearChild(modelParent);
        string modelName = mModelData.modelName;
        StartCoroutine(DelayBuild(GetModelPrefab()));

    }
    private GameObject GetModelPrefab()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            if(prefabs[i].name==mModelData.modelName)
            {
                return prefabs[i];
            }
        }
        return prefabs[0];
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
        GetPrice();
        txtPrice.text = strSumPrice;
    }
    private void ClearChild(Transform parent)
    {
        modelParent.transform.parent.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Transform t = parent.GetChild(i);
            Destroy(t.gameObject);
        }
       
    }

    public void OnBtnResetClick()
    {
        ClearChild(modelParent);
        // btnsParent.gameObject.SetActive(false);
       
    }
    private int totalFloor = 0;
    //ѡ�����
    public void OnSliderValueChange(float value)
    {
        txtFloorCount.text = "����:" + value * minFloor;
        mPrefabCount = (int)value;
        totalFloor= (int)value * minFloor;
    }

    //�Ŵ���С
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

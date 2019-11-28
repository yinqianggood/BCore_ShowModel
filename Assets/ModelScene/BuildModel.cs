using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using WJExcelDataClass;

public class BuildModel : MonoBehaviour
{
    public Camera mCam;
    public Transform modelParent;
    public List<GameObject> prefabs;
    public List<GameObject> ClothPrefabs;
    public Button btnBuild;
    public Button btnReset;
    public Slider slider;
    public int floorBaseCount;
    public Text txtFloorCout;
    public int minFloor = 4;
    public int modelPadding=12;
    private int mPrefabCount = 0;
    public GameObject btnsParent;
    


    
    // Start is called before the first frame update
    void Start()
    {
        DataManager d = new DataManager();
        d.LoadAll();
        ModelItem mi= d.GetModelItemByID(1);
        Debug.Log("打印出来："+mi.modelName);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //点击创建
    public void OnBtnBuildClick()
    {
        if (mPrefabCount <= 0) return;
        btnsParent.gameObject.SetActive(false);
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
            itemObj.transform.localPosition = new Vector3(0 ,modelPadding * i,0);
            itemObj.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        btnsParent.gameObject.SetActive(true);
    }
    private void ClearChild(Transform parent)
    {
        for (int i = parent.childCount-1; i >=0; i--)
        {
            Transform t = parent.GetChild(i);
            Destroy(t.gameObject);
        }
        modelParent.transform.localEulerAngles = Vector3.zero;
    }

    public void OnBtnResetClick()
    {
        ClearChild(modelParent);
        btnsParent.gameObject.SetActive(false);
    }
    //选择层数
    public void OnSliderValueChange(float value)
    {
        txtFloorCout.text = "层数：" + value * minFloor;
        mPrefabCount = (int)value;
    }

    //放大缩小
    public void OnBtnZoomClick(bool isIn)
    {
        if(isIn)
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

    public void OnRotateModel(bool isLeft)
    {
        int tempAngel = isLeft ? 30 : -30;
        float rotationY = modelParent.localEulerAngles.y + tempAngel;
        modelParent.DORotate(new Vector3(modelParent.localEulerAngles.x, rotationY, modelParent.localEulerAngles.z), 0.5f, RotateMode.FastBeyond360);
      
    }
    public void OnRotateModelX(bool isUp)
    {
        int tempAngel = isUp ? -30 : 30;
        float rotationX = modelParent.localEulerAngles.x + tempAngel;
        modelParent.DORotate(new Vector3(rotationX, modelParent.localEulerAngles.y, modelParent.localEulerAngles.z), 0.5f,RotateMode.FastBeyond360);

    }

    public void OnClothingClick()
    {
        StartCoroutine(DelayBuild(ClothPrefabs[0]));

    }

}

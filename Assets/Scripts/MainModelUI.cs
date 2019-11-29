
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
    public GameObject buildStyleParent;
    public GameObject buildStyleItem;
    
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
}

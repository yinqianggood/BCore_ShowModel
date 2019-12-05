using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WJExcelDataClass;
using UnityEngine.UI;
using UIFrameWork;
using System;

//楼型列表Item单元格。
public class MyItemModel 
{
    
    public ModelItem ItemData { get; set; }
    public GameObject go;
    public Action<ModelItem> callBack;
    

    public MyItemModel(ModelItem data,GameObject item=null,Transform parent=null)
    {
        ItemData = data;
        if(item)
        {
            go = (GameObject)UnityEngine.GameObject.Instantiate(item,parent);
            go.SetActive(true);
            if(parent)
            {
                go.transform.SetParent(parent);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;
            }
            go.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load("UI/"+ItemData.icon2D, typeof(Sprite)) as Sprite;//ItemData.icon2D.
            Button btn= go.GetComponent<Button>();
            // EventTriggerListener.Get(go).onClick += OnBtnClick;
            go.GetComponent<Button>().onClick.AddListener(delegate() {
                callBack(ItemData);
            });
        }
      
    }
   
   

}

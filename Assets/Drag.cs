using UnityEngine;

public class Drag : MonoBehaviour
{
    Vector3 scale;
    float offset = 0.2f;
    float maxSize = 2.0f;
    float minSize = 0.4f;
    public float speed = 200f;
    // Use this for initialization
    void Start()
    {
        scale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //鼠标滚轮的效果
        //Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (scale.x <= maxSize)
            {
                scale.x += offset;
                scale.y += offset;
                scale.z += offset;
                this.transform.localScale = scale;
            }

        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (scale.x > minSize)
            {
                scale.x -= offset;
                scale.y -= offset;
                scale.z -= offset;
                this.transform.localScale = scale;
            }
        }
        //鼠标左键旋转物体
        if (Input.GetMouseButton(0))
        {
           // float axis = Input.GetAxis("Mouse X");
           // this.transform.Rotate(Vector3.up * Time.deltaTime * speed * axis);
            float h = Time.deltaTime * speed * Input.GetAxis("Mouse X");
            float v = Time.deltaTime * speed * Input.GetAxis("Mouse Y");

            transform.Rotate(v, -h, 0);
        }
    }
}

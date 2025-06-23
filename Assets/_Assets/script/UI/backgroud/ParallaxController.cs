using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{




    //Material mat;
    //float distance;
    //[Range(0f, 1f)]
    //public float speed;
    //private void Start()
    //{
    //    mat = GetComponent<Renderer>().material;
    //}
    //private void Update()
    //{
    //    distance += Time.deltaTime * speed;

    //    Debug.Log($"{distance}");
    //    mat.SetTextureOffset("_MainTex", Vector2.right * distance);
    //}

    //// Lưu transform của camera chính
    //Transform cam;

    //// Lưu vị trí bắt đầu của camera
    //Vector3 camStart;

    //// Khoảng cách camera đã di chuyển theo trục X và Y
    //float distance;
    //float distance_y;

    //// Mảng lưu các Material của các lớp nền (background)
    //Material[] mat;

    //// Mảng lưu tốc độ cuộn của từng lớp nền
    //float[] backspeed;

    //// Mảng lưu các GameObject nền (background layer)
    //GameObject[] backgroud;

    //// Khoảng cách xa nhất giữa các lớp nền với camera (để chuẩn hóa tốc độ cuộn)
    //float farthestBack;

    //// Tốc độ cuộn parallax được điều chỉnh trong Editor (có thể dùng thanh slider)
    //[Range(0.01f, 0.05f)]
    //public float parallaxspeed;

    //void Start()
    //{
    //    // Lấy transform của Camera chính trong scene
    //    cam = Camera.main.transform;

    //    // Lưu lại vị trí ban đầu của camera để tính khoảng cách dịch chuyển
    //    camStart = cam.position;

    //    // Lấy số lượng lớp nền (số con của object gốc)
    //    int backcount = transform.childCount;

    //    // Khởi tạo mảng material và tốc độ tương ứng với số lớp nền
    //    mat = new Material[backcount];
    //    backspeed = new float[backcount];

    //    // Khởi tạo mảng GameObject cho các lớp nền
    //    backgroud = new GameObject[backcount];

    //    // Lặp qua tất cả các lớp nền và lưu lại object + material
    //    for (int i = 0; i < backcount; i++)
    //    {
    //        backgroud[i] = transform.GetChild(i).gameObject; // lấy object lớp nền thứ i
    //        mat[i] = backgroud[i].GetComponent<Renderer>().material; // lấy chất liệu (material) của nó
    //    }

    //    // Tính toán tốc độ cuộn tương ứng với từng lớp nền
    //    CalculateBackgroundSpeed(backcount);
    //}

    //void CalculateBackgroundSpeed(int backcount)
    //{
    //    // Tìm khoảng cách lớn nhất từ camera đến các lớp nền (theo trục Z)
    //    for (int i = 0; i < backcount; i++)
    //    {
    //        float zDistance = backgroud[i].transform.position.z - cam.position.z;
    //        if (zDistance > farthestBack)
    //        {
    //            farthestBack = zDistance;
    //        }
    //    }

    //    // Dựa vào khoảng cách Z để tính tốc độ cuộn (xa thì cuộn chậm hơn)
    //    for (int i = 0; i < backcount; i++)
    //    {
    //        float zDistance = backgroud[i].transform.position.z - cam.position.z;
    //        backspeed[i] = 1 - (zDistance / farthestBack); // chuẩn hóa từ 0 đến 1
    //    }
    //}

    //private void LateUpdate()
    //{
    //    // Tính khoảng cách camera đã dịch chuyển theo trục X
    //    distance = cam.position.x - camStart.x;

    //    // Nếu muốn dùng cả trục Y (chưa dùng): distance_y = cam.position.y - camStart.y;

    //    // Di chuyển object chứa tất cả các lớp nền theo camera (đảm bảo hiệu ứng mượt mà)
    //    transform.position = new Vector3(cam.position.x, cam.position.y, 2);

    //    // Áp dụng hiệu ứng cuộn cho từng lớp nền dựa vào tốc độ riêng của nó
    //    for (int i = 0; i < backgroud.Length; i++)
    //    {
    //        // Tốc độ lớp nền = tốc độ riêng * hệ số parallax toàn cục
    //        float speed = backspeed[i] * parallaxspeed;

    //        // Cuộn texture theo trục X bằng cách điều chỉnh offset của material
    //        mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
    //    }
    //}

    //// Material của nền (được tự động lấy từ Renderer)
    //private Material mat;

    //// Vị trí ban đầu của camera
    //private Vector3 camStart;

    //// Camera chính
    //private Transform cam;

    //// Khoảng cách dịch chuyển
    //private float distance;
    //private float distance_y;

    //// Tốc độ parallax chỉnh trong editor
    //[Range(0.01f, 0.1f)]
    //public float parallaxspeed = 0.03f;

    //void Start()
    //{
    //    // Lưu vị trí ban đầu của camera
    //    cam = Camera.main.transform;
    //    camStart = cam.position;

    //    // Lấy Material từ chính object này (bắt buộc phải có Renderer)
    //    mat = GetComponent<Renderer>().material;
    //}

    //void LateUpdate()
    //{
    //    // Tính khoảng cách camera đã di chuyển theo trục X
    //    distance = cam.position.x - camStart.x;
    //    distance_y = cam.position.y - camStart.y;

    //    // Cuộn texture theo trục X
    //    mat.SetTextureOffset("_MainTex", new Vector2(distance * parallaxspeed, distance_y * (parallaxspeed / 3)));

    //    // Di chuyển nền theo camera (giữ khoảng cách z cố định)
    //    transform.position = new Vector3(cam.position.x, cam.position.y, transform.position.z);

    //}
    private Material mat;
    private Vector3 camStart;
    private Vector2 currentOffset = Vector2.zero;

    private Transform virtualCam;
    [Range(0.01f, 0.1f)]
    public float parallaxspeed = 0.03f;

    void Start()
    {

        virtualCam = Camera.main.transform; 
        camStart = virtualCam.position;
        mat = GetComponent<Renderer>().material;
    }

    void LateUpdate()
    {
        // Tính offset camera
        float dx = virtualCam.position.x - camStart.x;
        float dy = virtualCam.position.y - camStart.y;

        // Tính offset mới
        Vector2 targetOffset = new Vector2(dx * parallaxspeed, dy * (parallaxspeed / 3f));

        // Làm mượt offset
        currentOffset = Vector2.Lerp(currentOffset, targetOffset, Time.deltaTime * 5f);

        // Gán offset vào texture
        mat.SetTextureOffset("_MainTex", currentOffset);

        // Nền đi theo camera (không áp dụng parallax vào position, chỉ offset)
        transform.position = new Vector3(virtualCam.position.x, virtualCam.position.y, transform.position.z);
    }
}



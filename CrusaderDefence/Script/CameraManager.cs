using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public float sensitivityX;
    public float sensitivityY;
    public float moveSensitivity;

    public GameObject mainCam;

    float mHdg = 0F;
    float mPitch = 0F;

    public GUITexture gt;
    //private bool wasLocked = false;

    //마우스 포인터로 사용할 텍스처를 입력받습니다.
    public Texture2D cursorTexture;
    //텍스처의 중심을 마우스 좌표로 할 것인지 체크박스로 입력받습니다.
    public bool hotSpotIsCenter = false;
    //텍스처의 어느부분을 마우스의 좌표로 할 것인지 텍스처의 

    //좌표를 입력받습니다.
    public Vector2 adjustHotSpot = Vector2.zero;
    //내부에서 사용할 필드를 선업합니다.
    private Vector2 hotSpot;
    
    void Start()
    {
        gt = GetComponent<GUITexture>();
        //StartCoroutine("MyCursor");
        Cursor.visible = true;
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        float deltaX = Input.GetAxis("Mouse X") * sensitivityX;
        float deltaY = Input.GetAxis("Mouse Y") * sensitivityY;

        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
        //    Strafe(deltaX);
        //    ChangeHeight(deltaY);
        }
        else
        {
            if (Input.GetMouseButton(1))
            {
                ChangeHeading(deltaX);
                ChangePitch(-deltaY);
            }
            else
            {
                if (Input.mousePosition.x < 10)
                    transform.Translate(new Vector3(-moveSensitivity * Time.deltaTime, 0, 0));
                else if (Input.mousePosition.x > Screen.width - 10)
                    transform.Translate(new Vector3(moveSensitivity * Time.deltaTime, 0, 0));
                else if (Input.mousePosition.y < 10)
                    transform.Translate(new Vector3(0, 0, -moveSensitivity * Time.deltaTime));
                else if (Input.mousePosition.y > Screen.height - 10)
                    transform.Translate(new Vector3(0, 0, moveSensitivity * Time.deltaTime));
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            mainCam.transform.Translate(Vector3.forward);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            mainCam.transform.Translate(-Vector3.forward);
        }
    }

    void Strafe(float aVal)
    {
        transform.position += aVal * transform.right;
    }

    void ChangeHeight(float aVal)
    {
        transform.position += aVal * Vector3.up;
    }

    void ChangeHeading(float aVal)
    {
        mHdg += aVal;
        WrapAngle(ref mHdg);
        mainCam.transform.localEulerAngles = new Vector3(mPitch, mHdg, 0);
    }

    void ChangePitch(float aVal)
    {
        mPitch += aVal;
        WrapAngle(ref mPitch);
        mainCam.transform.localEulerAngles = new Vector3(mPitch, mHdg, 0);
    }

    public static void WrapAngle(ref float angle)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
    }

    IEnumerator MyCursor()
    {
        //모든 렌더링이 완료될 때까지 대기할테니 렌더링이 완료되면 
        //깨워 달라고 유니티 엔진에 게 부탁하고 대기합니다.
        yield return new WaitForEndOfFrame();

        //텍스처의 중심을 마우스의 좌표로 사용하는 경우 
        //텍스처의 폭과 높이의 1/2을 hot Spot 좌표로 입력합니다.
        if (hotSpotIsCenter)
        {
            hotSpot.x = cursorTexture.width / 2;
            hotSpot.y = cursorTexture.height / 2;
        }
        else
        {
            //중심을 사용하지 않을 경우 Adjust Hot Spot으로 입력 받은 
            //것을 사용합니다.
            hotSpot = adjustHotSpot;
        }
        //이제 새로운 마우스 커서를 화면에 표시합니다.
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }
}

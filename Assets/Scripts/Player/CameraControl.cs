using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour {
    [Header("Чувствительность")]
    public float sensitivityX = 10;
    public float sensitivityY = 10;

    [Header("Ограничение вращения")]
    public float angleCameraMinX = -90;
    public float angleCameraMaxX = 90;
    public float angleCameraMinY = -360;
    public float angleCameraMaxY = 360;

    [Header("Вращение вокруг точки и дистанция")]
    public bool rotationAroundPoint = false;
    private float distance = 0;
    private Transform cameraObj;
    public float sensitivityDistance = 1;
    public float distanceMin = 0;
    public float distanceMax = 10;

    public float height;
    [HideInInspector] public bool sitDown = false;

    public float rotationX = 0;
    public float rotationY = 0;

    [SerializeField] private bool hidenCursorActive = false;
    [SerializeField] private bool hidenCursorOnStart = false;
    
    public void Start() {
        rotationX = -transform.localEulerAngles.x;
        if (!ignoreRotationY) {
            rotationY = transform.localEulerAngles.y;
        }

        if (rotationAroundPoint) {
            cameraObj = transform.GetChild(0);
            distance = -cameraObj.localPosition.z;
        }
        HideCursor(hidenCursorOnStart);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            HideCursor(Cursor.visible);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            HideCursor(false);
        }

        if (!Cursor.visible) {
            RotationCamera();
        }

        if (rotationAroundPoint) {
            if (Input.GetMouseButton(1)) {
                RotationCamera();
            }
            if (!EventSystem.current.IsPointerOverGameObject() && rotationAroundPoint) {
                DistanceCamera();
            }
        }


        if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            if (sitDown) {
                transform.position += Vector3.up * height;
                sitDown = false;
            } else {
                transform.position -= Vector3.up * height;
                sitDown = true;
            }
        }
    }

    //Скрыть/Показать курсор
    public void HideCursor(bool status) {
        if (hidenCursorActive) {
            Cursor.visible = !status;
            if (status) {
                Cursor.lockState = CursorLockMode.Locked;
            } else {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    private void OnDisable() {
        HideCursor(false);
    }

    [HideInInspector]
    public bool ignoreRotationY = false;

    //Вращение камеры
    private void RotationCamera() {
        rotationX += Input.GetAxis("Mouse Y") * sensitivityX;
        rotationX %= 360;
        if (rotationX <= angleCameraMinX)
            rotationX = angleCameraMinX;
        if (rotationX >= angleCameraMaxX)
            rotationX = angleCameraMaxX;


        if (!ignoreRotationY) {
            rotationY += Input.GetAxis("Mouse X") * sensitivityY;
            rotationY %= 360;
            if (rotationY <= angleCameraMinY)
                rotationY = angleCameraMinY;
            if (rotationY >= angleCameraMaxY)
                rotationY = angleCameraMaxY;

            transform.localRotation = Quaternion.Euler(-rotationX, rotationY, 0);
        } else {
            transform.localRotation = Quaternion.Euler(-rotationX, 0, 0);
        }
    }

    //Управление дистанцией
    private void DistanceCamera() {
        distance += Input.GetAxis("Mouse ScrollWheel") * -sensitivityDistance;
        distance = Mathf.Clamp(distance, distanceMin, distanceMax);
        cameraObj.localPosition = Vector3.back * distance;
    }
}

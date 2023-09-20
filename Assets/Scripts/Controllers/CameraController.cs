using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Serialization;

namespace GunduzDev
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] float moveSpeed;
        [SerializeField] float fastMoveSpeed;
        [SerializeField] float minXRot, maxXRot;
        private float _curXRot;
        [SerializeField] float minZoom, maxZoom;
        [SerializeField] float zoomSpeed;
        [SerializeField] float rotateSpeed;
        [SerializeField] float curZoom;

        private Camera _cam;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _cam = Camera.main;
            curZoom = _cam.transform.localPosition.y;
            _curXRot = 50f;
        }

        private void Update()
        {
            ZoomCamera();
            RotateCamera();
            MoveCamera();
        }

        private void ZoomCamera()
        {
            curZoom += Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;
            curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);
            _cam.transform.localPosition = Vector3.up * curZoom;
        }

        private void RotateCamera()
        {
            if (Input.GetMouseButton(1))
            {
                float x = Input.GetAxis("Mouse X");
                float y = Input.GetAxis("Mouse Y");

                _curXRot += rotateSpeed * -y;
                _curXRot = Mathf.Clamp(_curXRot, minXRot, maxXRot);

                transform.eulerAngles = new Vector3(_curXRot, transform.eulerAngles.y + (x * rotateSpeed), 0f);
            }
        }

        private void MoveCamera()
        {
            Vector3 forward = _cam.transform.forward;
            forward.y = 0f;
            forward.Normalize();

            Vector3 right = _cam.transform.right;
            
            float moveZ = Input.GetAxisRaw("Vertical");
            float moveX = Input.GetAxisRaw("Horizontal");

            float currentMoveSpeed = Input.GetKey(KeyCode.LeftShift) ? fastMoveSpeed : moveSpeed; // More Speed
            
            Vector3 dir = forward * moveZ + right * moveX;
            dir.Normalize();
            dir *= currentMoveSpeed * Time.deltaTime;

            transform.position += dir;
        }
    }
}

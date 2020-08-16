﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kamera : MonoBehaviour
{
    public enum RotationAxis
    {
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxis axes = RotationAxis.MouseX;
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    public float sensHorizontal = 5.0f;
    public float sensVertical = 5.0f;

    public float _rotationX = 0;

    public TouchPanel touch;

    void Update()
    {
        if (axes == RotationAxis.MouseX)
        {

            //transform.Rotate(0, Input.GetAxis("Mouse X") * sensHorizontal, 0);
            transform.Rotate(0, touch.TouchDist.x * sensHorizontal, 0);
        }
        else if (axes == RotationAxis.MouseY)
        {
            //_rotationX -= Input.GetAxis("Mouse Y") * sensVertical;
            _rotationX -= touch.TouchDist.y * sensVertical;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert); //Clamps the vertical angle within the min and max limits (45 degrees)

            float rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}
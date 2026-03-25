using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    public float xSense = 30f;
    public float ySense = 30f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        //Calculate camera rotation for looking up and down
        xRotation -= (mouseY * Time.deltaTime) * ySense;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        //Apply this to the cameras transform
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //roate player to look left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSense);
    }
}

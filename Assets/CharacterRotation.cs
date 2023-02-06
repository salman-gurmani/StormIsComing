using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    float rotationSpeed = 0.2f;
    public bool IsTouchOn = false;

    void Update()
    {

        //if (!(IsTouchOn))
        //{
        //   AutoRotate();
        //}
    }
    void OnMouseDrag()
    {
        IsTouchOn = true;
        float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.RotateAround(Vector3.down, XaxisRotation);
    }

    public void AutoRotate()
    {
        transform.Rotate(0f, 20.0f * Time.deltaTime, 0f);
    }

    private void OnMouseUp()
    {
        IsTouchOn = false;
    }
}
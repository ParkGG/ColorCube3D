using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraController : MonoBehaviour
{

    [Tooltip("If there is no target object, this position will be used")]
    public Vector3 worldPosition = Vector3.zero;

    [Header("Rotate Settings")]
    [Tooltip("Offset relative to target")]
    public Vector3 offset = Vector3.zero;
    [Tooltip("How much time it takes to finish rotation")]
    public float duration = 0.5f;


    //Checks if camera is rotating 
    bool isRotating = false;

    float time = 0;
    //angle 
    float angle = 0;



 
    private void LateUpdate()
    {
        if (isRotating)
        {
            Vector3 rotateAroundPos;

            
            rotateAroundPos = worldPosition;

            if (time+ Time.deltaTime < duration)
            {
                transform.RotateAround(rotateAroundPos + offset, Vector3.up, angle * Time.deltaTime / duration);

                time += Time.deltaTime;
            }
            else
            {
            
                transform.RotateAround(rotateAroundPos + offset, Vector3.up, angle * (duration - time) / duration);
                time = 0;
                isRotating = false;
            }

        }

    }

    public void RotateCamera(float angle)
    {
        if (!isRotating)
        {
            this.angle = angle;
            isRotating = true;
        }
    }

}
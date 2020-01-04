using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {


    [Range(10, 25)]
    public float speed;
    public int color;
    public GameObject particlePrefabs;


    private AudioSource audioSource;
    private int targetHeight;
    private bool isFall;


    public bool IsFall
    {
        get { return isFall; }
        set { isFall = value; }
          
    }

 

    /// <summary>
    ///     활성화될 때마다 랜덤하게 시작 위치 설정
    /// </summary>
    private void OnEnable()
    {
        audioSource = this.GetComponent<AudioSource>();

        transform.position = new Vector3(Random.Range(0,5), Global.stageSize.y, Random.Range(0, 5));

        IsFall = true;
    }


    void FixedUpdate () {

        targetHeight = CubeManager.Instance.GetTargetHeight(this);

        if (isFall)
        {
            if ((transform.position + (Vector3.down * speed * Time.deltaTime)).y < targetHeight)
            {
                isFall = false;
                transform.position = new Vector3(transform.position.x, targetHeight, transform.position.z);
                audioSource.Play();

                CubeManager.Instance.Add(this);
                return;

            }
            transform.position = Vector3.Lerp(transform.position, transform.position + (Vector3.down * speed), Time.deltaTime);
        }
    }


    private void OnMouseUp()
    {
        if (!isFall)
        {
            CubeManager.Instance.CubeFind(this);      
        }
    }
   
}

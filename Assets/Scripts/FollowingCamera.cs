using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    private float limitCamMinPosX;
    private float limitCamMaxPosX;
    private float limitCamMinPosY;
    private float limitCamMaxPosY;

    private bool isLimitCam = true;

    [SerializeField] private GameObject target;

    private Camera cam;

    public float orthographicSize => cam.orthographicSize;

    private void Awake()
    {
        ResetLimitValue();
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (isLimitCam)
        {
            this.transform.position =
                new Vector3(Mathf.Clamp(target.transform.position.x, limitCamMinPosX, limitCamMaxPosX),
                    Mathf.Clamp(target.transform.position.y, limitCamMinPosY, limitCamMaxPosY),
                    this.transform.position.z
                );
        }
        else
        {
            this.transform.position = 
                new Vector3(Mathf.Clamp(target.transform.position.x, limitCamMinPosX, limitCamMaxPosX),
                    Mathf.Clamp(target.transform.position.y, limitCamMinPosY, limitCamMaxPosY),
                    this.transform.position.z
                );
        
        }
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
        this.transform.position = target.transform.position + (Vector3.back * 10);
    }

    public void SetLimitCamPosX(float _limitCamMinPosX, float _limitCamMaxPosX)
    {
        this.limitCamMinPosX = _limitCamMinPosX;
        this.limitCamMaxPosX = _limitCamMaxPosX;
    }

    public void SetLimitCamPosY(float _limitCamMinPosY, float _limitCamMaxPosY)
    {
        this.limitCamMinPosY = _limitCamMinPosY;
        this.limitCamMaxPosY = _limitCamMaxPosY;
    }

    public void ResetLimitValue()
    {
        limitCamMinPosX = DefaultSettings.limitMetaCamMinPosX;
        limitCamMaxPosX = DefaultSettings.limitMetaCamMaxPosX;
        limitCamMinPosY = DefaultSettings.limitMetaCamMinPosY;
        limitCamMaxPosY = DefaultSettings.limitMetaCamMaxPosY;
    }

    public void SetLimitCam(bool _isLimitCam)
    {
        isLimitCam = _isLimitCam;
    }

    public void SetOrthographicSize(float _orthographicSize)
    {
        cam.orthographicSize = _orthographicSize;
    }

    public void SetOrthographic(bool _isOrthographic)
    {
        cam.orthographic = _isOrthographic;
    }
    
    public void SetMetaverseCamera()
    {
        ResetLimitValue();
        target                  = MetaverseGameManager.instance.player.gameObject;
        this.transform.position = target.transform.position + (Vector3.back * 10);

        SetLimitCam(true);
        cam.orthographic     = true;
        cam.orthographicSize = 1;

        transform.parent   = null;
        transform.rotation = Quaternion.identity;
    }
}
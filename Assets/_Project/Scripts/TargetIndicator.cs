﻿using UnityEngine;
using UnityEngine.UI;
public class TargetIndicator : MonoBehaviour
{
    private Camera mainCamera;
    private Canvas mainCanvas;
    private Image m_iconImage;
    private RectTransform m_icon;
    public Sprite m_targetIconOnScreen;
    public Sprite m_targetIconOffScreen;
    //public Transform m_followTarget;
    [Space]
    [Range(0, 100)]
    public float m_edgeBuffer;
    public Vector3 m_targetIconScale;
    [Space]
    public bool PointTarget = true;
    //Indicates if the object is out of the screen
    private bool m_outOfScreen;
    void Start()
    {
        mainCamera = Camera.main;
        mainCanvas = FindObjectOfType<Canvas>();
        Debug.Assert((mainCanvas != null), "There needs to be a Canvas object in the scene for the OTI to display");
        InstainateTargetIcon();
    }

   

    void Update()
    {
        
        if(m_icon!=null)
        UpdateTargetIconPosition();
    }
    private void InstainateTargetIcon()
    {
        m_icon = new GameObject().AddComponent<RectTransform>();
        m_icon.transform.SetParent(mainCanvas.transform);
        m_icon.localScale = m_targetIconScale;
        m_icon.name = name + ": OTI icon";

        m_iconImage = m_icon.gameObject.AddComponent<Image>();
        m_iconImage.sprite = m_targetIconOnScreen;//We are not using this currently.
    }
    private void UpdateTargetIconPosition()
    {
       
        m_iconImage.gameObject.SetActive(false);//Only active if its offscreen

        Vector3 newPos = transform.position;
        newPos = mainCamera.WorldToViewportPoint(newPos);
        //Simple check if the target object is out of the screen or inside
        if (newPos.x > 1 || newPos.y > 1 || newPos.x < 0 || newPos.y < 0)
            m_outOfScreen = true;
        else
            m_outOfScreen = false;

        newPos = mainCamera.ViewportToScreenPoint(newPos);
        newPos.x = Mathf.Clamp(newPos.x, m_edgeBuffer, Screen.width - m_edgeBuffer);
        newPos.y = Mathf.Clamp(newPos.y, m_edgeBuffer, Screen.height - m_edgeBuffer);
        m_icon.transform.position = newPos;
       

        //Operations if the object is out of the screen
        if (m_outOfScreen)
        {
            m_iconImage.gameObject.SetActive(true);
            //Show the target off screen icon
            m_iconImage.sprite = m_targetIconOffScreen;

            if (PointTarget)
            {
                //Rotate the sprite towards the target object
                var targetPosLocal = mainCamera.transform.InverseTransformPoint(transform.position);
                var targetAngle = -Mathf.Atan2(targetPosLocal.x, targetPosLocal.y) * Mathf.Rad2Deg - 90;
                //Apply rotation
                m_icon.transform.eulerAngles = new Vector3(0, 0, targetAngle);
            }

        }
        else
        {
            //Reset rotation to zero and swap the sprite to the "on screen" one
            m_icon.transform.eulerAngles = new Vector3(0, 0, 0);
            m_iconImage.sprite = m_targetIconOnScreen;
        }
    }
    private void OnDestroy()
    {
        if(m_icon)
        Destroy(m_icon.gameObject);
    }
}
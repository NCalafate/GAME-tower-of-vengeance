using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIThrobber : MonoBehaviour
{
    [ColorUsage(true, true)] [SerializeField]
    private Color _color;

    
    private RectTransform _rectTransform;
    private RawImage _rawImage;
    private float timer = 0.0f;
    

    private void Awake()
    {
        
        _rectTransform = GetComponent<RectTransform>();
        _rawImage = GetComponent<RawImage>();
    }

    private void Start()
    {
        _rawImage.color = _color;
    }

    private void Update()
    {
        _rectTransform.Rotate( new Vector3( 0, 0, 45*Time.deltaTime ) );
    }
}
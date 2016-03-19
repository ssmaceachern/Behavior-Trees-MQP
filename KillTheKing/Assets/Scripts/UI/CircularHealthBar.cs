﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Scrollbar))]
public class CircularHealthBar : MonoBehaviour
{
    [SerializeField]
    Image CircleImage;
    [SerializeField]
    Color start;
    [SerializeField]
    Color end;

    //[SerializeField]
    //Color current;

    [SerializeField]
    float BarEnd;

    Scrollbar scrollbar { get { return GetComponent<Scrollbar>(); } }

    void Start()
    {
        CircleImage.type = Image.Type.Filled;
        CircleImage.fillMethod = Image.FillMethod.Radial360;
        //CircleImage.fillOrigin = BarBegin;
    }

    void Update()
    {


        CircleImage.fillAmount = Mathf.Clamp(scrollbar.value, 0.001f, BarEnd);
        CircleImage.color = Color.Lerp(start, end, scrollbar.value / BarEnd);
        //current = Color.Lerp(start, end, scrollbar.value);
    }


}
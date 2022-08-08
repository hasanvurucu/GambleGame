using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelEvents : EventTrigger
{
    public override void OnPointerDown(PointerEventData data)
    {
        GetComponent<ItemShowdown>().GetCollectedInfo();
    }
}

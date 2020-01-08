using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
    public event Action OnKeyTriggered;
    public KeyCode triggerKey;

    private void Update()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            OnKeyTriggered?.Invoke();
        }
    }
}

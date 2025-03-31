using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public List<Key> buttonTypes = new List<Key>() { };
    public Action<Key> buttonAction;
    private Button uiButton;
    private Text uiText;
    private float delayTrigger = 0.5f;
    private bool canTrigger = true;

    public String Text {
        get
        {
            return uiText.text;
        }
        set
        {
            uiText.text = value;
        }
    }

    private void Awake()
    {
        uiButton = GetComponent<Button>();
        uiText = GetComponentInChildren<Text>();
        if (buttonTypes.Count != 0)
        {
            RegisterKey(buttonTypes);
        }
        if (uiButton != null)
        {
            uiButton.onClick.AddListener(() => OnTriggerAction());
        }
    }

    public void SetupRegisterKeys(List<Key> keys, string showText, Action<Key> action)
    {
        buttonTypes = keys;
        RegisterKey(buttonTypes);
        if (uiText != null && !string.IsNullOrEmpty(showText))
        {
            uiText.text = showText;
        }
        buttonAction = action;
    }

    public void RegisterKey(List<Key> keys)
    {
        foreach (var key in keys)
        {
            if (key == Key.None)
                return;
            InputManager.Instance.RegisterKey(key, OnTriggerAction);
        }
    }

    private void OnTriggerAction()
    {
        if (canTrigger)
        {
            //Debug.Log($"{buttonTypes[0]} Click");
            StartCoroutine(DelayedTrigger(buttonTypes[0]));
        }
    }

    private IEnumerator DelayedTrigger(Key key)
    {
        canTrigger = false;
        buttonAction?.Invoke(key);
        //uiButton?.onClick.Invoke();
        yield return new WaitForSeconds(delayTrigger);
        canTrigger = true;
    }
}

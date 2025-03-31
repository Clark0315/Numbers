using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<InputManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("InputManager");
                    instance = obj.AddComponent<InputManager>();
                }
            }
            return instance;
        }
    }

    private Dictionary<Key, Action> keyBindings = new Dictionary<Key, Action>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // íêç˚à¬åÆòaâÒí≤îüù…
    public void RegisterKey(Key key, Action callback)
    {
        if (!keyBindings.ContainsKey(key))
        {
            keyBindings.Add(key, callback);
        }
        else
        {
            bool callbackExists = false;
            foreach (var existingCallback in keyBindings[key].GetInvocationList())
            {
                if (existingCallback == (Delegate)callback)
                {
                    callbackExists = true;
                    break;
                }
            }

            if (!callbackExists)
            {
                keyBindings[key] += callback;
            }
        }
    }

    // éÊè¡íêç˚à¬åÆ
    public void UnregisterKey(Key key)
    {
        if (keyBindings.ContainsKey(key))
        {
            keyBindings.Remove(key);
        }
    }

    void Update()
    {
        foreach (var keyBinding in keyBindings)
        {
            if (Keyboard.current[keyBinding.Key].wasPressedThisFrame)
            {
                keyBinding.Value.Invoke();
            }
        }
    }
}

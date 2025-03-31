using System.Collections;
using UnityEngine;

public class EnterAction : MonoBehaviour
{
    public GameObject Hole;
    public GameObject Hammer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnterAction(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.Keypad7:
                ShowHitAction();
                break;
        }
    }

    public void ShowHitAction()
    {
        Debug.LogError("ShowHitAction");
        StartCoroutine(HitAction());
    }

    private IEnumerator HitAction()
    {
        Hammer?.SetActive(true);
        yield return new WaitForSeconds(1);
        Hammer?.SetActive(false);
    }
}

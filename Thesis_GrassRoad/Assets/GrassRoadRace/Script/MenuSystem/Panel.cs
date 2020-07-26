using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    private Canvas canvas = null;
    private MenuManager menuManager = null;
    
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    public void Setup(MenuManager menuManager)
    {
        this.menuManager = menuManager;
        Hide();
    }

    public void Show()
    {
        canvas.enabled = true;
        //canvas.gameObject.SetActive(true);
    }

    public void Hide()
    {
        canvas.enabled = false;
        //canvas.gameObject.SetActive(false);
    }
}

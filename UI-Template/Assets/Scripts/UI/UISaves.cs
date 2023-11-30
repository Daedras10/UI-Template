using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

public class UISaves : UIToolkitCanvas
{
    protected void Init()
    {
        root.style.visibility = Visibility.Hidden;
        //Add callback to close button //TODO
    }

    public void Show()
    {
        root.style.visibility = Visibility.Visible;
    }
    
    public void Hide()
    {
        root.style.visibility = Visibility.Hidden;
    }
}

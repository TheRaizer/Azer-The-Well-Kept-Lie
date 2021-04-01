using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInScreenBounds : MonoBehaviour
{
    public bool IsMouseOverGameWindow { get { return !(0 > Input.mousePosition.x || 
                                        0 > Input.mousePosition.y || 
                                        Screen.width < Input.mousePosition.x ||
                                        Screen.height < Input.mousePosition.y); } }
}

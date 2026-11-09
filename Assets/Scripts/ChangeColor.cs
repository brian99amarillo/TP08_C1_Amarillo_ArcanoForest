using UnityEngine;
using System;

public class ChangeColor 
{

    public static event Action <SpriteRenderer, Color, float> OnChangeColor;  
   

    public static void Change(SpriteRenderer spriteRenderer, Color color, float duration)
    {

        OnChangeColor?.Invoke(spriteRenderer,color,duration);
    }

}

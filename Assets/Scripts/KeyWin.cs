using UnityEngine;

public class KeyWin: Pickable
{

    protected override bool Apply(GameObject player)
    {
        UIWin.Instance.ShowWin();
        return true;
    }


}
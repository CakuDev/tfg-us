using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicAsync : CinematicItem
{
    private List<CinematicItem> asyncItems;

    public override IEnumerator Action()
    {
        for (int i = 0; i < asyncItems.Count - 1; i++)
        {
            asyncItems[i].Action();
        }
        yield return asyncItems[asyncItems.Count - 1].Action();
    }
}

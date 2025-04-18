using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemPicker : MonoBehaviour
{
    private Animator anim;
    public int gems = 0;
    public TMP_Text gemsText;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Gem")
        {
            AudioManager.instance.PlaySFX("coinPick");
            gems++;
            gemsText.text = gems.ToString();
            Destroy(coll.gameObject);
        }
    }

    public int getGems()
    {
        return gems;
    }
}

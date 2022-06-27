using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUPUI : MonoBehaviour
{
    public void SelectSkill()
    {
        //Hide after choose skill
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

}

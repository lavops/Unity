using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public MapPoint up, right, down, left;
    public bool isLevel, isLocked;
    public string levelToLoad, levelToCheck, levelName;

    public int gemsCollected, totalGems;
    public float bestTime, targetTime;

    public GameObject gemBadge, timeBadge;

    // Start is called before the first frame update
    void Start()
    {
        if(isLevel && levelToLoad != null)
        {
            if(PlayerPrefs.HasKey(levelToLoad + "_Gems_Collected"))
            {
                gemsCollected = PlayerPrefs.GetInt(levelToLoad + "_Gems_Collected");
            }

            if(PlayerPrefs.HasKey(levelToLoad + "_Time"))
            {
                bestTime = PlayerPrefs.GetFloat(levelToLoad + "_Time");
            }

            if(gemsCollected >= totalGems && gemsCollected > 0)
            {
                gemBadge.SetActive(true);
            }

            if(bestTime <= targetTime && bestTime != 0)
            {
                timeBadge.SetActive(true);
            }

            isLocked = true;

            if(levelToCheck != null)
            {
                if(PlayerPrefs.HasKey(levelToCheck + "_Unlocked"))
                {
                    if (PlayerPrefs.GetInt(levelToCheck + "_Unlocked") == 1)
                    {
                        isLocked = false;
                    }
                }
            }

            if(levelToLoad == levelToCheck)
            {
                isLocked = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

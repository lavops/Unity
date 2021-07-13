using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour
{
    public LSPlayer player;
    private MapPoint[] allPoints;

    // Start is called before the first frame update
    void Start()
    {
        allPoints = FindObjectsOfType<MapPoint>();
        
        if(PlayerPrefs.HasKey("Current_Level"))
        {
            foreach(MapPoint point in allPoints)
            {
                if (point.isLevel && point.levelToLoad == PlayerPrefs.GetString("Current_Level"))
                {
                    player.transform.position = point.transform.position;
                    player.currentPoint = point;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCo());
    }

    private IEnumerator LoadLevelCo()
    {
        LSUIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / LSUIController.instance.fadeSpeed) + .25f);

        SceneManager.LoadScene(player.currentPoint.levelToLoad);
    }
}

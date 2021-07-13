using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToRespawn;

    public int gemsCollected;

    public string levelToLoad;

    public float timeInLevel;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeInLevel = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeInLevel += Time.deltaTime;
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {
        AudioManager.instance.PlaySFX(8);

        PlayerController.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed));

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .2f);

        UIController.instance.FadeFromBlack();

        PlayerController.instance.gameObject.SetActive(true);

        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;

        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;

        UIController.instance.UpdateHealthDisplay();
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo()
    {
        PlayerController.instance.stopInput = true;

        CameraController.instance.stopFollow = true;

        UIController.instance.levelCompleteText.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + 0.25f);

        // Set global variables

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Unlocked", 1);

        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_Gems_Collected"))
        {
            if(PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_Gems_Collected") < gemsCollected)
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Gems_Collected", gemsCollected);
            }
        } else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Gems_Collected", gemsCollected);
        }

        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_Time"))
        {
            if(PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_Time") > timeInLevel)
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_Time", timeInLevel);
            }
        } else
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_Time", timeInLevel);
        }

        SceneManager.LoadScene(levelToLoad);
    }
}

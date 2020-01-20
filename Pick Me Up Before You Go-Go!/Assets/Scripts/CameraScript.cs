using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public AudioSource mSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;
    public AudioClip musicClipSix;

    public Text restartText;
    public Text helpText;

    private static int playerScore;

    private bool gameOver;
    private bool restart;

    private bool gamePaused;

    private static float playerTime;

    void Start()
    {
        StartCoroutine(ATime(2.0f));
        mSource.clip = musicClipSix;
        mSource.Play();
        restart = false;
        helpText.text = "Collect objects in order! Use WASD or Arrow Keys to move.";
        gamePaused = true;
        restartText.text = "";
        playerTime = 10;
    }

    IEnumerator ATime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        helpText.text = "Get the Floppy Disk!";
        mSource.clip = musicClipOne;
        mSource.Play();
        gamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gamePaused == false)
        {
            playerTime -= Time.deltaTime;
        }
        playerScore = PlayerScript.score;
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (playerScore >= 1)
        {
            helpText.text = "Get the Walkman!";
        }

        if (playerScore >= 2)
        {
            helpText.text = "Get the Camera!";
        }

        if (playerScore >= 3)
        {
            helpText.text = "Get the Cell Phone!";
        }

        if (playerScore >= 4)
        {
            if (mSource.clip == musicClipOne)
            {

                mSource.clip = musicClipTwo;

                mSource.Play();

            }
            helpText.text = "";
            gameOver = true;
            restart = true;
        }

        if (playerTime < 0)
        {
            if (mSource.clip == musicClipOne)
            {
                mSource.clip = musicClipThree;

                mSource.Play();
            }
            helpText.text = "";
            gameOver = true;
            restart = true;
        }

        if (gameOver)
        {
            //StartCoroutine(BTime(2.0f));
            restartText.text = "Press 'R' To Restart";
            restart = true;

        }

        /*IEnumerator BTime(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene("Main");
        }*/

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;

    public bool startPlaying;
    public static GameManager instance;
    public int currentScore;
    public int scorePerNote = 100;
    private int scoreCheckpoint = 0; // Dùng để kiểm tra điểm cho việc tăng tốc độ BoatControllers

    public Text scoreText;
    public Text multiText;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public BoatController boatController; // Tham chiếu tới BoatController

    // Use this for initialization
    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theMusic.Play();
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        multiText.text = "Multiplier: x" + currentMultiplier;

        currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;

        // Kiểm tra nếu currentScore đã tăng thêm 100 kể từ lần cập nhật trước
        if (currentScore - scoreCheckpoint >= 100)
        {
            boatController.IncreaseSpeed(0.5f); // Tăng tốc độ của BoatController thêm 2
            scoreCheckpoint = currentScore; // Cập nhật checkpoint
        }
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");
    }
}

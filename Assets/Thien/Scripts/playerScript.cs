using UnityEngine;
using TMPro;
using System.Collections; // Import TextMeshPro namespace

public class playerScript : MonoBehaviour
{
    public UIFishing UIFishing;
    public Animator playerAnim;
    public bool isFishing;
    public bool poleBack;
    public bool throwBobber;
    public Transform fishingPoint;
    public GameObject bobber;

    public float targetTime = 0.0f;
    public float savedTargetTime;
    public float extraBobberDistance;

    public GameObject fishGame;

    public float timeTillCatch = 0.0f;
    public bool winnerAnim;

    public int countFish;
    public int lives = 3; // Initial lives count

    // TextMeshPro variable for quest text and lives text
    public TextMeshProUGUI questText;
    public TextMeshProUGUI livesText;

    public int questFishGoal = 5;

    // Game Over UI
    public GameObject gameOverUI;

    // Delay for next fishing action
    private bool canFishAgain = true;

    void Start()
    {
        isFishing = false;
        fishGame.SetActive(false);
        throwBobber = false;
        targetTime = 0.0f;
        savedTargetTime = 0.0f;
        extraBobberDistance = 0.0f;
        UpdateQuestText();
        UpdateLivesText();
        gameOverUI.SetActive(false); // Ensure Game Over UI is hidden at the start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isFishing) // Press E to stop fishing and allow movement
        {
            StopFishing(); // Call the method to stop fishing
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isFishing && canFishAgain && winnerAnim == false)
        {
            poleBack = true;
        }

        if (isFishing)
        {
            timeTillCatch += Time.deltaTime;
            if (timeTillCatch >= 3)
            {
                fishGame.SetActive(true);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && !isFishing && canFishAgain && winnerAnim == false)
        {
            poleBack = false;
            isFishing = true;
            throwBobber = true;
            if (targetTime >= 3)
            {
                extraBobberDistance += 3;
            }
            else
            {
                extraBobberDistance += targetTime;
            }
        }

        Vector3 temp = new Vector3(extraBobberDistance, 0, 0);
        fishingPoint.transform.position += temp;

        if (poleBack)
        {
            playerAnim.Play("PlayerSwingBack");
            savedTargetTime = targetTime;
            targetTime += Time.deltaTime;
        }

        if (isFishing)
        {
            if (throwBobber)
            {
                Instantiate(bobber, fishingPoint.position, fishingPoint.rotation, transform);
                fishingPoint.transform.position -= temp;
                throwBobber = false;
                targetTime = 0.0f;
                savedTargetTime = 0.0f;
                extraBobberDistance = 0.0f;
            }
            playerAnim.Play("PlayerFishing");
        }

        if (Input.GetKeyDown(KeyCode.P) && timeTillCatch <= 3)
        {
            playerAnim.Play("PlayerStill");
            poleBack = false;
            throwBobber = false;
            isFishing = false;
            timeTillCatch = 0;
        }
    }

    public void fishGameWon()
    {
        countFish++;
        playerAnim.Play("PlayerWonFish");

        poleBack = false;
        throwBobber = false;
        isFishing = false;
        timeTillCatch = 0;

        UpdateQuestText();

        if (countFish >= questFishGoal)
        {
            UIFishing.ShowSuccessMenu(); // Show success menu
        }

        StartCoroutine(FishingCooldown()); // Start cooldown after fishing
    }

    public void fishGameLossed()
    {
        lives--; // Decrease life count
        UpdateLivesText(); // Update lives UI

        playerAnim.Play("PlayerStill");
        fishGame.SetActive(false);
        poleBack = false;
        throwBobber = false;
        isFishing = false;
        timeTillCatch = 0;

        if (lives <= 0)
        {
            GameOver(); // Handle game over
        }
    }

    private void StopFishing()
    {
        playerAnim.Play("PlayerStill"); // Change animation to PlayerStill
        poleBack = false;
        throwBobber = false;
        isFishing = false;
        timeTillCatch = 0; // Reset time till catch
    }

    private IEnumerator FishingCooldown()
    {
        canFishAgain = false; // Prevent fishing
        yield return new WaitForSeconds(Random.Range(3f, 5f)); // Wait for 3 to 5 seconds
        canFishAgain = true; // Allow fishing again
    }

    private void UpdateQuestText()
    {
        questText.text = $"Fish Caught: {countFish}/{questFishGoal}";
        if (countFish >= questFishGoal)
        {
            questText.text += "\nQuest Completed!";
        }
    }

    private void UpdateLivesText()
    {
        livesText.text = $"Lives: {lives}";
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true); // Show Game Over UI
        Time.timeScale = 0; // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Resume the game
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1; // Resume the game
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); // Load main menu scene
    }
    //private void OnTriggerEnter2D(Collider other)
    //{
    //    // Check if the player collides with a wave
    //    if (other.CompareTag("Wave"))
    //    {
    //        lives--;
    //        UpdateLivesText(); // Update lives UI
    //    }
    //}
}
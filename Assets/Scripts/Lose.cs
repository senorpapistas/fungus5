using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public BasicMovement basicmovement;
    public Flashlight flashlight;
    public GameObject playerModel;
    public GameObject loseScreen;

    private void OnEnable()
    {
        PlayerHealth.PlayerChangeHealthEvent += CheckLose;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerChangeHealthEvent -= CheckLose;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckLose(int currentHealth)
    {
        if (currentHealth <= 0)
        {
            LoseState();   
        }
    }

    void LoseState()
    {
        playerHealth.enabled = false;
        basicmovement.enabled = false;
        flashlight.enabled = false;
        playerModel.SetActive(false);
        loseScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

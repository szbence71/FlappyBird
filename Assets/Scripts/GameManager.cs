using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager> {
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI percentText;
    public Slider slider;
    public GameObject settingsPanel; 
    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject backButton;
    public GameObject gameOver;
    public GameObject getReady;
    private int score;
    public AudioClip scoreAudio;

    public override void Init() {
        Application.targetFrameRate = 60;
        AudioListener.volume = PlayerPrefs.GetFloat("volume", 5);
        slider.value = AudioListener.volume * 100f;
        getReady.SetActive(true);
        gameOver.SetActive(false);
        settingsButton.SetActive(true);
        playButton.SetActive(true);
        Pause();
    }

    public void Play() {
        score = 0;
        scoreText.text = score.ToString();
        Player.Instance.transform.position = Vector3.zero;
        Player.Instance.direction = Vector3.up * Player.Instance.strength;
        
        playButton.SetActive(false);
        gameOver.SetActive(false);
        getReady.SetActive(false);
        settingsButton.SetActive(false);

        Time.timeScale = 1f;
        Player.Instance.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++) {
            Destroy(pipes[i].gameObject);
        }
    }

    public void Pause() {
        Time.timeScale = 0f;
        Player.Instance.enabled = false;
    }

    public void GameOver() {
        gameOver.SetActive(true);
        getReady.SetActive(false);
        playButton.SetActive(true);
        settingsButton.SetActive(true);

        Pause();
    }

    public void Settings() {
        settingsPanel.SetActive(true);
    }

    public void DestroyPanel() {
        settingsPanel.SetActive(false);
    }
    
    public void IncreaseScore() {
        score++;
        AudioSource.PlayClipAtPoint(scoreAudio, Vector3.zero, 1);
        scoreText.text = score.ToString();
    }

    public void PercentChange() {
        percentText.text = slider.value.ToString() + "%";
        AudioListener.volume = slider.value / 100f;
        PlayerPrefs.SetFloat("volume", AudioListener.volume);
    }
}

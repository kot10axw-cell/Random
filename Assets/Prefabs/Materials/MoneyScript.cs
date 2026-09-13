using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoneyScript : MonoBehaviour
{
   

     private int _score = 0;
    private Text _scoreText;
    private Text _timerText;
    private Camera _cam;

    [SerializeField] private float roundDuration = 40f;
    [SerializeField] private Font customFont;
    private float _timeLeft;
    private bool _timerStarted = false;
    private bool _gameEnded = false;

    void Start()
    {
        _cam = Camera.main;

        if (_scoreText == null)
        {
            GameObject canvasGO = new GameObject("Canvas");
            Canvas canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();

            GameObject textGO = new GameObject("ScoreText");
            textGO.transform.SetParent(canvasGO.transform);

            _scoreText = textGO.AddComponent<Text>();
            _scoreText.font = customFont;
            _scoreText.fontSize = 32;
            _scoreText.color = new Color32(30, 180, 110, 255);
            _scoreText.alignment = TextAnchor.UpperLeft;

            RectTransform rt = _scoreText.rectTransform;
            rt.anchorMin = new Vector2(0, 1);
            rt.anchorMax = new Vector2(0, 1);
            rt.pivot = new Vector2(0, 1);
            rt.anchoredPosition = new Vector2(10, -10);
            rt.sizeDelta = new Vector2(300, 50);

            GameObject timerGO = new GameObject("TimerText");
            timerGO.transform.SetParent(canvasGO.transform);

            _timerText = timerGO.AddComponent<Text>();
            _timerText.font = customFont;
            _timerText.fontSize = 32;
            _timerText.color = Color.white;
            _timerText.alignment = TextAnchor.UpperCenter;

            RectTransform trt = _timerText.rectTransform;
            trt.anchorMin = new Vector2(0.5f, 1f);
            trt.anchorMax = new Vector2(0.5f, 1f);
            trt.pivot = new Vector2(0.5f, 1f);
            trt.anchoredPosition = new Vector2(0, -10); 
            trt.sizeDelta = new Vector2(400, 100);

            _timeLeft = roundDuration;
            UpdateTimerText();
        }

        UpdateScoreText();
    }

    void Update()
    {
        if (_timerStarted && !_gameEnded)
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft < 0f) _timeLeft = 0f;

            UpdateTimerText();

            if (_timeLeft <= 0f)
            {
                EndGame();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_gameEnded) return;

        if (other.name == "Player")
        {
            if (!_timerStarted)
            {
                _timerStarted = true;
                _timeLeft = roundDuration;
                UpdateTimerText();
            }

            _score++;
            UpdateScoreText();
            MoveToCameraView();
        }
    }

    void MoveToCameraView()
    {
        Vector2 min = _cam.ViewportToWorldPoint(new Vector2(0.1f, 0.2f));
        Vector2 max = _cam.ViewportToWorldPoint(new Vector2(0.9f, 0.8f));
        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);
        transform.position = new Vector2(x, y);
    }

    void UpdateScoreText()
    {
        _scoreText.text = "Coins: " + _score;
    }

    void UpdateTimerText()
    {
        int seconds = Mathf.CeilToInt(_timeLeft);
        _timerText.text = string.Format("{0:00}:{1:00}", seconds / 60, seconds % 60);
    }

    void EndGame()
    {
        _gameEnded = true;
        _timerText.text = "TIME'S UP!";

        RectTransform trt = _timerText.rectTransform;
        trt.anchorMin = new Vector2(0.5f, 0.5f);
        trt.anchorMax = new Vector2(0.5f, 0.5f);
        trt.pivot = new Vector2(0.5f, 0.5f);
        trt.anchoredPosition = Vector2.zero;

        Time.timeScale = 0f;
    }
}

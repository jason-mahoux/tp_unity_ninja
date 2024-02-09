using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerBehaviour PlayerBehaviour;
    public EnemyBehaviour EnemyBehaviour;
    public static GameManager Instance;
    [Range(1f, 3f)]
    public int Lives;
    public RectTransform LivesPanel;
    public Canvas Canvas;
    public Image LifeImagePrefab;
    public Image BossImagePrefab;
    public Image HealthBar;
    public Image HealthBarEnnemy;
    public GameObject OpossumPrefab;
    public GameObject BearPrefab;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnValidate()
    {
        UnityEditor.EditorApplication.delayCall += OnValidateAsync;
    }

    void FixedUpdate()
    {
        if (HealthBar != null)
            HealthBar.fillAmount = PlayerBehaviour.GetHPRatio();

        if (HealthBarEnnemy != null)
            HealthBarEnnemy.fillAmount = EnemyBehaviour.GetHPRatio();
    }

    void OnValidateAsync()
    {
        if (LivesPanel == null)
            return;
        int count = LivesPanel.childCount;
        for (int i = 0; i < count; i++)
        {
            DestroyImmediate(LivesPanel.GetChild(0).gameObject);
        }
        for (int i = 0; i < Lives; i++)
        {
            Instantiate(LifeImagePrefab, LivesPanel);
        }
    }

    public void LoseLife()
    {
        Lives--;
        Destroy(LivesPanel.GetChild(0).gameObject);
        if (Lives <= 0)
        {
            Destroy(Canvas.gameObject);
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

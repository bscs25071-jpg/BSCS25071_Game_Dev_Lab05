using TMPro;                              // TextMesh Pro
using UnityEngine;
using UnityEngine.SceneManagement;        // Restart ke liye

// ============================================================================
//  GameManager.cs  (LECTURE 06 wali script + LAB 05 ke 3 tasks)
//  Empty GameObject "GameManager" pe lagao (sirf EK scene me).
//  Score ka akela malik. Coins sirf ISE report karte hain.
// ============================================================================
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }   // kisi bhi script se: GameManager.Instance

    [SerializeField] int targetScore = 20;      // is score pe WIN (lab ke liye 10 se zyada rakho!)
    [SerializeField] TMP_Text scoreText;        // "Score: x / y" wala text
    [SerializeField] GameObject winPanel;       // win ka panel (shuru me inactive)

    [Header("LAB 05")]
    [SerializeField] Spawner spawner;           // Spawner object yahan drag karo
    [SerializeField] TMP_Text bonusText;        // "BonusCoins: x/50" wala HIDDEN text
    [SerializeField] int bonusTotal = 50;       // kul blue coins

    int score;
    int redTriggerScore;      // TASK 1: is score pe red coin aayega (1..10 random)
    bool redSpawned;          // TASK 1: sirf ek baar spawn hone ka flag
    int bonusCollected;       // TASK 3: ab tak kitne blue coins mile

    void Awake() => Instance = this;

    void Start()
    {
        // TASK 1: random score chuno. Random.Range(int, int) me max EXCLUSIVE hota hai, isliye 11
        redTriggerScore = Random.Range(1, 11);
        Debug.Log($"Red coin spawn hoga jab score = {redTriggerScore}");

        // TASK 3: bonus text shuru me CHHUPA hua
        bonusText.text = $"BonusCoins: 0/{bonusTotal}";
        bonusText.gameObject.SetActive(false);

        UpdateUI();
    }

    // ---------------- LECTURE: score ----------------
    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();

        // TASK 1: target score aa gaya aur red coin abhi tak nahi aaya -> ek baar spawn
        if (!redSpawned && score >= redTriggerScore)
        {
            redSpawned = true;               // ab dobara kabhi nahi
            spawner.SpawnRedCoin();
        }

        if (score >= targetScore) Win();
    }

    // ---------------- TASK 2 ----------------
    // Coin.cs (Red type) isko bulata hai
    public void OnRedCoinCollected() => spawner.SpawnBlueRing();

    // ---------------- TASK 3 ----------------
    // Coin.cs (Blue type) har blue coin pe isko bulata hai
    public void AddBonusCoin()
    {
        bonusCollected++;
        bonusText.gameObject.SetActive(true);                       // hidden text ab show
        bonusText.text = $"BonusCoins: {bonusCollected}/{bonusTotal}";
    }

    void UpdateUI() => scoreText.text = $"Score: {score} / {targetScore}";

    void Win() { winPanel.SetActive(true); Time.timeScale = 0f; }   // game freeze

    public void Restart()                                           // Win panel ke button se
    {
        Time.timeScale = 1f;                                        // PEHLE time wapas chalao
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

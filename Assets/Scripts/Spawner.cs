using UnityEngine;

// ============================================================================
//  Spawner.cs  (LECTURE 06 wali script + LAB 05 ke 2 naye functions)
//  Empty GameObject "Spawner" pe lagao (arena ke beech me).
//  Lecture wala hissa: prefab + clock (InvokeRepeating) = normal coins.
//  Naya hissa: SpawnRedCoin() aur SpawnBlueRing()
// ============================================================================
public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;        // normal (yellow) coin prefab
    [SerializeField] Transform player;             // Player (sphere)
    [SerializeField] float interval = 1.5f;        // har kitne second baad coin
    [SerializeField] int maxCoins = 5;             // ek waqt me zyada se zyada coins
    [SerializeField] float area = 7f;              // arena ka aadha size (x aur z me +-7)

    [Header("LAB 05")]
    [SerializeField] GameObject redCoinPrefab;     // Red coin prefab
    [SerializeField] GameObject blueCoinPrefab;    // Blue coin prefab
    [SerializeField] int blueCount = 50;           // kitne blue coins
    [SerializeField] float ringRadius = 5f;        // player ke gird circle ka radius

    void Start() => InvokeRepeating(nameof(SpawnCoin), 1f, interval);

    // ---------- LECTURE wala normal coin spawn ----------
    void SpawnCoin()
    {
        // transform.childCount = abhi kitne coins bahar hain (coins is spawner ke bachay hain)
        if (transform.childCount >= maxCoins) return;

        float x = Random.Range(-area, area);
        float z = Random.Range(-area, area);
        Vector3 pos = new Vector3(x, 0.5f, z);

        // player ke upar spawn na ho
        if (Vector3.Distance(pos, player.position) < 2f) return;

        // aakhri argument "transform" = spawner ka bachcha banao
        Instantiate(coinPrefab, pos, Quaternion.identity, transform);
    }

    // ---------- TASK 1: sirf EK red coin, random jagah ----------
    // GameManager ise sirf ek baar bulata hai
    public void SpawnRedCoin()
    {
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < 10; i++)                       // 10 koshish: player se door jagah dhoondo
        {
            pos = new Vector3(Random.Range(-area, area), 0.5f, Random.Range(-area, area));
            if (Vector3.Distance(pos, player.position) > 3f) break;
        }
        // Parent NAHI diya (transform nahi likha) -> warna maxCoins ki ginti me aa jata
        Instantiate(redCoinPrefab, pos, Quaternion.identity);
    }

    // ---------- TASK 2: player ke gird circle me 50 blue coins ----------
    public void SpawnBlueRing()
    {
        for (int i = 0; i < blueCount; i++)
        {
            // poora circle = 2*PI radians. 50 barabar hisson me baant diya
            float angle = i * Mathf.PI * 2f / blueCount;

            // circle ka point: center + (cos, sin) * radius
            float x = player.position.x + Mathf.Cos(angle) * ringRadius;
            float z = player.position.z + Mathf.Sin(angle) * ringRadius;

            // arena se bahar na jaye (walls ke peeche phans jate)
            x = Mathf.Clamp(x, -area, area);
            z = Mathf.Clamp(z, -area, area);

            Instantiate(blueCoinPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
        }
    }
}

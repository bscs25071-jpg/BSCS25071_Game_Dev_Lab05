using UnityEngine;

// Teen type ke coins (LAB 05 ke liye naya): normal (yellow), red, blue
public enum CoinType { Normal, Red, Blue }

// ============================================================================
//  Coin.cs  (LECTURE 06 wali script + sirf "type" ka izafa)
//  Coin prefab pe lagao. Collider me "Is Trigger" ON hona chahiye.
//  Coin kabhi score ko khud nahi chhoota -> GameManager ko REPORT karta hai.
// ============================================================================
public class Coin : MonoBehaviour
{
    [SerializeField] CoinType type = CoinType.Normal;   // Inspector me choose karo: Normal / Red / Blue
    [SerializeField] int value = 1;                     // normal coin kitne points
    [SerializeField] float spinSpeed = 90f;             // degrees per second

    // coin ghoomta rehta hai
    void Update() => transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Coin touched by {other.name}");     // proof ke trigger fire hua
        if (other.CompareTag("Player"))                 // sirf Player se
        {
            switch (type)
            {
                case CoinType.Normal:
                    GameManager.Instance.AddScore(value);        // score +1
                    break;
                case CoinType.Red:
                    GameManager.Instance.OnRedCoinCollected();   // TASK 2: 50 blue coins spawn
                    break;
                case CoinType.Blue:
                    GameManager.Instance.AddBonusCoin();         // TASK 3: BonusCoins +1
                    break;
            }
            Destroy(gameObject);   // gameObject (Destroy(this) sirf script hatata hai!)
        }
    }
}

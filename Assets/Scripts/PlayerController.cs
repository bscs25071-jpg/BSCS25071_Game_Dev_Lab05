using UnityEngine;

// ============================================================================
//  PlayerController.cs  (LECTURE 06 wali script - bilkul same)
//  Sphere (tag = Player) pe lagao. Isko Rigidbody + Sphere Collider chahiye.
//  Part 1: AddForce har physics step pe (FixedUpdate me) -> ball roll hoti hai.
// ============================================================================
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveForce = 10f;               // push ki taqat
    [SerializeField] ForceMode mode = ForceMode.Force;    // "Force" = har step lagne wali force

    Rigidbody rb;       // physics body
    Vector3 input;      // keyboard se aane wali direction

    void Awake() => rb = GetComponent<Rigidbody>();

    // INPUT Update me padho (har frame)
    void Update()
    {
        float h = Input.GetAxis("Horizontal");   // A/D ya Left/Right
        float v = Input.GetAxis("Vertical");     // W/S ya Up/Down
        // ClampMagnitude: diagonal pe ball tez na ho jaye
        input = Vector3.ClampMagnitude(new Vector3(h, 0f, v), 1f);
    }

    // FORCE FixedUpdate me lagao (physics ke step ke saath)
    void FixedUpdate() => rb.AddForce(input * moveForce, mode);
}

using UnityEngine;

public class Player : MonoSingleton<Player> {
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    public Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;
    public float rotation = 3f;
    public AudioClip swing;
    
    public override void Init() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            direction = Vector3.up * strength;
            AudioSource.PlayClipAtPoint(swing, Vector3.zero, 1);
        }

        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                direction = Vector3.up * strength;
                AudioSource.PlayClipAtPoint(swing, Vector3.zero, 1);
            }
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void AnimateSprite() {
        spriteIndex++;

        if (spriteIndex >= sprites.Length) {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Obstacle") {
            GameManager.Instance.GameOver();
        } else if (other.gameObject.tag == "Scoring") {
            GameManager.Instance.IncreaseScore();
        }
    }
}

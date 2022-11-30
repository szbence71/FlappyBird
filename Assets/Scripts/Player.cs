using UnityEngine;

public class Player : MonoSingleton<Player> {
    private SpriteRenderer _spriteRenderer;
    public Sprite[] sprites;
    private int _spriteIndex;
    public Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;
    public float rotation = 3f;
    public AudioClip swing;
    
    public override void Init() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        _spriteIndex++;

        if (_spriteIndex >= sprites.Length) {
            _spriteIndex = 0;
        }

        _spriteRenderer.sprite = sprites[_spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Obstacle") {
            GameManager.Instance.GameOver();
        } else if (other.gameObject.tag == "Scoring") {
            GameManager.Instance.IncreaseScore();
        }
    }
}

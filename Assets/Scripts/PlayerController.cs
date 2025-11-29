using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement; // Menyimpan input X dan Y
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. Menerima Input (Horizontal & Vertical)
        // Horizontal = A/D atau Panah Kiri/Kanan
        // Vertical = W/S atau Panah Atas/Bawah
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // 2. Membalik Badan (Hanya saat gerak kiri/kanan)
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void FixedUpdate()
    {
        // 3. Menggerakkan Fisika Karakter ke Segala Arah
        // normalize agar saat jalan miring (diagonal) kecepatannya tidak dobel
        rb.velocity = movement.normalized * moveSpeed;
    }
}
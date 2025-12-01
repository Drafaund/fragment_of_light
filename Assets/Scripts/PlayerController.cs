using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f; // Kekuatan lompat

    [Header("Ground Detection")]
    public Transform groundCheck; // Drag objek GroundCheck kesini
    public LayerMask whatIsGround; // Pilih layer Ground
    public float checkRadius = 0.2f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float moveInput;
    private bool isGrounded;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. Cek apakah kaki menyentuh tanah?
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // 2. Input Gerak Kiri/Kanan
        moveInput = Input.GetAxisRaw("Horizontal");

        // 3. Input Lompat (Panah Atas)
        // Hanya bisa lompat jika ada di tanah (isGrounded)
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Jump();
        }

        // 4. Update Animasi
        animator.SetFloat("Speed", Mathf.Abs(moveInput)); // Pakai Abs agar nilai -1 jadi 1
        animator.SetBool("IsJumping", !isGrounded); // Jika tidak di tanah = Jumping

        // 5. Flip Badan
        if (moveInput > 0) spriteRenderer.flipX = false;
        else if (moveInput < 0) spriteRenderer.flipX = true;
    }

    void FixedUpdate()
    {
        // Gerak Kiri Kanan (Y tetap mengikuti velocity yang ada agar gravitasi bekerja)
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        // Reset kecepatan Y agar lompatan konsisten
        rb.velocity = new Vector2(rb.velocity.x, 0);
        // Tambah gaya ke atas
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
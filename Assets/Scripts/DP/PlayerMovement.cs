using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;

    [Header("Hareket Ayarlarý")]
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float crouchSpeed = 3f;
    public float dashSpeed = 25f;
    public float dashDuration = 0.3f;
    public float dashCooldown = 1f;

    [Header("Zýplama & Yerçekimi")]
    public float jumpPower = 7f;
    public float gravity = 20f;

    [Header("Kamera Kontrolleri")]
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    [Header("Karakter Yüksekliði")]
    public float defaultHeight = 2f;
    public float crouchHeight = 1.2f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool isCrouching = false;
    private bool isDashing = false;
    private bool canMove = true;

    private float lastDashTime = -10f;
    private float lastShiftPressTime = 0f;
    private int shiftPressCount = 0;

    [Header("Karakter Saðlýðý")]
    public float health = 100f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleCameraRotation();
        HandleCrouch();
        HandleDash();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Ölüm animasyonu, oyun sonu vb. iþlemleri burada gerçekleþtirin
        Debug.Log("Oyuncu öldü!");
        // Örneðin, buradan oyuncunun nesnesini yok edebilir veya oyun sonu sahnesine geçebilirsiniz.
        Destroy(gameObject); // Oyuncu nesnesini yok et
    }

    void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && !isCrouching && !isDashing;
        float targetSpeed = isRunning ? runSpeed : walkSpeed;
        if (isCrouching) targetSpeed = crouchSpeed;
        if (isDashing) return; // Dash sýrasýnda normal hareketi durdur

        float curSpeedX = canMove ? targetSpeed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? targetSpeed * Input.GetAxis("Horizontal") : 0;

        float movementDirectionY = moveDirection.y;
        moveDirection = Vector3.Lerp(moveDirection, (forward * curSpeedX) + (right * curSpeedY), 0.1f);
        moveDirection.y = movementDirectionY;

        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump") && canMove)
            {
                moveDirection.y = jumpPower;
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleCameraRotation()
    {
        if (!canMove) return;

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }

        float targetHeight = isCrouching ? crouchHeight : defaultHeight;
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, Time.deltaTime * 10);
    }

    void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            float timeSinceLastPress = Time.time - lastShiftPressTime;
            lastShiftPressTime = Time.time;

            if (timeSinceLastPress < 0.3f) // Eðer iki kere Shift'e basýldýysa
            {
                shiftPressCount++;

                if (shiftPressCount >= 2 && Time.time > lastDashTime + dashCooldown)
                {
                    StartCoroutine(Dash());
                    shiftPressCount = 0;
                }
            }
            else
            {
                shiftPressCount = 1; // Eðer basma aralýðý geçtiyse sayaç sýfýrlanýr
            }
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        lastDashTime = Time.time;

        // O an basýlan tuþlara göre dash yönünü belirle
        Vector3 dashDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) dashDirection += transform.forward;
        if (Input.GetKey(KeyCode.S)) dashDirection -= transform.forward;
        if (Input.GetKey(KeyCode.A)) dashDirection -= transform.right;
        if (Input.GetKey(KeyCode.D)) dashDirection += transform.right;

        if (dashDirection == Vector3.zero)
        {
            dashDirection = transform.forward; // Eðer yön tuþlarýna basýlmadýysa ileriye dash at
        }

        dashDirection.Normalize();

        float startTime = Time.time;
        float dashSpeedLerp = dashSpeed;

        while (Time.time < startTime + dashDuration)
        {
            // Dash hýzýný daha akýcý hale getirmek için Lerp kullanýyoruz
            dashSpeedLerp = Mathf.Lerp(dashSpeed, 0, (Time.time - startTime) / dashDuration);
            characterController.Move(dashDirection * dashSpeedLerp * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
    }
}

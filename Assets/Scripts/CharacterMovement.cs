using UnityEngine;
public class CharacterMovement : MonoBehaviour
{
    public Transform pointA;  // A noktası
    public Transform pointB;  // B noktası
    public float jumpHeight = 1.5f;  // Zıplama yüksekliği
    public float jumpDuration = 0.5f;  // Zıplama süresi
    public float movementSpeed = 5f;  // Hareket hızı

    private Vector3 targetPosition;
    private bool isJumping = false;

    void Start()
    {
        // Hedef pozisyonu belirleme
        targetPosition = pointB.position;
    }

    void Update()
    {
        if (isJumping == false)
        {
            // Karakter zıplamıyorsa, hedefe doğru zıpla
            JumpTowardsTarget();
        }
        else
        {
            // Karakter zıplarken, hedefe doğru ilerle
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
        }
    }

    void JumpTowardsTarget()
    {
        // Hedefe doğru birim vektörü bulma
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Zıplama yüksekliği hesaplama
        float jumpDistance = Vector3.Distance(transform.position, targetPosition);
        float jumpHeight = Mathf.Clamp01(jumpDistance / 5f) * this.jumpHeight;

        // Zıplama işlemi
        targetPosition = targetPosition + Vector3.up * jumpHeight;
        isJumping = true;
        Invoke("ResetJumping", jumpDuration);
    }

    void ResetJumping()
    {
        // Zıplama işaretçisini false yaparak karakterin tekrar zıplayabileceğini belirtin
        isJumping = false;

        // Hedef pozisyonunu değiştirerek karakterin B noktasına doğru zıplamasını sağlayın
        if (targetPosition == pointA.position)
        {
            targetPosition = pointB.position;
        }
        else
        {
            targetPosition = pointA.position;
        }
    }
}
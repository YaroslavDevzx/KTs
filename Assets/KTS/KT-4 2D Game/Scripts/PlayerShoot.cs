using DG.Tweening;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private float damage = 2f;


    [Header("Throw")]
    [SerializeField] private int maxAmmo = 5;
    [SerializeField] private float reloadTime = 1.5f;
    [SerializeField] private float fireRate = 0.2f;

    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject heldProjectileVisual;

    [SerializeField] private GameObject[] bulletPrefabs;

    [Header("Gun Rotation")]
    [SerializeField] private float rotationOffset = 0f;
    [SerializeField] private Transform gunToRotate;
    [SerializeField] private float gunRotateTime = 0.15f;
    [SerializeField] private SpriteRenderer gunSprite;

    [SerializeField] private Animator animator;

    private int currentShot = 0;
    private int currentAmmo;
    private bool isReloading = false;
    private bool isThrowing = false;
    private float lastShotTime = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Throw();
        if (Input.GetKeyDown(KeyCode.R)) StartReload();
        RotateToMouse();
    }

    void SpawnProjectile()
    {
        heldProjectileVisual.SetActive(false);

        if (!isThrowing) return;
        isThrowing = false;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector2 throwDir = (mouseWorldPos - shootPoint.position).normalized;

        float angle = Mathf.Atan2(throwDir.y, throwDir.x) * Mathf.Rad2Deg;
        Quaternion throwRotation = Quaternion.Euler(0f, 0f, angle - 90f + rotationOffset);

        Projectile projectile = Instantiate(bulletPrefabs[currentShot], shootPoint.position, throwRotation).GetComponent<Projectile>();
        projectile.Shoot(damage, throwDir, enemyLayer, gameObject);

        currentShot = (currentShot + 1) % bulletPrefabs.Length;


    }
    void Throw()
    {
        if (isReloading || isThrowing) return;
        if (Time.time < lastShotTime + fireRate) return;
        if (currentAmmo <= 0) return;

        lastShotTime = Time.time;
        currentAmmo--;
        isThrowing = true;

        animator.SetTrigger("Attack");
    }


    void StartReload()
    {
        if (isReloading || currentAmmo == maxAmmo) return;

        isReloading = true;
        DOVirtual.DelayedCall(reloadTime, FinishReload);
    }

    void FinishReload()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
        heldProjectileVisual.SetActive(true);
    }

    void RotateToMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector2 direction = mouseWorldPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        gunToRotate.DORotateQuaternion(Quaternion.Euler(0f, 0f, angle - 90f + rotationOffset), gunRotateTime).SetEase(Ease.OutSine);
        gunSprite.flipY = !(angle > -90f && angle < 90f);
    }

    public int GetAmmo() => currentAmmo;
    public int GetMaxAmmo() => maxAmmo;
    public bool IsReloading() => isReloading;
}
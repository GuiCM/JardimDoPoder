using UnityEngine;

public class Defender : MonoBehaviour
{
    public enum DefenderType { Cactus = 0, Gnome, Gravestone, NotShooter = 99 }

    [SerializeField] private GameObject damageTextHit;
    private GameObject parent;
    private GameObject parentDamageSystem;
    private int layerMaskRayCast;
    private float distanceRay;
    private Vector3 initialRayPosition;

    protected Animator animator;

    public GameObject projectile;
    public DefenderType defenderType;

    #region "Atributos no jogo"

    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] private float projectileDamage = 5f;

    [SerializeField] protected float life = 20f;
    protected float currentLife;

    public int cost = 20;

    #endregion

    // Use this for initialization
    private void Start()
    {
        layerMaskRayCast = LayerMask.GetMask("AttackerLayer");
        animator = GetComponent<Animator>();
        parent = GameObject.Find("Projectiles");

        if (!parent)
        {
            parent = new GameObject("Projectiles");
            parent.transform.position.Set(0f, 0f, 0f);
        }

        parentDamageSystem = GameObject.Find("DamageSystem");

        if (!parentDamageSystem)
        {
            parentDamageSystem = new GameObject("DamageSystem");
            parentDamageSystem.transform.position.Set(0f, 0f, 0f);
        }

        currentLife = life;
        initialRayPosition = transform.position;
        distanceRay = 9.5f - transform.position.x;
    }

    private void Update()
    {
        if (HasEnemyInLine())
        {
            animator.SetBool("IsAttacking", true);
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }
    }

    private bool HasEnemyInLine()
    {
        if (Physics2D.Raycast(initialRayPosition, Vector2.right, distanceRay, layerMaskRayCast))
            return true;

        return false;
    }

    public void Shoot()
    {
        Vector3 spawnPosition = gameObject.transform.position;
        spawnPosition.x += 0.3f;
        GameObject actualProjectile = Instantiate(projectile, spawnPosition, Quaternion.identity, parent.transform);
        actualProjectile.GetComponent<Projectile>().SetAttributes(projectileSpeed, projectileDamage);
    }

    public void ReceiveDamage(float amount)
    {
        currentLife -= amount;
        DisplayTextDamage((int)amount);

        if (currentLife <= 0) //DEAD!
        {
            //Libera o espaço pra uma nova planta e remove esta (Because this IS DEAD!)
            FieldControl.field[(int)transform.position.x - 1, (int)transform.position.y - 1] = false;
            Destroy(gameObject);          
        }
    }

    private void DisplayTextDamage(int amount)
    {
        Vector3 pos = transform.position;
        pos.x -= 0.25f;
        pos.y += 0.35f;
        GameObject newDamageHit = Instantiate(damageTextHit, pos, Quaternion.identity, parentDamageSystem.transform);
        newDamageHit.GetComponentInChildren<TextMesh>().text = "-" + amount;
        Destroy(newDamageHit, 2f);
    }
}

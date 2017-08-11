using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private GameObject star;
    [SerializeField] private float life;
    private float currentSpeed = 0f; //Velocidade atual (Pode sofrer com modificadores (Ex.: Slow))
    private float speedAux = 0f; //Guarda o valor da última velocidade
    private Spawner spawner;
    private GraphicsInterface graphicsInterface;

    [SerializeField] protected float damageCaused;
    protected Animator anim;
    protected GameObject currentTarget;

    [Range(0f, 3f)] public float normalSpeed; //Velocidade normal SEM nenhum modificador (Ex.: Sem slow)
    public float currentLife;

    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set { currentSpeed = speedAux = value; }
    }

    // Use this for initialization
    void Start()
    {
        spawner = GameObject.FindObjectOfType<Spawner>();
        graphicsInterface = GameObject.FindObjectOfType<GraphicsInterface>();

        CurrentSpeed = normalSpeed;
        currentLife = life;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
    }

    //A fox sobrescreve esse método porque ela pode, o marido dela tem 2 empregos!
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(name + " bateu de frente com " + collider);
        if (collision.tag != "Defender")
            return;

        currentTarget = collision.gameObject;
        //A maioria das entidades atacantes não consegue pular a mureta do desespero
        anim.SetBool("IsAttacking", true);
    }

    public void CauseDamageToActualTarget()
    {
        //Se o target atual não foi destruído antes da chamada do método
        if (currentTarget)
        {
            Defender df = currentTarget.GetComponent<Defender>();
            df.ReceiveDamage(damageCaused);
        }
        else //Se não muda a animação pra walking e vai pro próximo target
        {
            anim.SetBool("IsAttacking", false);
            currentTarget = null;
        }
    }

    public void ReceiveDamage(float amount)
    {
        currentLife -= amount;
        
        if (currentLife <= 0) //DEAD!
        {
            spawner.SubtractMonstersAlive();
            GenerateStars();
            Destroy(gameObject);
        }
    }

    private void GenerateStars()
    {
        int randomValue = Random.Range(1, 101);

        if (randomValue < 16)
            Instantiate(star, transform.position, Quaternion.identity);
    }

    #region "Usados no Animator para controle de movimento"

    public void CanMove(int speed)
    {
        if (speed == 0)
            currentSpeed = 0;
        else
            currentSpeed = speedAux;
    }

    public void SetSpeed(float speed)
    {
        currentSpeed = speed;
    }

    #endregion
}

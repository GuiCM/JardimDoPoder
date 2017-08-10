using UnityEngine;

public class StarPlant : Defender
{
    private Player player;
    private float timeToGenerateMoney = 10f;
    private float currentTimeToGenerateMoney;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();

        currentLife = life;
    }

    private void Update()
    {
        currentTimeToGenerateMoney += Time.deltaTime;
        if (currentTimeToGenerateMoney >= timeToGenerateMoney)
        {
            animator.SetTrigger("GenerateMoney");
            currentTimeToGenerateMoney = 0f;
        }
    }

    public void GenerateMoney()
    {
        int value = Random.Range(1, 11);
     
        if (value == 1) //10% de chance de gerar 15 estrelas
            player.AddStars(15);
        else if (value > 1 && value < 5) //30% de chance de gerar 10 estrelas
            player.AddStars(10);
        else //60% de chance de gerar 5 estrelas
            player.AddStars(5);
    }
}

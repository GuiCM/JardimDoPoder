using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] attackers;

    private float currentTime = 0f;
    private float spawnTime;
    private int monstersSpawned;
    private bool canSpawn = false;
    private GameObject parent;
    private GraphicsInterface graphicsInterface;
    private FieldControl fieldControl;
    private Player player;

    public delegate void Method();

    public static int level;
    public static int numMonstersAliveInLevel;
    public int maxNumMonstersOfLevel;

    private void Start()
    {
        graphicsInterface = GameObject.FindObjectOfType<GraphicsInterface>();
        fieldControl = GameObject.FindObjectOfType<FieldControl>();
        player = GameObject.FindObjectOfType<Player>();

        parent = GameObject.Find("Attackers");
        if (!parent)
        {
            parent = new GameObject("Attackers");
            parent.transform.position.Set(0f, 0f, 0f);
        }

        spawnTime = 5f;
        GenerateLevelInformations();
        NewLevelStarts();
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn && monstersSpawned < maxNumMonstersOfLevel)
        {
            currentTime += Time.deltaTime;
            if (currentTime > spawnTime)
                SpawnMonsterInLine();
        }
    }

    IEnumerator CooldownToStartSpawn(Method method, float time)
    {
        yield return new WaitForSeconds(time);
        method();
    }

    private void NewLevelStarts()
    {
        graphicsInterface.ShowInfoOnScreen(0, true);

        StartCoroutine(CooldownToStartSpawn(StartSpawn, 10f));
    }

    private void StartSpawn()
    {
        canSpawn = true;
        graphicsInterface.ShowInfoOnScreen(1, true);
    }

    public void LevelFinished()
    {
        //Just for now
        GameObject defenders = GameObject.Find("Defenders");
        for (int i = 0; i < defenders.transform.childCount; i++)
        {
            GameObject toDestroy = defenders.transform.GetChild(i).gameObject;
            Destroy(toDestroy, 3f);
        }

        GameObject projectiles = GameObject.Find("Projectiles");
        Destroy(projectiles, 2f);

        fieldControl.CleanField();
        //player.DiscountStars(50f);

        //Gera/puxa as informações do próximo level
        GenerateLevelInformations();

        graphicsInterface.RefreshMonsterRemainingBar(numMonstersAliveInLevel, maxNumMonstersOfLevel);
        graphicsInterface.ShowInfoOnScreen(2, false); //Mostra mensagem na tela que acabou o level atual
        StartCoroutine(CooldownToNewLevelStart()); //Espera 5 segundos para começar o novo level
    }

    IEnumerator CooldownToNewLevelStart()
    {
        yield return new WaitForSeconds(8f);
        NewLevelStarts();
    }

    public bool CheckIfLevelIsFinished()
    {
        return numMonstersAliveInLevel == 0;
    }

    private void SpawnMonsterInLine()
    {
        int enemyIndex = Random.Range(0, 2);
        int lineToSpawn = Random.Range(2, 5);

        Instantiate(attackers[enemyIndex], new Vector3(10f, lineToSpawn, 0), Quaternion.identity, parent.transform);

        monstersSpawned++;
        currentTime = 0f;

        spawnTime = Random.Range(10f, 15f);
    }
}

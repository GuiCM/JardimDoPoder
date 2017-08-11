using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] attackers;

    private float spawnTime;
    private float currentTime;
    private float countdownToNewLevelBegin;
    private int monstersSpawned;
    private bool canSpawn;
    private LevelInfo actualLevel;
    //Referências
    private GameObject parent;
    private GraphicsInterface graphicsInterface;
    private FieldControl fieldControl;
    private Player player;
    private LevelManager levelManager;

    public delegate void Method();
    public int numMonstersAliveInLevel;
    //public int maxNumMonstersOfLevel;

    private void Start()
    {
        graphicsInterface = GameObject.FindObjectOfType<GraphicsInterface>();
        fieldControl = GameObject.FindObjectOfType<FieldControl>();
        player = GameObject.FindObjectOfType<Player>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();

        parent = GameObject.Find("Attackers");
        if (!parent)
        {
            parent = new GameObject("Attackers");
            parent.transform.position.Set(0f, 0f, 0f);
        }

        spawnTime = 5f; //Depois que um novo level iniciar, após este tempo os monstros começarão a surgir
        countdownToNewLevelBegin = 8f; //Depois que um level acabar, após este tempo um novo level irá iniciar
        currentTime = 0f;
        monstersSpawned = 0;
        canSpawn = false;

        LoadNextLevelInfo();
    }

    void Update()
    {
        if (canSpawn && (monstersSpawned < actualLevel.AttackersAmount))
        {
            currentTime += Time.deltaTime;
            if (currentTime > spawnTime)
                SpawnMonsterInLine();
        }
    }

    private void LoadNextLevelInfo()
    {
        actualLevel = levelManager.NextLevelInformations();
    }

    private void NewLevelStarts()
    {
        graphicsInterface.ShowInfoOnScreen(0, true);
        StartCoroutine(CountdownToStartSpawn(StartSpawn, 10f));
    }

    private void StartSpawn()
    {
        canSpawn = true;
        graphicsInterface.ShowInfoOnScreen(1, true);
    }

    public void LevelFinished()
    {
        //Limpa os defenders do mapa
        GameObject defenders = GameObject.Find("Defenders");
        for (int i = 0; i < defenders.transform.childCount; i++)
        {
            GameObject toDestroy = defenders.transform.GetChild(i).gameObject;
            Destroy(toDestroy, 3f);
        }

        //Limpa os projéteis que sobraram (se sobrarem)
        GameObject projectiles = GameObject.Find("Projectiles");
        Destroy(projectiles, 2f);

        //Limpa o campo
        fieldControl.CleanField();

        //Gera/puxa as informações do próximo level
        actualLevel = levelManager.NextLevelInformations();

        graphicsInterface.ResetMonsterRemainingBar();
        graphicsInterface.ShowInfoOnScreen(2, false); //Mostra mensagem na tela que acabou o level atual
        StartCoroutine(CountdownToNewLevelStart(countdownToNewLevelBegin)); //Espera 5 segundos para começar o novo level
    }

    public void SubtractMonstersAlive()
    {
        numMonstersAliveInLevel--;
        graphicsInterface.RefreshMonsterRemainingBar(numMonstersAliveInLevel, actualLevel.AttackersAmount);

        if (numMonstersAliveInLevel == 0)
            LevelFinished();
    }

    private void SpawnMonsterInLine(int lineMin, int lineMax)
    {
        int enemyIndex = Random.Range(0, 2);
        int lineToSpawn = Random.Range(lineMin, lineMax + 1);

        Instantiate(attackers[enemyIndex], new Vector3(10f, lineToSpawn, 0), Quaternion.identity, parent.transform);
        monstersSpawned++;
        currentTime = 0f;

        spawnTime = Random.Range(actualLevel.AttackerTimeRateMin, actualLevel.AttackerTimeRateMax);
    }

    //////////////////Co-rotinas//////////////////

    IEnumerator CountdownToStartSpawn(Method method, float time)
    {
        yield return new WaitForSeconds(time);
        method();
    }

    IEnumerator CountdownToNewLevelStart(float time)
    {
        yield return new WaitForSeconds(time);
        NewLevelStarts();
    }
}

using UnityEngine;

public class FieldControl : MonoBehaviour
{
    private Player player;
    private GameObject parent;
    private Defender defender;

    public static bool[,] field = new bool[9, 5];

    public static bool IsSpaceEmpty(Vector2 point)
    {
        return field[(int)point.x, (int)point.y] == false;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        parent = GameObject.Find("Defenders");

        if (!parent)
        {
            parent = new GameObject("Defenders");
            parent.transform.position.Set(0f, 0f, 0f);
        }
    }

    private void OnMouseDown()
    {
        if (!Button.selectedDefender)
            return;
        else
        {
            defender = Button.selectedDefender.GetComponent<Defender>();
        }

        //Checa se possui moedas suficiente pra comprar a planta
        if (!player.PlayerHasMoney(defender.cost))
        {
            Button.ClearSelectedButton();
            Button.selectedDefender = null;
            return;
        }

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);
        pos.z = 0;

        //Checa se o espaço a colocar está vazio
        if (IsSpaceEmpty(new Vector2(pos.x - 1, pos.y - 1)))
            CreatePlant(pos);
    }

    private void CreatePlant(Vector3 pos)
    {
        Instantiate(Button.selectedDefender, pos, Quaternion.identity, parent.transform);
        Button.selectedDefender = null;
        field[(int)pos.x - 1, (int)pos.y - 1] = true;

        player.UseStars(defender.cost);

        Button.ClearSelectedButton();
    }

    public void CleanField()
    {
        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 5; j++)
                field[i, j] = false;
    }

}

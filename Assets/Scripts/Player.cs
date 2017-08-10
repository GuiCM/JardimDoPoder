using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int currency;
    [SerializeField] private int seeds;
    private GraphicsInterface graphicsInterface;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DiscountStars(50);
            return;
        }
    }

    private void Start()
    {
        graphicsInterface = GetComponent<GraphicsInterface>();

        AddStars(30);
    }

    public void AddStars(int amount)
    {
        currency += amount;
        graphicsInterface.ChangeStars(currency);
    }

    public void UseStars(int amount)
    {
        currency -= amount;
        graphicsInterface.ChangeStars(currency);
    }

    public bool PlayerHasMoney(int amount)
    {
        if (currency < amount)
        {
            graphicsInterface.WriteMessage("Você não possui estrelas suficientes para realizar a compra!", 3f);
            return false;
        }

        return true;
    }

    public void DiscountStars(float percent)
    {
        currency -= (int)(currency * (percent / 100));
        graphicsInterface.ChangeStars(currency);
    }
}

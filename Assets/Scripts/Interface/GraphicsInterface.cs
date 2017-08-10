using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsInterface : MonoBehaviour
{
    [SerializeField] private GameObject panelBgToInformations;
    [SerializeField] private GameObject[] imgsTexts;
    private GameObject canvasInformations;
    private Text txtMessages;
    private Text txtStars;
    private Text txtMonstersRemaining;
    private Image imgBarMRFilled;

    public delegate void Method(Text element);

    private void Awake()
    {
        canvasInformations = GameObject.Find("CanvasInformations");

        txtMessages = GameObject.Find("TxtMessages").GetComponent<Text>();
        txtStars = GameObject.Find("TxtStarsValue").GetComponent<Text>();
        imgBarMRFilled = GameObject.Find("ImgBarMRFilled").GetComponent<Image>();
        txtMonstersRemaining = GameObject.Find("TxtMonsterRemaining").GetComponent<Text>();
    }

    public void ChangeStars(int value)
    {
        txtStars.text = value.ToString();
    }

    public void RefreshMonsterRemainingBar(int numMonstersAlive, int maxNumMonstersOfLevel)
    {
        float deadMonstersAmount = maxNumMonstersOfLevel - numMonstersAlive;
        imgBarMRFilled.fillAmount = deadMonstersAmount / maxNumMonstersOfLevel;
        txtMonstersRemaining.text = deadMonstersAmount + "/" + maxNumMonstersOfLevel;
    }

    #region "Mensagens em tempo de jogo"

    public void WriteMessage(string message, float timeToExpire)
    {
        txtMessages.text = message;
        StartCoroutine(Cooldown(ClearTextElement, txtMessages, timeToExpire));
    }

    IEnumerator Cooldown(Method method, Text element, float time)
    {
        yield return new WaitForSeconds(time);
        method(element);
    }

    private void ClearTextElement(Text element)
    {
        element.text = "";
    }

    #endregion

    /// <summary>
    /// Display an image message in the screen for 4 seconds
    /// </summary>
    /// <param name="indexMessage">The index of the image to show in the array of images</param>
    /// <param name="hasBackground">Controls if the message have background</param>
    public void ShowInfoOnScreen(int indexMessage, bool hasBackground)
    {
        if (hasBackground)
        {
            GameObject newPanel = Instantiate(panelBgToInformations, canvasInformations.transform);
            Destroy(newPanel, 6f);
        }

        GameObject newMessage = Instantiate(imgsTexts[indexMessage], canvasInformations.transform);
        Destroy(newMessage, 6f);
    }
}

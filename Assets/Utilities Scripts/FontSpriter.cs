using System.Collections.Generic;
using UnityEngine;

public class FontSpriter : MonoBehaviour
{
    private string bodyTextFile;
    private string[] linesBody;
    private string headerTextFile;
    private string[] linesHeader;
    private int fullImageXSize;
    private int fullImageYSize;
    private List<CharacterInfo> listCharacters = new List<CharacterInfo>();

    public Font customFont;
    public TextAsset fileFont;

    public void GenerateFont()
    {
        if (!Application.isPlaying)
        {
            listCharacters.Clear();

            int indexBodyStart, indexBodyEnd;
            indexBodyStart = fileFont.text.IndexOf("char id");
            indexBodyEnd = fileFont.text.IndexOf("kernings count") - 2;

            headerTextFile = fileFont.text.Substring(0, indexBodyStart);
            bodyTextFile = fileFont.text.Substring(indexBodyStart, indexBodyEnd - indexBodyStart);

            linesBody = bodyTextFile.Split('\n');

            int charactersCount = int.Parse(headerTextFile.Substring(headerTextFile.IndexOf("chars count=") + 12));

            fullImageXSize = int.Parse(headerTextFile.Substring(headerTextFile.IndexOf("scaleW") + 7, 3));
            fullImageYSize = int.Parse(headerTextFile.Substring(headerTextFile.IndexOf("scaleH") + 7, 3));

            for (int i = 0; i < charactersCount; i++)
            {
                string[] items;
                linesBody[i] = linesBody[i].Remove(0, 5);
                items = linesBody[i].Split(' ');

                CharacterInfo cInfo = new CharacterInfo();
                cInfo.index = int.Parse(items[0].Substring(items[0].IndexOf("=") + 1));
                cInfo.vert.width = int.Parse(items[3].Substring(items[3].IndexOf("=") + 1));
                cInfo.vert.height = int.Parse(items[4].Substring(items[4].IndexOf("=") + 1));

                int tempX = int.Parse(items[1].Substring(items[1].IndexOf("=") + 1));
                int tempY = int.Parse(items[2].Substring(items[2].IndexOf("=") + 1));
                //Debug.Log(tempX);
                cInfo.uv.x = tempX / (float)fullImageXSize;
                cInfo.uv.y = (fullImageYSize - (tempY + cInfo.vert.height)) / (float)fullImageYSize;

                cInfo.uv.width = cInfo.vert.width / (float)fullImageXSize;
                cInfo.uv.height = cInfo.vert.height / (float)fullImageYSize;

                cInfo.vert.x = 0;
                cInfo.vert.y = int.Parse(items[6].Substring(items[6].IndexOf("=") + 1)) * -1;
                cInfo.advance = int.Parse(items[7].Substring(items[7].IndexOf("=") + 1));

                cInfo.vert.height *= -1;
                listCharacters.Add(cInfo);
            }

            CharacterInfo[] cInfoCollection = listCharacters.ToArray();
            customFont.characterInfo = cInfoCollection;

            print("Fonte criada com sucesso!");
        }
    }
}

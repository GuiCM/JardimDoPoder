using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FontSpriter))]
public class FontSpriterButton : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FontSpriter fs = (FontSpriter)target;
        if (GUILayout.Button("Gerar Fonte"))
        {
            fs.GenerateFont();
        }
    }
}

using System;
using UnityEditor;
using UnityEngine;

public class CreateNewClassEditor : EditorWindow
{
    /// <summary>
    /// Path of script template
    /// </summary>
    private const string pathToYourScriptTemplate =
        "Assets/Scripts/Scriptable Object Architecture/Variable/GenericVariable.cs.txt";

    /// <summary>
    /// Type of new variable
    /// </summary>
    private static string _typeName = "float";

    /// <summary>
    /// Path of the generated code
    /// </summary>
    private static string _path = "Assets/Scripts/Scriptable Object Architecture/Generated Code/";

    /// <summary>
    /// Window of this class
    /// </summary>
    private static CreateNewClassEditor window;

    /// <summary>
    /// Get Type name
    /// </summary>
    public static string TypeName
    {
        get => _typeName;
    }

    [MenuItem("SO-Architecture/New Variable Type")]
    public static void ShowWindow()
    {
        _typeName = "float";
        window = GetWindow<CreateNewClassEditor>(false, "New Variable", true);
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.HelpBox("Variable type : Can be float, bool, Vector3 etc or any enum or struct",
            MessageType.Info);
        _typeName = EditorGUILayout.TextField("Variable Type", _typeName);
        EditorGUILayout.HelpBox("Path to save generated script",
            MessageType.Info);
        _path = EditorGUILayout.TextField("Path", _path);

        // draw button and give true if button is pressed
        if (GUILayout.Button("Create Class"))
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(pathToYourScriptTemplate,
                _path + SO_Utility.FirstCharToUpper(_typeName) + "Variable.cs");

            window.Close();
        }
    }
}

public class CustomAssetModificationProcessor : UnityEditor.AssetModificationProcessor
{
    static void OnWillCreateAsset(string path)
    {
        if (!path.Contains(".cs"))
            return;

        path = path.Replace(".meta", "");
        int index = path.LastIndexOf(".");
        string file = path.Substring(index);

        index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;
        file = System.IO.File.ReadAllText(path);

        file = file.Replace("#TYPE#", CreateNewClassEditor.TypeName);
        file = file.Replace("#VARIABLENAME#", string.Format("{0}-{1}","Variable",SO_Utility.FirstCharToUpper(CreateNewClassEditor.TypeName)));

        System.IO.File.WriteAllText(path, file);
        AssetDatabase.Refresh();
    }
}
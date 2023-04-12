using UnityEditor;

public class CreateNewScriptClassFromCustomTemplate
{
    private const string customStateTemplatePath = "Assets/ScriptTemplates/CustomState.cs.txt";
    private const string customInterfaceTemplatePath = "Assets/ScriptTemplates/NewInterface.cs.txt";
    private const string customCSharpClassTemplatePath = "Assets/ScriptTemplates/NewCSharpClass.cs.txt";
 
    [MenuItem(itemName: "Assets/Create/Custom Script Templates/Custom State", isValidateFunction: false, priority: 51)]
    public static void CreateScriptFromTemplate()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(customStateTemplatePath, "NewState.cs");
    }

    [MenuItem(itemName: "Assets/Create/Create New Interface", isValidateFunction: false, priority: 52)]
    public static void CreateInterfaceFromTemplate()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(customInterfaceTemplatePath, "NewInterface.cs");
    }

    [MenuItem(itemName: "Assets/Create/Create New Class", isValidateFunction: false, priority: 53)]
    public static void CreateClassFromTemplate()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(customCSharpClassTemplatePath, "NewClass.cs");
    }
}
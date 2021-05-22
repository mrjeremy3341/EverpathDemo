using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

public class AbilityEditor : OdinMenuEditorWindow
{
    [MenuItem("Tools/Ability Editor")]
    private static void OpenWindow()
    {
        GetWindow<AbilityEditor>().Show();
    }

    private static CreateNewAbility newAbility;

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        newAbility = new CreateNewAbility();
        tree.Add("Create New", newAbility);
        tree.AddAllAssetsAtPath("Ability Editor", "Assets/Abilities", typeof(Ability));
        return tree;
    }

    public class CreateNewAbility
    {
        public CreateNewAbility()
        {
            ability = ScriptableObject.CreateInstance<Ability>();
            ability.name = "New Ability";
        }

        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public Ability ability;

        [InfoBox("Create the new ability SO prior to building it")]
        [Button("Create New Ability Object")]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(ability, "Assets/Abilities/" + newAbility.ability.name + ".asset");
            AssetDatabase.SaveAssets();
            ability = ScriptableObject.CreateInstance<Ability>();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (newAbility != null)
        {
            DestroyImmediate(newAbility.ability);
        }
    }
}



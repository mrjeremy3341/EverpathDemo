using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

public class EverpathElementsCreator : OdinMenuEditorWindow
{
    [MenuItem("Tools/Everpath Elements Creator")]
    private static void OpenWindow()
    {
        GetWindow<EverpathElementsCreator>().Show();
    }

    private static CreateNewAbility newAbility;
    private static CreateNewUnit newUnit;
    private static CreateNewEnemy newEnemy;
    private static CreateNewItem newItem;


    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        newAbility = new CreateNewAbility();
        newUnit = new CreateNewUnit();
        newEnemy = new CreateNewEnemy();
        newItem = new CreateNewItem();
        tree.Add("Create New Ability", newAbility);
        tree.Add("Create New Party Unit", newUnit);
        tree.Add("Create New Enemy", newEnemy);
        tree.Add("Create New Item", newItem);
        tree.AddAllAssetsAtPath("Ability Editor", "Assets/Abilities", typeof(Ability), true);
        tree.AddAllAssetsAtPath("", "Assets/Inventory", typeof(InventoryItem), true);
        tree.AddAllAssetsAtPath("", "Assets/Units", typeof(UnitTemplate), true);
        tree.AddAllAssetsAtPath("", "Assets/Units", typeof(EnemyTemplate), true);
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

    public class CreateNewUnit
    {
        public CreateNewUnit()
        {
            unit = ScriptableObject.CreateInstance<UnitTemplate>();
            unit.name = "New Unit";
        }

        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public UnitTemplate unit;

        [InfoBox("Create the new unit SO prior to building it")]
        [Button("Create New Unit Template")]
        private void CreateNewUnitData()
        {
            AssetDatabase.CreateAsset(unit, "Assets/Units/Party Units" + newUnit.unit.name + ".asset");
            AssetDatabase.SaveAssets();
            unit = ScriptableObject.CreateInstance<UnitTemplate>();
        }
    }

    public class CreateNewEnemy
    {
        public CreateNewEnemy()
        {
            enemy = ScriptableObject.CreateInstance<EnemyTemplate>();
            enemy.name = "New Unit";
        }

        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public EnemyTemplate enemy;

        [InfoBox("Create the new enemy SO prior to building it")]
        [Button("Create New Enemy Template")]
        private void CreateNewUnitData()
        {
            AssetDatabase.CreateAsset(enemy, "Assets/Units/Party Units" + newEnemy.enemy.name + ".asset");
            AssetDatabase.SaveAssets();
            enemy = ScriptableObject.CreateInstance<EnemyTemplate>();
        }
    }

    public class CreateNewItem
    {
        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public InventoryItem item;

        public CreateNewItem()
        {
            item = ScriptableObject.CreateInstance<InventoryItem>();
            item.name = "New Item";
        }

        [Button("Create New Item Object")]
        private void CreateNewItemObject()
        {
            AssetDatabase.CreateAsset(item, "Assets/Inventory/Items" + newItem.item.name + ".asset");
            AssetDatabase.SaveAssets();
            item = ScriptableObject.CreateInstance<InventoryItem>();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (newAbility != null)
        {
            DestroyImmediate(newAbility.ability);
        }

        if (newUnit != null)
        {
            DestroyImmediate(newUnit.unit);
        }
    }
}



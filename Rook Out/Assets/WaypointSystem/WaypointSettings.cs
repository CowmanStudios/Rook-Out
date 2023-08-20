using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[FilePath("Editor/WaypointSettings.asset", FilePathAttribute.Location.PreferencesFolder)]
public class WaypointSettings : ScriptableSingleton<WaypointSettings>
{

    [SerializeField]
    private Waypoint m_WaypointPrefab;

    public static GameObject GetWaypointPrefab()
    {
        return instance.m_WaypointPrefab.gameObject;
    }

    internal static SerializedObject GetSerializedSettings()
    {
        instance.Save(false);
        return new SerializedObject(instance);
    }

    [MenuItem("GameObject/Create Waypoint", priority = 11)]
    public static void CreateWaypointMenu()
    {
        PrefabUtility.InstantiatePrefab(GetWaypointPrefab(), Selection.activeTransform);
    }
}

// Register a SettingsProvider using IMGUI for the drawing framework
static class WaypointSettingsIMGUIRegister
{
    [SettingsProvider]
    public static SettingsProvider CreateWaypointSettingsProvider()
    {
        // First parameter is the path in the Settings window
        // Second parameter is the scope: it only appears in the Project Settings window
        var provider = new SettingsProvider("Project/WaypointSettings", SettingsScope.Project)
        {
            label = "Waypoints",
            guiHandler = (searchContext) =>
            {
                var settings = WaypointSettings.GetSerializedSettings();
                EditorGUILayout.ObjectField(settings.FindProperty("m_WaypointPrefab"), typeof(Waypoint), new GUIContent("Waypoint Prefab"));
                settings.ApplyModifiedPropertiesWithoutUndo();
            },
            keywords = new HashSet<string>(new[] { "Waypoint" })
        };

        return provider;
    }
}

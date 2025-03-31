using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomEditor(typeof(Flock))]
public class FlockEditor : Editor
{
    private Flock flock;

    private readonly List<Object> foldouts = new();
    private readonly List<bool> foldoutStates = new();
    private readonly List<Editor> foldoutEditors = new();

    private void OnEnable()
    {
        flock = (Flock)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        foreach (var item in flock.Behaviours)
        {
            DrawFoldoutEditor(item);
        }
    }

    private void DrawFoldoutEditor(Object editorToDraw, System.Action onPropertiesChanged = null, params string[] excludeProperties)
    {
        int index;

        if (foldouts.Contains(editorToDraw))
        {
            index = foldouts.IndexOf(editorToDraw);
        }
        else
        {
            foldouts.Add(editorToDraw);
            foldoutStates.Add(true);
            foldoutEditors.Add(CreateEditor(editorToDraw));

            index = foldouts.Count - 1;
        }

        if (!editorToDraw) return;

        foldoutStates[index] = EditorGUILayout.InspectorTitlebar(foldoutStates[index], editorToDraw);

        using var check = new EditorGUI.ChangeCheckScope();

        if (!foldoutStates[index]) return;

        var editorSerializedObject = foldoutEditors[index].serializedObject;

        editorSerializedObject.Update();

        excludeProperties = new string[] { "m_Script" }.Concat(excludeProperties).ToArray();
        Editor.DrawPropertiesExcluding(editorSerializedObject, excludeProperties);

        editorSerializedObject.ApplyModifiedProperties();

        if (check.changed && onPropertiesChanged is not null) onPropertiesChanged();
    }

    public override bool RequiresConstantRepaint() => true;
}

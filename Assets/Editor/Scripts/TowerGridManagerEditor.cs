using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TowerGridManager))]
public class TowerGridManagerEditor : Editor
{
    private TowerGridManager gridManager;

    private void OnEnable()
    {
        gridManager = (TowerGridManager)target;
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
        {
            Plane plane = new Plane(Vector3.forward, gridManager.transform.position);
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            if (plane.Raycast(ray, out float enter))
            {
                Vector3 worldPos = ray.GetPoint(enter);
                Vector2Int cell = gridManager.WorldToCell(worldPos);
                gridManager.ToggleCell(cell);

                EditorUtility.SetDirty(gridManager);
                e.Use();
            }
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox(
            "In Scene View:\n" +
            "- Left click per togglare celle\n" +
            "- Scegli in 'Editor Brush' se dipingere Road o Tree",
            MessageType.Info);
    }
}

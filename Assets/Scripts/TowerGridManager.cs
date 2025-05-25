using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TowerGridManager : MonoBehaviour
{
    public enum CellType { Empty, Road, Tree, Tower }

    [Header("Griglia")]
    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;

    [Header("Impostazioni manuali")]
    public List<Vector2Int> roadCells = new List<Vector2Int>();
    public List<Vector2Int> treeCells = new List<Vector2Int>();

    [Header("Editor Brush")]
    public CellType brushType = CellType.Road;
    // Seleziona quale CellType “dipingere” con il click

    private CellType[,] grid;

    private void OnValidate()
    {
        InitGrid();
    }

    private void InitGrid()
    {
        grid = new CellType[width, height];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                grid[x, y] = CellType.Empty;

        foreach (var c in roadCells)
            if (InBounds(c)) grid[c.x, c.y] = CellType.Road;

        foreach (var c in treeCells)
            if (InBounds(c)) grid[c.x, c.y] = CellType.Tree;
    }

    public bool CanPlaceTower(Vector3 worldPos)
    {
        Vector2Int c = WorldToCell(worldPos);
        return InBounds(c) && grid[c.x, c.y] == CellType.Empty;
    }

    public Vector2Int WorldToCell(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt((worldPos.x - transform.position.x) / cellSize);
        int y = Mathf.FloorToInt((worldPos.y - transform.position.y) / cellSize);
        return new Vector2Int(x, y);
    }

    private bool InBounds(Vector2Int c) =>
        c.x >= 0 && c.x < width && c.y >= 0 && c.y < height;

    // Chiamata dall'Editor per aggiungere o togliere la cella corrispondente
    public void ToggleCell(Vector2Int cell)
    {
        if (!InBounds(cell)) return;

        switch (brushType)
        {
            case CellType.Road:
                if (roadCells.Contains(cell)) roadCells.Remove(cell);
                else roadCells.Add(cell);
                break;

            case CellType.Tree:
                if (treeCells.Contains(cell)) treeCells.Remove(cell);
                else treeCells.Add(cell);
                break;

                // qui potresti aggiungere altri tipi di “dipingere”
        }

        OnValidate();  // ricostruisce subito la griglia
    }

    private void OnDrawGizmos()
    {
        if (grid == null) InitGrid();

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                Vector3 cellCenter = transform.position + new Vector3(x + 0.5f, y + 0.5f) * cellSize;
                switch (grid[x, y])
                {
                    case CellType.Empty: Gizmos.color = Color.green; break;
                    case CellType.Road: Gizmos.color = Color.gray; break;
                    case CellType.Tree: Gizmos.color = new Color(0.4f, 0.2f, 0f); break;
                    case CellType.Tower: Gizmos.color = Color.blue; break;
                }
                Gizmos.DrawWireCube(cellCenter, Vector3.one * (cellSize * 0.9f));
            }
    }
}

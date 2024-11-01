using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DigController : MonoBehaviour
{
    public Tilemap groundCoverTilemap;
    public Tilemap trapTilemap;
    public Tilemap groundTilemap;
    public GameObject endPoint;
    public string levelSelectionSceneName;
    public Vector3 startDigPosition;
    public int currentLevel; // Số cấp độ hiện tại (chỉ định trong Inspector)

    private HashSet<Vector3Int> dugPositions = new HashSet<Vector3Int>();
    private bool canDig = false;

    void Start()
    {
        dugPositions.Add(Vector3Int.FloorToInt(startDigPosition));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            canDig = true;
        }

        if (Input.GetMouseButton(0) && canDig)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = groundCoverTilemap.WorldToCell(mouseWorldPos);

            if (dugPositions.Contains(cellPosition) || IsAdjacentToDug(cellPosition))
            {
                if (groundTilemap.HasTile(cellPosition))
                {
                    RevealConnectedGroundArea(cellPosition);
                    return;
                }

                if (groundCoverTilemap.HasTile(cellPosition))
                {
                    groundCoverTilemap.SetTile(cellPosition, null);
                    dugPositions.Add(cellPosition);

                    if (trapTilemap.HasTile(cellPosition))
                    {
                        Debug.Log("Trúng bẫy! Bắt đầu lại.");
                        ResetPlayerPosition();
                    }
                    else if (endPoint != null && cellPosition == groundCoverTilemap.WorldToCell(endPoint.transform.position))
                    {
                        Debug.Log("Chúc mừng! Đã qua màn.");
                        CompleteCurrentLevel();
                        RevealEntireMap();
                        LoadLevelSelectionScene();
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            canDig = false;
        }
    }

    private void CompleteCurrentLevel()
    {
        PlayerPrefs.SetInt("Level_" + currentLevel, 1); // Đánh dấu cấp độ hiện tại là hoàn thành
        PlayerPrefs.SetInt("Level_" + (currentLevel + 1), 1); // Mở khóa cấp độ tiếp theo duy nhất
    }

    private bool IsAdjacentToDug(Vector3Int cellPosition)
    {
        foreach (Vector3Int pos in dugPositions)
        {
            if (Vector3Int.Distance(cellPosition, pos) == 1)
            {
                return true;
            }
        }
        return false;
    }

    private void RevealConnectedGroundArea(Vector3Int start)
    {
        Queue<Vector3Int> toReveal = new Queue<Vector3Int>();
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();
        toReveal.Enqueue(start);

        while (toReveal.Count > 0)
        {
            Vector3Int pos = toReveal.Dequeue();
            if (!visited.Contains(pos) && groundTilemap.HasTile(pos))
            {
                groundCoverTilemap.SetTile(pos, null);
                visited.Add(pos);
                dugPositions.Add(pos);

                foreach (Vector3Int offset in new Vector3Int[] { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right })
                {
                    Vector3Int neighborPos = pos + offset;
                    if (!visited.Contains(neighborPos) && groundTilemap.HasTile(neighborPos))
                    {
                        toReveal.Enqueue(neighborPos);
                    }
                }
            }
        }
    }

    private void RevealEntireMap()
    {
        BoundsInt bounds = groundCoverTilemap.cellBounds;
        foreach (var pos in bounds.allPositionsWithin)
        {
            if (groundCoverTilemap.HasTile(pos))
            {
                groundCoverTilemap.SetTile(pos, null);
            }
        }
    }

    private void LoadLevelSelectionScene()
    {
        SceneManager.LoadScene("Level_UI");
    }

    private void ResetPlayerPosition()
    {
        dugPositions.Clear();
        dugPositions.Add(Vector3Int.FloorToInt(startDigPosition));
        canDig = false;
    }
}

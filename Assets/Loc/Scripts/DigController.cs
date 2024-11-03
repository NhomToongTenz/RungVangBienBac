using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class DigController : MonoBehaviour
{
    public UIManager UIManager; // Tham chiếu tới UIManager
    public Tilemap groundCoverTilemap;
    public Tilemap trapTilemap;
    public Tilemap groundTilemap;
    public GameObject endPoint;
    public Vector3 startDigPosition;
    public int currentLevel;

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
            // Lấy vị trí chuột trong thế giới
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = groundCoverTilemap.WorldToCell(mouseWorldPos);

            // Chỉ cho phép tiết lộ khi vị trí hiện tại đã được đào hoặc liền kề với vị trí đã đào
            if (dugPositions.Contains(cellPosition) || IsAdjacentToDug(cellPosition))
            {
                // Kiểm tra nếu có tile đất (ground)
                if (groundTilemap.HasTile(cellPosition))
                {
                    RevealSmallArea(cellPosition); // Gọi hàm RevealSmallArea
                    return;
                }

                // Kiểm tra nếu có tile lớp che (groundCover)
                if (groundCoverTilemap.HasTile(cellPosition))
                {
                    // Tiết lộ vùng nhỏ quanh vị trí hiện tại
                    RevealSmallArea(cellPosition);
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

    private void RevealSmallArea(Vector3Int center)
    {
        // Tạo một danh sách các vị trí xung quanh điểm vuốt (vùng nhỏ)
        Vector3Int[] offsets = new Vector3Int[]
        {
        Vector3Int.zero, // Vị trí người chơi vuốt
        Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right
        };

        foreach (Vector3Int offset in offsets)
        {
            Vector3Int revealPosition = center + offset;

            // Chỉ tiết lộ nếu ô đó có tile lớp che (groundCover)
            if (groundCoverTilemap.HasTile(revealPosition))
            {
                groundCoverTilemap.SetTile(revealPosition, null);
                dugPositions.Add(revealPosition);
            }
        }

        // Kiểm tra bẫy và điểm đích chỉ tại vị trí chính xác người chơi vuốt (Vector3Int.zero)
        if (trapTilemap.HasTile(center))
        {
            Debug.Log("Trúng bẫy! Bắt đầu lại.");
            UIManager.ShowTrapMenu(); // Hiển thị menu chạm bẫy
        }
        else if (endPoint != null && center == groundCoverTilemap.WorldToCell(endPoint.transform.position))
        {
            Debug.Log("Chúc mừng! Đã qua màn.");
            CompleteCurrentLevel();
            RevealEntireMap();
            UIManager.ShowSuccessMenu(); // Hiển thị menu qua màn
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
}

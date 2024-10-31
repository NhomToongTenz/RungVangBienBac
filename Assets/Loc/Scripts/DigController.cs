using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DigController : MonoBehaviour
{
    public Tilemap groundCoverTilemap; // Lớp che phủ
    public Tilemap trapTilemap; // Lớp bẫy
    public Tilemap groundTilemap; // Lớp đất, đường đi
    public Tilemap backgroundTilemap; // Lớp nền đằng sau lớp Ground
    public GameObject endPoint; // GameObject là đích đến

    private Vector3Int lastCellPosition;
    private List<Vector3Int> dugPositions = new List<Vector3Int>(); // Lưu các vị trí đã đào

    void Update()
    {
        if (Input.GetMouseButton(0)) // Đang vuốt
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = groundCoverTilemap.WorldToCell(mouseWorldPos);

            // Kiểm tra nếu người chơi vuốt đến ô mới để tránh xóa lặp lại
            if (cellPosition != lastCellPosition)
            {
                // Đào qua lớp GroundCover
                if (groundCoverTilemap.HasTile(cellPosition))
                {
                    groundCoverTilemap.SetTile(cellPosition, null); // Xóa tile ở lớp che phủ
                    dugPositions.Add(cellPosition); // Lưu vị trí đã đào

                    // Kiểm tra nếu có bẫy ở vị trí này
                    if (trapTilemap.HasTile(cellPosition))
                    {
                        Debug.Log("Trúng bẫy! Bắt đầu lại.");
                        ResetDugTiles(); // Khôi phục các ô đã đào
                        ResetPlayerPosition(); // Đưa người chơi về điểm xuất phát
                    }
                    // Kiểm tra nếu đạt tới đích
                    else if (endPoint != null && cellPosition == groundCoverTilemap.WorldToCell(endPoint.transform.position))
                    {
                        Debug.Log("Chúc mừng! Đã qua màn.");
                        // Thực hiện logic qua màn, ví dụ: chuyển màn mới
                        SceneManager.LoadScene("Level 2");
                    }
                }

                lastCellPosition = cellPosition; // Cập nhật vị trí cuối cùng đã vuốt
            }
        }
    }

    private void ResetDugTiles()
    {
        // Khôi phục tất cả các tile đã đào trong GroundCover
        foreach (var pos in dugPositions)
        {
            groundCoverTilemap.SetTile(pos, /* đặt tile bạn dùng cho lớp GroundCover ở đây */ null);
        }
        dugPositions.Clear(); // Xóa danh sách vị trí đã đào sau khi khôi phục
    }

    private void ResetPlayerPosition()
    {
        // Đặt lại vị trí của người chơi về điểm xuất phát
        transform.position = new Vector3(0, 0, 0); // Điều chỉnh lại vị trí xuất phát theo ý muốn
    }
}

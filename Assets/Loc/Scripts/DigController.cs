using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class DigController : MonoBehaviour
{
    public Tilemap groundCoverTilemap; // Lớp che phủ
    public Tilemap trapTilemap; // Lớp bẫy
    public Tilemap groundTilemap; // Lớp đất, đường đi
    public Tilemap backgroundTilemap; // Lớp nền đằng sau lớp Ground
    public GameObject endPoint; // Điểm đích
    public Vector3Int startDigPosition; // Vị trí ngôi nhà

    private HashSet<Vector3Int> dugPositions = new HashSet<Vector3Int>(); // Lưu các vị trí đã đào
    private bool canDig = false;

    void Start()
    {
        // Đặt vị trí đào ban đầu ở ngôi nhà
        dugPositions.Add(startDigPosition); // Thêm vị trí bắt đầu vào danh sách các ô đã đào
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Bắt đầu vuốt mới
        {
            canDig = true; // Cho phép đào khi bắt đầu vuốt
        }

        if (Input.GetMouseButton(0) && canDig) // Đang vuốt và được phép đào
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = groundCoverTilemap.WorldToCell(mouseWorldPos);

            // Kiểm tra nếu người chơi vuốt đến một ô có thể đào được (ô đã đào hoặc ô liền kề)
            if (dugPositions.Contains(cellPosition) || IsAdjacentToDug(cellPosition))
            {
                // Kiểm tra nếu ô hiện tại thuộc lớp Ground, nếu có thì hiển thị toàn bộ khu vực Ground liên kết
                if (groundTilemap.HasTile(cellPosition))
                {
                    RevealConnectedGroundArea(cellPosition); // Hiển thị toàn bộ khu vực Ground liên kết
                    return; // Dừng lại để tránh đào tiếp qua ô Ground
                }

                // Đào qua lớp GroundCover
                if (groundCoverTilemap.HasTile(cellPosition))
                {
                    groundCoverTilemap.SetTile(cellPosition, null); // Xóa tile ở lớp che phủ
                    dugPositions.Add(cellPosition); // Lưu vị trí đã đào

                    // Kiểm tra nếu gặp bẫy
                    if (trapTilemap.HasTile(cellPosition))
                    {
                        Debug.Log("Trúng bẫy! Bắt đầu lại.");
                        ResetPlayerPosition(); // Đưa người chơi về vị trí bắt đầu hoặc điểm đã đào trước đó
                    }
                    // Kiểm tra nếu đạt tới đích
                    else if (endPoint != null && cellPosition == groundCoverTilemap.WorldToCell(endPoint.transform.position))
                    {
                        Debug.Log("Chúc mừng! Đã qua màn.");
                        RevealEntireMap(); // Hiển thị toàn bộ bản đồ khi đạt tới điểm đích
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0)) // Khi người chơi thả tay
        {
            canDig = false; // Ngừng đào khi thả tay ra
        }
    }

    private bool IsAdjacentToDug(Vector3Int cellPosition)
    {
        // Kiểm tra xem ô hiện tại có liền kề với bất kỳ ô nào đã đào không
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
        // Sử dụng tìm kiếm theo vùng để hiển thị toàn bộ khu vực Ground liên kết
        Queue<Vector3Int> toReveal = new Queue<Vector3Int>();
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();
        toReveal.Enqueue(start);

        while (toReveal.Count > 0)
        {
            Vector3Int pos = toReveal.Dequeue();
            if (!visited.Contains(pos) && groundTilemap.HasTile(pos))
            {
                // Xóa lớp che phủ tại vị trí này
                groundCoverTilemap.SetTile(pos, null);
                visited.Add(pos);
                dugPositions.Add(pos); // Lưu vị trí đã lộ

                // Thêm các ô xung quanh vào danh sách để kiểm tra
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
        // Ẩn toàn bộ lớp GroundCover để hiển thị bản đồ
        BoundsInt bounds = groundCoverTilemap.cellBounds;
        foreach (var pos in bounds.allPositionsWithin)
        {
            if (groundCoverTilemap.HasTile(pos))
            {
                groundCoverTilemap.SetTile(pos, null); // Xóa tất cả các tile ở lớp che phủ
            }
        }
    }

    private void ResetPlayerPosition()
    {
        // Đưa người chơi về vị trí đã đào gần nhất hoặc điểm bắt đầu
        dugPositions.Clear();
        dugPositions.Add(startDigPosition); // Đặt lại vị trí đào ban đầu
        canDig = false;
    }
}

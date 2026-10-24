// LabRoom - quan he KET TAP (aggregation) voi Device.
// Ket tap (khong phai hop thanh) vi: thiet bi van ton tai khi chua vao
// phong nao; xoa phong khong huy thiet bi (vong doi doc lap).
using System;
using System.Collections.Generic;
using System.Linq;

public class LabRoom
{
    public string RoomId { get; }
    public string RoomName { get; set; }
    public int Capacity { get; set; }
    private readonly List<Device> _devices = new();
    public IReadOnlyList<Device> Devices => _devices.AsReadOnly();

    public LabRoom(string roomId, string roomName, int capacity)
    {
        if (string.IsNullOrWhiteSpace(roomId))
            throw new ArgumentException("Ma phong khong duoc rong.", nameof(roomId));
        RoomId = roomId.Trim(); RoomName = roomName; Capacity = capacity;
    }

    public void AddDevice(Device device)
    {
        if (device is null) throw new ArgumentNullException(nameof(device), "Khong chap nhan thiet bi null.");
        if (_devices.Any(d => d.DeviceId == device.DeviceId))
            throw new InvalidOperationException($"Trung ma: phong {RoomId} da co {device.DeviceId}.");
        _devices.Add(device);
    }

    public bool RemoveDevice(string deviceId)
    {
        var d = FindDevice(deviceId);
        if (d is null) return false;
        return _devices.Remove(d);
    }

    public Device? FindDevice(string deviceId)
        => _devices.FirstOrDefault(d => d.DeviceId == deviceId);

    // Da hinh: Sum goi override cua tung loai, khong if/switch.
    public decimal CalculateAnnualMaintenanceCost()
        => _devices.Sum(d => d.CalculateAnnualMaintenanceCost());

    public List<Device> GetDevicesRequiringMaintenance()
        => _devices.Where(d => d.Status == DeviceStatus.UnderMaintenance).ToList();
}

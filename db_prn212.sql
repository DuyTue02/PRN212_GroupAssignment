/* ============================================================
 PHẦN 1: TẠO DATABASE
============================================================
*/
USE master;
GO

-- Kiểm tra nếu DB đã tồn tại thì xoá đi để tạo mới
IF DB_ID('EVRentalDB') IS NOT NULL
BEGIN
    -- Đóng mọi kết nối đến DB cũ để có thể xoá
    ALTER DATABASE EVRentalDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE EVRentalDB;
END
GO

-- Tạo database mới
CREATE DATABASE EVRentalDB;
GO

-- Sử dụng database vừa tạo
USE EVRentalDB;
GO

/* ============================================================
 PHẦN 2: TẠO CÁC BẢNG
============================================================
*/

/* ============================================================
 PHẦN 1: TẠO DATABASE
============================================================
*/
USE master;
GO

-- Kiểm tra nếu DB đã tồn tại thì xoá đi để tạo mới
IF DB_ID('EVRentalDB') IS NOT NULL
BEGIN
    -- Đóng mọi kết nối đến DB cũ để có thể xoá
    ALTER DATABASE EVRentalDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE EVRentalDB;
END
GO

-- Tạo database mới
CREATE DATABASE EVRentalDB;
GO

-- Sử dụng database vừa tạo
USE EVRentalDB;
GO

/* ============================================================
 PHẦN 2: TẠO CÁC BẢNG (VỚI MẬT KHẨU RÕ)
============================================================
*/

-- 1. Bảng Trạm (Điểm thuê)
CREATE TABLE Stations (
    StationID INT PRIMARY KEY IDENTITY(1,1),
    StationName NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255) NOT NULL
);

-- 2. Bảng Người dùng (Lưu cả 3 vai trò)
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Password NVARCHAR(100) NOT NULL, -- Thay đổi ở đây: lưu mật khẩu rõ
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Admin', 'Staff', 'Customer'))
);

-- 3. Bảng Xe điện
CREATE TABLE Vehicles (
    VehicleID INT PRIMARY KEY IDENTITY(1,1),
    LicensePlate NVARCHAR(20) UNIQUE NOT NULL,
    Model NVARCHAR(50) NOT NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Available', 'Rented', 'Maintenance')),
    CurrentStationID INT FOREIGN KEY REFERENCES Stations(StationID) -- Xe đang ở trạm nào
);

-- 4. Bảng Lịch sử Thuê xe
CREATE TABLE Rentals (
    RentalID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT FOREIGN KEY REFERENCES Users(UserID) NOT NULL,
    VehicleID INT FOREIGN KEY REFERENCES Vehicles(VehicleID) NOT NULL,
    StaffID INT FOREIGN KEY REFERENCES Users(UserID) NOT NULL, -- Nhân viên thực hiện giao xe
    PickupStationID INT FOREIGN KEY REFERENCES Stations(StationID) NOT NULL,
    RentalTime DATETIME NOT NULL DEFAULT GETDATE(),
    ReturnTime DATETIME NULL, -- Sẽ cập nhật khi trả xe
    TotalCost DECIMAL(10, 2) NULL
);

GO

/* ============================================================
 PHẦN 3: THÊM DỮ LIỆU MẪU (VỚI MẬT KHẨU RÕ)
============================================================
*/

-- Thêm 2 trạm
INSERT INTO Stations (StationName, Address)
VALUES 
(N'Trạm Quận 1', N'123 Nguyễn Huệ, Q1, TPHCM'),
(N'Trạm Bình Thạnh', N'456 Xô Viết Nghệ Tĩnh, Bình Thạnh, TPHCM');

-- Thêm 3 loại người dùng
INSERT INTO Users (FullName, Email, Password, Role) -- Thay đổi ở đây
VALUES
(N'Nguyễn Văn Admin', 'admin@email.com', 'admin123', 'Admin'),   -- Mật khẩu rõ
(N'Trần Thị Staff', 'staff@email.com', 'staff123', 'Staff'),     -- Mật khẩu rõ
(N'Lê Khách Hàng', 'customer@email.com', 'khach123', 'Customer'); -- Mật khẩu rõ

-- Thêm 3 xe điện
INSERT INTO Vehicles (LicensePlate, Model, Status, CurrentStationID)
VALUES
('59A-12345', 'VinFast Klara S', 'Available', 1), -- Xe 1 ở trạm Q1
('59B-67890', 'Yadea G5', 'Available', 1),        -- Xe 2 ở trạm Q1
('59C-11122', 'DatBike Weaver', 'Maintenance', 2); -- Xe 3 ở trạm Bình Thạnh

-- Thêm 1 giao dịch thuê (đã hoàn thành)
INSERT INTO Rentals (CustomerID, VehicleID, StaffID, PickupStationID, RentalTime, ReturnTime, TotalCost)
VALUES
(3, 1, 2, 1, '2025-10-20 09:00:00', '2025-10-20 17:00:00', 150000.00);

GO

/* ============================================================
 PHẦN 4: KIỂM TRA
============================================================
*/

PRINT '*** Database EVRentalDB đã được tạo thành công! ***';
PRINT '--- Dữ liệu bảng Users (Mật khẩu đã hiện rõ) ---';
SELECT * FROM Users;

PRINT '--- Dữ liệu bảng Vehicles (vị trí hiện tại) ---';
SELECT V.*, S.StationName AS CurrentLocation 
FROM Vehicles V
LEFT JOIN Stations S ON V.CurrentStationID = S.StationID;

PRINT '--- Dữ liệu bảng Rentals (Lịch sử thuê) ---';
SELECT 
    R.RentalID,
    C.FullName AS CustomerName,
    V.LicensePlate,
    S.FullName AS StaffName
FROM Rentals R
JOIN Users C ON R.CustomerID = C.UserID
JOIN Users S ON R.StaffID = S.UserID
JOIN Vehicles V ON R.VehicleID = V.VehicleID;

GO


--USE QL_dichVuNauTiecLanHue
--GO


INSERT INTO CUSTOMER (CUS_NAME, PHONE_NUMBER , SEX, ADDRESS, CITIZEN_NUMBER) 
VALUES
	(N'Nguyễn Văn Vũ', '0123456789', 1, N'78 đường 17, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', '123456789012'),
	(N'Nguyễn Ninh Ninh', '0234567890', 0, N'76/2 đường Phạm Văn Đồng, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', '234567890123'),
	(N'Lý Tiểu Long', '0345678901', 1, N'26 đường 16, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', '345678901234'),
	(N'Đoàn Thị Minh Nguyệt', '0456789012', 0, N'34/2A, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', '456789012345'),
	(N'Chu Văn Chương', '0567890123', 1, N'24 đường Lê Văn Chí, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', '567890123456');
    

INSERT INTO DISH_TYPE (TYPE_NAME) 
 VALUES 
	(N'Tráng miệng'),
    (N'Món súp'),
	(N'Khai vị'), 
    (N'Gỏi'),
    (N'Mực'),
    (N'Gà'),
    (N'Vịt'),
    (N'Bò'),
    (N'Cá'),
    (N'Tôm'),
	(N'Lẩu');

INSERT INTO UNIT(UNIT_NAME, DESCRIPTION)
VALUES
	(N'Đĩa', N'1 Dĩa'),
	(N'Tô', N'1 Tô'),
	(N'Con', N'1 Con'),
	(N'Lẩu', N'1 Lẩu')


INSERT INTO DISH(DISH_NAME, PRICE, DISH_TYPE_ID, UNIT_ID)
VALUES 
	(N'Rau câu', 20000, 1, 1),
    (N'Trái cây', 30000, 1, 1),
    (N'Sữa chua', 80000, 1, 2),
    (N'Súp cua gà', 200000, 2, 2),
    (N'Súp hải sản', 200000, 2, 2),
    (N'Súp thập cẩm', 180000, 2, 2),
    (N'Súp tóc tiên', 180000, 2, 2),
    (N'Chả đùm - Bánh đa', 100000, 3, 1),
    (N'Khai vị - Kim chi', 90000, 3, 1),
    (N'Khai vị gỏi - Phồng tôm', 100000, 3, 1),
	(N'Gỏi dồi trường - phồng tôm', 100000, 4, 1),
    (N'Gỏi bò Hồng Kông - phồng tôm', 90000, 4, 1),
    (N'Gỏi trộn cải mầm - phồng tôm', 80000, 4, 1),
    (N'Bò trộn ngũ sắc - phồng tôm', 110000, 4, 1),
    (N'Bò tái thấu - phồng tôm', 100000, 4, 1),
    (N'Mực hấp gừng', 150000, 5, 1),
    (N'Mực xào sa tế', 140000, 5, 1),
    (N'Mực chiên giòn', 160000, 5, 1),
    (N'Mực chiên xù', 150000, 5, 1),
    (N'Gà chiên giòn', 300000, 6, 3),
    (N'Cánh gà quay', 200000, 6, 1),
    (N'Gà hấp nấm - Xôi', 280000, 6, 1),	
    (N'Gà sốt Pa tê - Bánh mì', 300000, 6, 3),
    (N'Gà nướng xí muội', 280000, 6, 3),
    (N'Gà tiềm thuốc bắc - Xà lách Xoong', 320000, 6, 3),
    (N'Vịt quay', 250000, 7, 3), 
    (N'Vịt tiềm - Mì', 270000, 7, 3),
    (N'Vịt nướng xí muội', 270000, 7, 3),
    (N'Bò hầm tiêu xanh - Bánh mì', 280000, 8, 2),
    (N'Bò nhúng dấm', 250000, 8, 4),
    (N'Bò lúc lắc - Khoai tây chiên', 280000, 8, 1),
    (N'Bò sốt rượu vang - Bánh mì', 300000, 8, 1),
    (N'Bê né - Bông thiên lý', 260000, 8, 1),
    (N'Cá lóc hấp bánh tráng', 200000, 9, 1),
    (N'Cá tai tượng chiên xù', 210000, 9, 1),
    (N'Cá chẻm phi lê chiên giòn', 190000, 9, 1),
    (N'Tôm hấp bia', 200000, 10, 1),
    (N'Tôm sốt me', 220000, 10, 1),
    (N'Tôm chiên xù', 210000, 10, 1),
    (N'Tôm sốt chanh dây', 220000, 10, 1),
    (N'Tôm cháy tổi', 200000, 10, 1),
    (N'Tôm nướng giấy bạc', 210000, 10, 1),
    (N'Lẩu Thái bún', 200000, 11, 4),
    (N'Lẩu gà lá giang', 200000, 11, 4),
    (N'Lẩu hải sản nấm - bún', 220000, 11, 4),
    (N'Lẩu thập cẩm - Mì vàng', 220000, 11, 4),
    (N'Lẩu cua đồng - mồng tơi - bún', 200000, 11, 4),
	(N'Lẩu cá lăng - Bún', 300000, 11, 4);


INSERT INTO PARTY_TYPE (NAME) 
VALUES
	(N'Sinh nhật'),
	(N'Đám cưới'),
	(N'Tân gia'),
	(N'Đám giỗ'),
	(N'Lễ khai trương');

INSERT INTO Party(PARTY_NAME, QUANTITY , DATE , TIME, LOCATION, NOTE, STATUS, HAS_MENU, CUSTOMER_ID, PARTY_TYPE_ID)
VALUES 
	(N'Đám cưới Kiệt và Hậu', 100, '2023-12-16', '20:00:00', N'78 đường 17, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', N'Note for Party A', N'Sắp diễn ra', 0, 1, 1),
	(N'Khai trương Phát Đạt', 8, '2023-12-22','18:30:00', N'64/2 đường 18, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', N'Note for Party B', N'Sắp diễn ra', 0 ,2, 2),
	(N'Sinh nhật Kim Thỏa', 10, '2023-12-12','12:00:00', N'66/2 đường 18, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', N'Note for Party C', N'Đã xong', 1, 3, 1),
	(N'Khai trương Thuận Phát', 6, '2023-12-05', '19:00:00', N'66 Trần Quang Diệu, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', N'Note for Party D', N'Đang diễn ra',1, 4, 3),
	(N'Đám cưới Tần và Thủy', 120, '2023-12-22', '16:00:00', N'25 Phạm Văn Đồng, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', N'Note for Party E', N'Sắp diễn ra',1,  5, 4),
    (N'Đám cưới Thảo và Nghĩa', 37, '2023-12-30', ' 16:00:00', N'78 đường 17, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', N'Note for Party A', N'Sắp diễn ra', 0, 1, 1);
    

INSERT INTO INVOICE(INVOICE_DATE, PAYMENT_TIME , TOTAL_PRICE , DEPOSIT, TOTAL, PARTY_ID)
VALUES 
	 ('2023-02-02', '2023-02-01 20:00:00', 10000000, 3000000, 7000000, 1),
	 ('2023-02-20', '2023-02-25 08:00:00', 20000000, 6000000,  14000000, 2),
	 ('2023-02-05', '2023-02-07 16:00:00', 7000000, 2100000, 4900000, 3),
	 ('2023-02-11', '2023-02-13 19:00:00', 10000000, 3000000, 7000000, 4),
	 ('2023-02-14', '2023-02-15 19:00:00', 20000000, 6000000,  14000000, 5),
	 ('2023-02-23', '2023-02-24 20:00:00', 10000000, 3000000, 7000000, 6)


INSERT INTO DETAIL_INVOICE(NUMBER, PRICE, AMOUNT, DISH_ID, INVOICE_ID)
VALUES
	(6, 120000, 10000000, 5, 4),
	(7, 125000, 8000000, 6, 4),
	(7, 200000, 1500000, 1, 3),
	(5, 100000, 8000000, 7, 5)
go


--INSERT INTO ACCOUNT(USERNAME, PASSWORD, EMAIL)
--VALUES
--	('admin', 'admin', 'lanhue101@gmail.com'),
--	('user','user@123', 'user1@gmail.com'),
--	('user1','user@123', 'user2@gmail.com'),
--	('user2','user@123', 'user3@gmail.com'),
--	('user3','user@123', 'user4@gmail.com');


INSERT INTO STAFF_TYPE( NAME)
VALUES
	(N'Chạy bàn'),
    (N'Nhà bếp'),
    (N'Tài xế'),
    (N'Quản lý');


--INSERT INTO STAFF(STAFF_NAME, PHONE_NUMBER, SEX, ADDRESS, CITIZEN_NUMBER, STAFF_TYPE_ID, ACCOUNT_ID)
--VALUES
--	('Hoàng Thị Huệ', '0908445378', 1, '130/4, tổ 28, Phường Bình Đa, Thành phố Biên Hòa, Tỉnh Đồng Nai', '433434567479',4, 1),
--	('Nguyễn Văn Trí', '0123456789',0, '78 đường 17, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', '012345678901', 1,2),
--	('Phạm Thị Trang', '0234567890',1, '66/2 đường 18, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', '123456789012', 2, 3),
--	('Trần Văn Nguyễn Ánh', '0345678901', 1, '64/2 đường 18, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', '234567890123', 3, 4),
--	('Lê Quốc Công Thần', '0456789012',0, '25 Phạm Văn Đồng, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', '345678901234', 2, 5)

INSERT INTO STAFF(STAFF_NAME, PHONE_NUMBER, SEX, ADDRESS, CITIZEN_NUMBER, STAFF_TYPE_ID)
VALUES
	(N'Hoàng Thị Huệ', '0908445378', 1, N'130/4, tổ 28, Phường Bình Đa, Thành phố Biên Hòa, Tỉnh Đồng Nai', '433434567479', 4),
	(N'Nguyễn Văn Trí', '0123456789',0, N'78 đường 17, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', '012345678901', 1),
	(N'Phạm Thị Trang', '0234567890',1, N'66/2 đường 18, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', '123456789012', 2),
	(N'Trần Văn Nguyễn Ánh', '0345678901', 1, N'64/2 đường 18, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', '234567890123', 3),
	(N'Lê Quốc Công Thần', '0456789012',0, N'25 Phạm Văn Đồng, Phường Trảng Dài, Thành phố Biên Hòa, Tỉnh Đồng Nai', '345678901234', 2)


--INSERT INTO WORK(STAFF_ID, PARTY_ID, SALARY)
--VALUES
--	(1, 1, 400000),
--    (1, 2, 600000),
--    (1, 3, 400000),
--    (1, 4, 600000),

--    (2, 1, 400000),
--    (2, 2, 600000),
--    (2, 4, 600000),
--    (2, 5, 400000),
    
--    (3, 1, 400000),
--    (3, 2, 600000),
--    (3, 4, 600000),

--    (4, 1, 400000),
--    (4, 2, 600000),
--    (4, 3, 400000),
--    (4, 4, 600000),

--    (5, 2, 600000),
--    (5, 3, 400000),
--    (5, 4, 600000);
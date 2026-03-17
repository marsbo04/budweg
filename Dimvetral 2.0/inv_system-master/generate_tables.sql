CREATE TABLE DeliveryNote
(
    Id        INT IDENTITY (1,1) PRIMARY KEY,
    StartDate Datetime2,
    Status    BIT,
    Name      NVARCHAR(255)
)

INSERT INTO DeliveryNote (StartDate, Status, Name) VALUES ('2026-01-01', 1, 'Delivery Note 1')
INSERT INTO DeliveryNote (StartDate, Status, Name) VALUES ('2026-01-02', 0, 'Delivery Note 2')
INSERT INTO DeliveryNote (StartDate, Status, Name) VALUES ('2026-01-03', 1, 'Delivery Note 3')
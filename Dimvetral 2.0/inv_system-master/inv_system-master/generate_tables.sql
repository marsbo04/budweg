CREATE TABLE DeliveryNote
(
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    StartDate DATETIME2,
    Status    BIT,
    Name      NVARCHAR(255)
);

CREATE TABLE StationHistory
(
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    DeliveryNoteId  INT NOT NULL,
    Name            NVARCHAR(32),
    StartDate       DATETIME2,
    EndDate         DATETIME2,
    Status          BIT,
    Note            NVARCHAR(MAX),

    CONSTRAINT FK_StationHistory_DeliveryNote
        FOREIGN KEY (DeliveryNoteId) REFERENCES DeliveryNote(Id)
);

INSERT INTO DeliveryNote (StartDate, Status, Name)
VALUES
    ('2026-03-16 08:00:00', 1, N'Kaliber A'),
    ('2026-03-16 09:00:00', 1, N'Kaliber B');

INSERT INTO StationHistory (DeliveryNoteId, Name, StartDate, EndDate, Status, Note)
VALUES
    (1, N'Modtagelse', '2026-03-16 08:00:00', '2026-03-16 08:15:00', 1, N'OK'),
    (1, N'Rensning',   '2026-03-16 08:20:00', '2026-03-16 09:00:00', 1, N'OK'),
    (1, N'Montering',  '2026-03-16 09:10:00', '2026-03-16 10:30:00', 0, N'Fejl i pakning'),
    (2, N'Modtagelse', '2026-03-16 09:00:00', '2026-03-16 09:20:00', 1, N'OK');
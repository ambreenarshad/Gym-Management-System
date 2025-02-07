create table audit_trail(
audit_id int identity(1,1) primary key,
username varchar(50),
action varchar(100),
timestamp datetime,
details varchar(200)
);

--trigger1
create trigger TrainerRegistrationRequest
ON TrainerGym
AFTER INSERT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM inserted WHERE status = 'pending')
	BEGIN
		INSERT INTO audit_trail(username, action, timestamp, details)
		SELECT inserted.trainer_username, 'Trainer Registration Request', GETDATE(), 'Trainer registration request is sent to Gym ' + cast(inserted.gymid as varchar(10))
		FROM inserted;
	END;
END;

--trigger2
CREATE TRIGGER TrainerAcceptance
ON TrainerGym
AFTER UPDATE
AS
BEGIN
    IF UPDATE(status) AND EXISTS (SELECT 1 FROM inserted i join deleted d on i.trainer_username=d.trainer_username WHERE i.status = 'accepted' and d.status='pending')
    BEGIN
        INSERT INTO audit_trail (username, action, timestamp, details)
        SELECT i.trainer_username, 'Trainer Acceptance', GETDATE(), 'Trainer is accepted by Gym ' + cast(i.gymid as varchar(10))
        FROM inserted i join deleted d on i.trainer_username=d.trainer_username
		where i.status='accepted' and d.status='pending';
    END;
END;

--trigger3
CREATE TRIGGER TrainerRejection
ON TrainerGym
AFTER DELETE
AS
BEGIN
    DECLARE @Status VARCHAR(50);
    SELECT @Status = status FROM deleted;

    IF @Status = 'pending'
    BEGIN
        INSERT INTO audit_trail (username, action, timestamp, details)
        SELECT deleted.trainer_username, 'Trainer Rejection', GETDATE(), 'Trainer is rejected by Gym ' + cast(deleted.gymid as varchar(10))
        FROM deleted;
    END;
END;

--trigger4
CREATE TRIGGER TrainerRevoked
ON TrainerGym
AFTER UPDATE
AS
BEGIN
    IF UPDATE(status) AND EXISTS (SELECT 1 FROM inserted i join deleted d on i.trainer_username=d.trainer_username WHERE i.status = 'revoked' and d.status='accepted')
    BEGIN
        INSERT INTO audit_trail (username, action, timestamp, details)
        SELECT i.trainer_username, 'Trainer Revoked', GETDATE(), 'Trainer is revoked by Gym ' + cast(i.gymid as varchar(10))
        FROM inserted i join deleted d on i.trainer_username=d.trainer_username
		where i.status='revoked' and d.status='accepted';
    END;
END;

--trigger5
create trigger MemberRegistrationRequest
ON MemberTable
AFTER INSERT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM inserted WHERE status = 'pending')
	BEGIN
		INSERT INTO audit_trail(username, action, timestamp, details)
		SELECT inserted.username, 'Member Registration Request', GETDATE(), 'Member registration request is sent to Gym ' + cast(inserted.gymid as varchar(10))
		FROM inserted;
	END;
END;

--trigger6

CREATE TRIGGER MemberAcceptance
ON MemberTable
AFTER UPDATE
AS
BEGIN
    IF UPDATE(status) AND EXISTS (SELECT 1 FROM inserted i join deleted d on i.username=d.username WHERE i.status = 'accepted' and d.status='pending')
    BEGIN
        INSERT INTO audit_trail (username, action, timestamp, details)
        SELECT i.username, 'Member Acceptance', GETDATE(), 'Member is accepted by Gym ' + cast(i.gymid as varchar(10))
        FROM inserted i join deleted d on i.username=d.username
		where i.status='accepted' and d.status='pending';
    END;
END;

--trigger7
CREATE TRIGGER MemberRejection
ON MemberTable
AFTER DELETE
AS
BEGIN
    DECLARE @Status VARCHAR(50);
    SELECT @Status = status FROM deleted;

    IF @Status = 'pending'
    BEGIN
        INSERT INTO audit_trail (username, action, timestamp, details)
        SELECT deleted.username, 'Member Rejection', GETDATE(), 'Member is rejected by Gym ' + cast(deleted.gymid as varchar(10))
        FROM deleted;
    END;
END;

--trigger8

CREATE TRIGGER MemberRevoked
ON MemberTable
AFTER UPDATE
AS
BEGIN
    IF UPDATE(status) AND EXISTS (SELECT 1 FROM inserted i join deleted d on i.username=d.username WHERE i.status = 'revoked' and d.status='accepted')
    BEGIN
        INSERT INTO audit_trail (username, action, timestamp, details)
        SELECT i.username, 'Member Revoked', GETDATE(), 'Member is revoked by Gym ' + cast(i.gymid as varchar(10))
        FROM inserted i join deleted d on i.username=d.username
		where i.status='revoked' and d.status='accepted';
    END;
END;

--trigger9
create trigger BookSessionRequest
ON SessionTable
AFTER INSERT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM inserted WHERE status = 'pending')
	BEGIN
		INSERT INTO audit_trail(username, action, timestamp, details)
		SELECT inserted.member_username, 'Member Book Session Request', GETDATE(), 'Member session request is sent to Trainer ' + inserted.trainer_username
		FROM inserted;
	END;
END;

--trigger10
CREATE TRIGGER TrainerSessionAccept
ON SessionTable
AFTER UPDATE
AS
BEGIN
    IF UPDATE(status) AND EXISTS (SELECT 1 FROM inserted i join deleted d on i.sessionid=d.sessionid WHERE i.status = 'booked' and d.status='pending')
    BEGIN
        INSERT INTO audit_trail (username, action, timestamp, details)
        SELECT i.trainer_username, 'Trainer Accepting Session', GETDATE(), 'Trainer accepted session request by Member ' + i.member_username
        FROM inserted i join deleted d on i.sessionid=d.sessionid
		where i.status='booked' and d.status='pending';
    END;
END;

--trigger11
CREATE TRIGGER TrainerRejectSession
ON SessionTable
AFTER DELETE
AS
BEGIN
    DECLARE @Status VARCHAR(50);
    SELECT @Status = status FROM deleted;

    IF @Status = 'pending'
    BEGIN
        INSERT INTO audit_trail (username, action, timestamp, details)
        SELECT deleted.member_username, 'Member Session Request Rejection', GETDATE(), 'Member session request was rejected by Trainer ' + deleted.trainer_username
        FROM deleted;
    END;
END;

--trigger12
CREATE TRIGGER TrainerCancelSession
ON SessionTable
AFTER DELETE
AS
BEGIN
    DECLARE @Status VARCHAR(50);
    SELECT @Status = status FROM deleted;

    IF @Status = 'booked'
    BEGIN
        INSERT INTO audit_trail (username, action, timestamp, details)
        SELECT deleted.trainer_username, 'Trainer Cancel Session', GETDATE(), 'Trainer cancelled session with Member ' + deleted.member_username
        FROM deleted;
    END;
END;

--trigger13
create trigger MemberGiveFeedback
ON feedback
AFTER INSERT
AS
BEGIN
		INSERT INTO audit_trail(username, action, timestamp, details)
		SELECT inserted.member_username, 'Member Gave Feedback', GETDATE(), 'Member gave ' + cast(inserted.stars as varchar(10)) + ' stars to Trainer ' + inserted.trainer_username
		FROM inserted;
END;

--trigger14
CREATE TRIGGER trg_Workout_Insert
ON WorkOutPlan_M
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Username VARCHAR(50);
    DECLARE @Action VARCHAR(100);
    DECLARE @Timestamp DATETIME;
    DECLARE @Details VARCHAR(200);

    -- Get the inserted values
    SELECT @Username = inserted.member_username,
           @Action = 'Insert in Member’s WorkoutPlan',
           @Timestamp = GETDATE(),
           @Details = 'New workout added: wpid=' + CAST(inserted.wpid AS VARCHAR(10)) + ', goal=' + inserted.goal + ', experiencelevel=' + inserted.experiencelevel
    FROM inserted;

    -- Insert into audit trail
    INSERT INTO audit_trail (username, action, timestamp, details)
    VALUES (@Username, @Action, @Timestamp, @Details);
END;

--trigger15
CREATE TRIGGER trg_Workout_Insert_Trainer
ON WorkOutPlan_T
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Username VARCHAR(50);
    DECLARE @Action VARCHAR(100);
    DECLARE @Timestamp DATETIME;
    DECLARE @Details VARCHAR(200);

    -- Get the inserted values
    SELECT @Username = inserted.trainer_username,
           @Action = 'Insert in Trainer WorkoutPlans',
           @Timestamp = GETDATE(),
           @Details = 'New workout added: wpid=' + CAST(inserted.wpid AS VARCHAR(10)) + ', goal=' + inserted.goal + ', experiencelevel=' + inserted.experiencelevel
    FROM inserted;

    -- Insert into audit trail
    INSERT INTO audit_trail (username, action, timestamp, details)
    VALUES (@Username, @Action, @Timestamp, @Details);
END;

--trigger16
CREATE TRIGGER trg_DietPlan_Insert
ON DietPlan_M
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Username VARCHAR(50);
    DECLARE @Action VARCHAR(100);
    DECLARE @Timestamp DATETIME;
    DECLARE @Details VARCHAR(200);

    -- Get the inserted values
    SELECT @Username = inserted.member_username,
           @Action = 'Insert in Member’s DietPlan',
           @Timestamp = GETDATE(),
           @Details = 'New workout added: dietid =' + CAST(inserted.dietid AS VARCHAR(10)) + ', purpose=' + inserted.purpose + ', type of meal=' + inserted.type
    FROM inserted;

    -- Insert into audit trail
    INSERT INTO audit_trail (username, action, timestamp, details)
    VALUES (@Username, @Action, @Timestamp, @Details);
END;

--trigger17
CREATE TRIGGER trg_DietPlan_Insert_Trainer
ON DietPlan_T
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Username VARCHAR(50);
    DECLARE @Action VARCHAR(100);
    DECLARE @Timestamp DATETIME;
    DECLARE @Details VARCHAR(200);

    -- Get the inserted values
    SELECT @Username = inserted.trainer_username,
           @Action = 'Insert in Trainer’s DietPlan',
           @Timestamp = GETDATE(),
           @Details = 'New workout added: wpid=' + CAST(inserted.dietid AS VARCHAR(10)) + ', purpose=' + inserted.purpose + ', type of meal=' + inserted.type
    FROM inserted;

    -- Insert into audit trail
    INSERT INTO audit_trail (username, action, timestamp, details)
    VALUES (@Username, @Action, @Timestamp, @Details);
END;

--Trigger 18
CREATE TRIGGER MemberUpdateFeedback
ON feedback
AFTER UPDATE
AS
BEGIN
    IF UPDATE(stars) 
    BEGIN
        INSERT INTO audit_trail (username, action, timestamp, details)
        SELECT inserted.member_username, 'Member updated feedback', GETDATE(), 'Member updated Trainer ' + inserted.trainer_username + 'stars from ' + cast(deleted.stars as varchar(10)) + ' to ' + cast(inserted.stars as varchar(10))
        FROM inserted, deleted;
    END
END;

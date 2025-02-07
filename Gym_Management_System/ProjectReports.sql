--Project Reports

--report1
SELECT m.username AS Member_Username,
       m.Fname AS Member_FirstName,
       m.Lname AS Member_LastName,
       m.dob AS Member_DateOfBirth,
       m.gender AS Member_Gender,
       m.email AS Member_Email,
       g.gym_name AS Gym_Name,
       t.username AS Trainer_Username
FROM MemberTable m JOIN Gym g ON m.gymid = g.gymid
JOIN TrainerGym tg ON m.gymid = tg.gymid
JOIN Trainer t ON tg.trainer_username = t.username
WHERE g.gymid = 1 AND t.username = 'trainer1';

--report2
SELECT m.username AS Member_Username,
       m.Fname AS Member_FirstName,
       m.Lname AS Member_LastName,
       m.dob AS Member_DateOfBirth,
       m.gender AS Member_Gender,
       m.email AS Member_Email,
       g.gym_name AS Gym_Name,
       dm.purpose AS DietPlan_Purpose,
       dm.noofmeals AS DietPlan_NumberOfMeals,
       dm.type AS DietPlan_Type
FROM MemberTable m JOIN Gym g ON m.gymid = g.gymid
JOIN SelectTable_M stm ON m.username = stm.member_username
JOIN DietPlan_M dm ON stm.dietid = dm.dietid
WHERE g.gymid = 1 AND dm.dietid = 3;

--report3
SELECT m.username AS Member_Username,
       m.Fname AS Member_FirstName,
       m.Lname AS Member_LastName,
       m.dob AS Member_DateOfBirth,
       m.gender AS Member_Gender,
       m.email AS Member_Email,
       dm.purpose AS DietPlan_Purpose,
       dm.noofmeals AS DietPlan_NumberOfMeals,
       dm.type AS DietPlan_Type
FROM MemberTable m JOIN SelectTable_T stm ON m.username = stm.member_username
JOIN DietPlan_T dm ON stm.dietid = dm.dietid
WHERE dm.trainer_username = 'trainer1' AND dm.dietid = 1;

--report4 
SELECT Machine.name AS MachineName, COUNT(DISTINCT MemberTable.username) AS MembersUsingMachine
FROM Gym JOIN MemberTable ON Gym.gymid = MemberTable.gymid
JOIN WorkOutPlan_M ON MemberTable.username = WorkOutPlan_M.member_username
JOIN ContainTable_M ON WorkOutPlan_M.wpid = ContainTable_M.wpid
JOIN Exercise ON ContainTable_M.exid = Exercise.exid
JOIN Machine ON Exercise.macid = Machine.macid
WHERE Gym.gymid = 1 -- Specify the name of the specific gym
AND ContainTable_M.day = 'Monday' -- Specify the specific day
GROUP BY Machine.name;

--report5 
--member diet plan
SELECT DietPlan_M.dietid, DietPlan_M.purpose, (Meal.proteins * 4 + Meal.carbs * 4 +  Meal.fats * 9) as Calories
FROM DietPlan_M JOIN Have_M ON DietPlan_M.dietid = Have_M.dietid
JOIN Meal ON Have_M.mealid = Meal.mealid
WHERE Meal.name = 'Breakfast' 
AND (Meal.proteins * 4 + Meal.carbs * 4 +  Meal.fats * 9)  < 500 
--trainer diet plan
SELECT DietPlan_T.dietid, DietPlan_T.purpose, (Meal.proteins * 4 + Meal.carbs * 4 +  Meal.fats * 9) as Calories
FROM DietPlan_T JOIN Have_T ON DietPlan_T.dietid = Have_T.dietid
JOIN Meal ON Have_T.mealid = Meal.mealid
WHERE Meal.name = 'Breakfast' 
AND (Meal.proteins * 4 + Meal.carbs * 4 +  Meal.fats * 9)  < 500

--report6 
--member diet plan
SELECT DietPlan_M.dietid, DietPlan_M.purpose, SUM(Meal.carbs) as totalCarbs
FROM DietPlan_M JOIN Have_M ON DietPlan_M.dietid = Have_M.dietid
JOIN Meal ON Have_M.mealid = Meal.mealid
GROUP BY DietPlan_M.dietid, DietPlan_M.purpose
HAVING SUM(Meal.carbs) < 300;
--trainer diet plan
SELECT DietPlan_T.dietid, DietPlan_T.purpose, SUM(Meal.carbs) as totalCarbs
FROM DietPlan_T JOIN Have_T ON DietPlan_T.dietid = Have_T.dietid
JOIN Meal ON Have_T.mealid = Meal.mealid
GROUP BY DietPlan_T.dietid, DietPlan_T.purpose
HAVING SUM(Meal.carbs) < 300;

--report8
--member diet plan
SELECT DISTINCT DietPlan_M.dietid, DietPlan_M.purpose
FROM DietPlan_M JOIN Have_M ON DietPlan_M.dietid = Have_M.dietid
JOIN Meal ON Have_M.mealid = Meal.mealid
WHERE Meal.potential_allergies IS NULL OR Meal.potential_allergies != 'Peanuts'
--TRAINER diet plan
SELECT DISTINCT DietPlan_T.dietid, DietPlan_T.purpose
FROM DietPlan_T JOIN Have_T ON DietPlan_T.dietid = Have_T.dietid
JOIN Meal ON Have_T.mealid = Meal.mealid
WHERE Meal.potential_allergies IS NULL OR Meal.potential_allergies != 'Peanuts'

--TRAINER ADDITIONAL REPORTS

--List of Trainers with Their Assigned Gyms, Specialization Areas, and Experience Levels:
SELECT T.username, T.Fname, T.Lname, TG.gymid, G.gym_name, T.specialty_areas, T.experience, T.qualifications
FROM Trainer T JOIN TrainerGym TG ON T.username = TG.trainer_username
JOIN Gym G ON TG.gymid = G.gymid
where T.status='accepted';

--List of Trainers and Their Sessions with Members and Session Duration--
SELECT T.username AS trainer_username, M.username AS member_username, S.sessionid, S.type, S.date, S.time, S.duration    
FROM Trainer T JOIN SessionTable S ON T.username = S.trainer_username
JOIN MemberTable M ON S.member_username = M.username
where S.status='booked'

--List of Trainers and Their Feedback Ratings--
SELECT T.username, T.Fname, T.Lname, AVG(F.stars) AS avg_rating
FROM Trainer T JOIN Feedback F ON T.username = F.trainer_username
GROUP BY T.username, T.Fname, T.Lname;

--List of trainers along with session counts:
SELECT T.username, T.Fname, T.Lname, COUNT(S.sessionid) AS session_count
FROM Trainer T JOIN SessionTable S ON T.username = S.trainer_username
GROUP BY T.Fname, T.Lname, T.username
Order by session_count DESC;

--Details of trainers with their specialization areas and the total number of members they have trained:
SELECT T.username, T.Fname, T.Lname, T.specialty_areas, COUNT(DISTINCT S.member_username) AS member_count
FROM Trainer T JOIN SessionTable S ON T.username = S.trainer_username
GROUP BY T.username, T.Fname, T.Lname, T.specialty_areas
Order by member_count DESC;

--Details of trainers and their sessions with the highest and lowest durations:
SELECT T.username, T.Fname, T.Lname, MAX(S.duration) AS max_session_duration, MIN(S.duration) AS min_session_duration
FROM Trainer T JOIN SessionTable S ON T.username = S.trainer_username
where T.status='accepted'
GROUP BY T.username, T.Fname, T.Lname
order by max_session_duration, min_session_duration;

--GYM/GYMOWNER ADDITIONAL REPORTS

--report1
SELECT m.username
FROM MemberTable m
INNER JOIN Gym g ON m.gymid = g.gymid
WHERE g.gymid = 2;

--report2
SELECT t.username, t.Fname, t.Lname, t.qualifications, t.experience, t.specialty_areas
FROM Trainer t
JOIN TrainerGym tg ON t.username = tg.trainer_username
WHERE tg.gymid = 1;

--report3
SELECT g.gymid, g.gym_name, mt.username, mt.typeofmembership, mt.duration
FROM Gym g
JOIN MemberTable mt ON g.gymid = mt.gymid;

--report4
SELECT go.username, go.Fname, go.Lname, COUNT(DISTINCT g.gymid) AS num_gyms, COUNT(t.username) AS total_trainers
FROM GymOwner go
JOIN Gym g ON go.username = g.go_username
LEFT JOIN TrainerGym tg ON g.gymid = tg.gymid
LEFT JOIN Trainer t ON tg.trainer_username = t.username
GROUP BY go.username, go.Fname, go.Lname;

--report5
SELECT 
    g.gym_name, 
    m.status, 
    COUNT(m.username) AS MemberCount
FROM 
    Gym g
JOIN 
    MemberTable m ON g.gymid = m.gymid
GROUP BY 
    g.gym_name, m.status;

--report6
SELECT 
    g.gym_name, 
    mt.typeofmembership, 
    COUNT(*) AS Count
FROM 
    MemberTable mt
JOIN 
    Gym g ON mt.gymid = g.gymid
GROUP BY 
    g.gym_name, mt.typeofmembership
ORDER BY 
    g.gym_name, COUNT(*) DESC;

--report7
SELECT 
    m.username, 
    m.Fname, 
    m.Lname
FROM 
    MemberTable m
WHERE 
    m.gymid = 9 AND m.status = 'revoke'; 


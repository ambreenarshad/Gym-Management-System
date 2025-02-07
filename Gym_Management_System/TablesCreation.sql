--Admin
create table AdminTable(
username varchar(50) primary key not null,
password varchar(50) );

--GYM OWNER:
create table GymOwner(
username varchar(50) not null primary key,
password varchar(50) not null,
Fname varchar(50) not null,
Lname varchar(50) not null,
dob varchar(100) not null,
gender varchar(100) not null,
email varchar(100) not null,
status varchar(20) not null );

--GYM
create table Gym(
gymid int identity(1, 1) not null primary key,
gym_name varchar(100) not null,
go_username varchar(50) foreign key references GymOwner(username),
admin_username varchar(50) foreign key references AdminTable(username),
status varchar(20) not null );

--Member
create table MemberTable(
username varchar(50) not null primary key,
password varchar(50) not null,
Fname varchar(50) not null,
Lname varchar(50) not null,
dob varchar(100) not null,
gender varchar(100) not null,
email varchar(100) not null,
status varchar(20) not null,
gymid int foreign key references Gym(gymid),
typeofmembership varchar(20),
duration int );

--Trainer
CREATE TABLE Trainer(
    username VARCHAR(50) primary key ,
    password VARCHAR(50) NOT NULL,
    Fname VARCHAR(50) NOT NULL,
    Lname VARCHAR(50) NOT NULL,
    dob VARCHAR(100) NOT NULL,
    gender VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    qualifications VARCHAR(255),
    experience varchar(255),
    specialty_areas VARCHAR(255),
    status varchar(20)	not null );

--Trainer’s Workout plan
create table WorkOutPlan_T(
wpid int identity(1, 1) not null primary key,
goal varchar(100) not null,
experiencelevel varchar(50) not null,
trainer_username varchar(50) foreign key references Trainer(username) );

--Member’s Workout plan
create table WorkOutPlan_M(
wpid int identity(1, 1) not null primary key,
goal varchar(100) not null,
experiencelevel varchar(50) not null,
member_username varchar(50) foreign key references MemberTable(username) );

--Trainer’s DietPlan
create table DietPlan_T(
dietid int identity(1, 1) not null primary key,
purpose varchar(100) not null,
noofmeals int not null,
type varchar(100) not null,
trainer_username varchar(50) foreign key references Trainer(username) );

--Member’s DietPlan
create table DietPlan_M(
dietid int identity(1, 1) not null primary key,
purpose varchar(100) not null,
noofmeals int not null,
type varchar(100) not null,
member_username varchar(50) foreign key references MemberTable(username) );

--Feedback
create table feedback(
feedbackid int identity(1, 1) not null primary key,
stars int not null,
member_username varchar(50) foreign key references MemberTable(username),
trainer_username varchar(50) foreign key references Trainer(username) );

--SessionTable
create table SessionTable(
sessionid int identity(1,1) not null primary key,
date varchar(100) not null,
time int not null,
duration int not null, 
type varchar(100), 
member_username varchar(50) foreign key references MemberTable(username),
trainer_username varchar(50) foreign key references Trainer(username),
status varchar(20) not NULL );

--Machine
Create table Machine(
macid int identity(1,1) primary key not null,
name varchar(100) );

--Exercise
Create table Exercise(
exid int identity(1,1) primary key not null,
name varchar(100),
muscle_target varchar (100),
macid int foreign key references Machine(macid) );

--Meal
Create table Meal(
mealid int identity(1,1) primary key not null,
name varchar(100),
proteins decimal (10,3), 
carbs decimal (10,3),
fibers decimal (10,3),
fats decimal (10,3),
potential_allergies varchar (100) );

--Explore Member’s Workout plan
create table Explore_M(
member_username varchar(50) foreign key references MemberTable(username),
wpid int foreign key references WorkOutPlan_M(wpid),
PRIMARY KEY (member_username, wpid) );

--Explore Trainer’s Workout plan
create table Explore_T(
member_username varchar(50) foreign key references MemberTable(username),
wpid int foreign key references WorkOutPlan_T(wpid),
PRIMARY KEY (member_username, wpid) );

--Select Member’s dietplan
create table SelectTable_M(
member_username varchar(50) foreign key references MemberTable(username),
dietid int foreign key references DietPlan_M(dietid),
PRIMARY KEY (member_username, dietid) );

--Select Trainer’s dietplan
create table SelectTable_T(
member_username varchar(50) foreign key references MemberTable(username),
dietid int foreign key references DietPlan_T(dietid),
PRIMARY KEY (member_username, dietid) );

--have 	Member
create table Have_M(
mealid int foreign key references Meal(mealid),
dietid int foreign key references DietPlan_M(dietid),
PRIMARY KEY (mealid, dietid) );

--have Trainer
create table Have_T(
mealid int foreign key references Meal(mealid),
dietid int foreign key references DietPlan_T(dietid),
PRIMARY KEY (mealid, dietid) );

--contains Member
create table ContainTable_M(
wpid int foreign key references WorkOutPlan_M(wpid),
exid int foreign key references Exercise(exid),
sets int,
reps int,
restintervals int,
day varchar(50),
PRIMARY KEY (wpid, exid) );

--contains Trainer
create table ContainTable_T(
wpid int foreign key references WorkOutPlan_T(wpid),
exid int foreign key references Exercise(exid),
sets int,
reps int,
restintervals int,
day varchar(50),
PRIMARY KEY (wpid, exid) );

--trainergym 
CREATE TABLE TrainerGym(
    gymid INT NOT NULL FOREIGN KEY REFERENCES Gym(gymid),
    trainer_username VARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Trainer(username),
status varchar (20) not null,
 PRIMARY KEY (gymid, trainer_username) );

--TrainerReport
CREATE TABLE TrainerReport(
    report_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    trainer_username VARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Trainer(username),
    gymid INT NOT NULL FOREIGN KEY REFERENCES Gym(gymid),
    performance_rating DECIMAL(3,2),
    report_details TEXT );

--Trainer Removal
CREATE TABLE TrainerRemovals(
    removal_id INT IDENTITY(1,1) PRIMARY KEY,
    gym_id INT NOT NULL,
    username VARCHAR(50) NOT NULL,
    removed_by VARCHAR(50) NOT NULL,  -- Username of the gym owner who removed the trainer
    removal_date DATE NOT NULL DEFAULT GETDATE(),  -- When the trainer was removed
    reason TEXT,  -- Optional field to describe why the trainer was removed
    FOREIGN KEY (gym_id) REFERENCES Gym(gymid),
    FOREIGN KEY (username) REFERENCES Trainer(username) );

--machine
CREATE TABLE GymMachine(
    gym_id INT NOT NULL,
    machine_id INT NOT NULL,
    quantity INT DEFAULT 1,  
    PRIMARY KEY (gym_id, machine_id),
    FOREIGN KEY (gym_id) REFERENCES Gym(gymid),
    FOREIGN KEY (machine_id) REFERENCES Machine(macid) );

-- MariaDB dump 10.19  Distrib 10.6.11-MariaDB, for debian-linux-gnu (x86_64)
--
-- Host: localhost    Database: project
-- ------------------------------------------------------
-- Server version	10.6.11-MariaDB-0ubuntu0.22.04.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Admins`
--

DROP TABLE IF EXISTS `Admins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Admins` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Email` varchar(100) NOT NULL,
  `PasswordHash` varchar(100) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Salt` blob NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Admins`
--

LOCK TABLES `Admins` WRITE;
/*!40000 ALTER TABLE `Admins` DISABLE KEYS */;
INSERT INTO `Admins` VALUES (1,'brunomircevski@protonmail.com','qpXGtREOyyuayC+MTzW9m8SrZawmye6oW7w/Vt5CjqM=','bruno','#‘Ä‡Ë§+U∞D'),(2,'admin@admin','VYSuA9Stdd+Rv9kl7N2IcWjs/4eAtGs9vEDq2DUHsK8=','Admin2','∆êA^&⁄…¬\\Ω\Z˜œ');
/*!40000 ALTER TABLE `Admins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Announcements`
--

DROP TABLE IF EXISTS `Announcements`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Announcements` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(100) NOT NULL,
  `Content` varchar(5000) NOT NULL,
  `TeacherId` int(11) DEFAULT NULL,
  `Date` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Announcements_TeacherId` (`TeacherId`),
  CONSTRAINT `FK_Announcements_Teachers_TeacherId` FOREIGN KEY (`TeacherId`) REFERENCES `Teachers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Announcements`
--

LOCK TABLES `Announcements` WRITE;
/*!40000 ALTER TABLE `Announcements` DISABLE KEYS */;
INSERT INTO `Announcements` VALUES (1,'Lorem ipsum dolor sit amet, consectetur adipiscing elit.','Nam gravida venenatis risus, ut vulputate enim iaculis vitae. Quisque elementum urna nisi, et tincidunt justo auctor volutpat. Mauris arcu justo, commodo non arcu vel, commodo egestas dolor. Maecenas ex justo, tristique non aliquam et, porta nec ante. Maecenas aliquam pretium lobortis. Donec in sem dolor. Maecenas mi sapien, maximus vel erat euismod, fermentum laoreet odio. In consectetur magna nibh, id gravida sem finibus non. Integer ligula magna, cursus pretium urna non, aliquet feugiat lacus. Fusce rutrum luctus sem nec venenatis. In commodo elit vel magna pharetra, eu rhoncus purus convallis. Etiam ullamcorper egestas congue. Fusce pretium elementum enim, ut lobortis erat fermentum nec. Aenean fermentum non enim at tristique. Aenean vehicula mauris mauris.\r\n\r\nProin facilisis egestas dui, nec egestas est. Integer finibus velit vitae ante pulvinar pharetra. Donec feugiat at justo quis aliquet. Nam mollis purus a libero viverra porta. Nam nec venenatis nulla. In hac habitasse platea dictumst. Quisque et auctor massa, facilisis vehicula sapien. Vestibulum accumsan scelerisque risus, eu vehicula magna lacinia vitae. Quisque eu mauris in quam fermentum porta. Sed commodo vel purus non tincidunt.\r\n\r\n',7,'2022-11-11 11:06:09.840430'),(2,'Pellentesque a ornare arcu, tincidunt tempor erat. Ut nisl lectus, tincidunt quis dolor nec, ornare ','Sed scelerisque est sit amet leo tincidunt, sit amet suscipit neque feugiat. Pellentesque iaculis, sem at dapibus congue, erat felis imperdiet turpis, et bibendum sapien risus sit amet nisi. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Curabitur ac commodo tortor, vitae mollis mauris. Proin tempor ipsum nec turpis efficitur, ac facilisis dui mollis. Phasellus ante lorem, hendrerit ut augue hendrerit, maximus condimentum nisl. Vestibulum convallis tincidunt eros eu sollicitudin. Nunc scelerisque tempor nunc, a scelerisque libero mattis id. Nunc non congue massa, sodales euismod metus. Donec at libero vitae nibh lobortis rutrum at et nisl. Praesent sagittis odio a leo malesuada, quis porta massa hendrerit. Sed pharetra risus ac blandit ullamcorper. Aliquam a tellus sit amet sem congue rhoncus faucibus nec magna. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Mauris in magna sed diam elementum ultrices posuere ac purus.',7,'2022-11-11 11:08:32.571901'),(3,'Og≈Çoszenie admin','admina og≈Çoszeniee nfuiods nioufn dio jnoisfdnoi fjos df\r\n',NULL,'2022-11-11 11:31:14.959651'),(5,'nowe kolejne','adminA',NULL,'2022-11-11 12:17:21.155058'),(7,'nauczyciel 4 og≈Çoszenie','tre≈õƒá',6,'2022-11-11 15:56:40.455022');
/*!40000 ALTER TABLE `Announcements` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Contents`
--

DROP TABLE IF EXISTS `Contents`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Contents` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `SubjectId` int(11) NOT NULL,
  `Date` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  `Name` varchar(100) NOT NULL,
  `Link` varchar(1000) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Contents_SubjectId` (`SubjectId`),
  CONSTRAINT `FK_Contents_Subjects_SubjectId` FOREIGN KEY (`SubjectId`) REFERENCES `Subjects` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Contents`
--

LOCK TABLES `Contents` WRITE;
/*!40000 ALTER TABLE `Contents` DISABLE KEYS */;
INSERT INTO `Contents` VALUES (16,15,'2023-01-20 15:34:20.728750','egzaminy','https://ceze.wi.pb.edu.pl/'),(23,15,'2023-01-20 15:50:10.221566','cez','https://cez2.wi.pb.edu.pl/');
/*!40000 ALTER TABLE `Contents` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Grades`
--

DROP TABLE IF EXISTS `Grades`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Grades` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Value` float NOT NULL,
  `Weight` int(11) NOT NULL,
  `StudentId` int(11) NOT NULL,
  `SubjectId` int(11) NOT NULL,
  `Date` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Id`),
  KEY `IX_Grades_StudentId` (`StudentId`),
  KEY `IX_Grades_SubjectId` (`SubjectId`),
  CONSTRAINT `FK_Grades_Students_StudentId` FOREIGN KEY (`StudentId`) REFERENCES `Students` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Grades_Subjects_SubjectId` FOREIGN KEY (`SubjectId`) REFERENCES `Subjects` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Grades`
--

LOCK TABLES `Grades` WRITE;
/*!40000 ALTER TABLE `Grades` DISABLE KEYS */;
INSERT INTO `Grades` VALUES (1,5,1,7,8,'0001-01-01 00:00:00.000000'),(2,4,1,7,8,'0001-01-01 00:00:00.000000'),(3,3,1,7,8,'0001-01-01 00:00:00.000000'),(4,3,1,7,8,'0001-01-01 00:00:00.000000'),(5,4,1,7,8,'0001-01-01 00:00:00.000000'),(6,4,2,6,8,'0001-01-01 00:00:00.000000'),(8,2.25,6,6,8,'0001-01-01 00:00:00.000000'),(9,4.25,6,6,8,'2022-10-28 15:45:35.000000'),(10,4.25,6,3,8,'2022-10-28 15:49:09.000000'),(12,3.25,5,4,15,'0001-01-01 00:00:00.000000'),(14,3,10,3,15,'0001-01-01 00:00:00.000000'),(15,6,10,7,15,'0001-01-01 00:00:00.000000'),(17,1.75,1,6,15,'0001-01-01 00:00:00.000000'),(18,1,1,3,15,'2022-10-31 10:52:21.064214'),(19,5.25,10,3,15,'2022-10-31 10:52:28.729930'),(20,3,10,7,15,'2022-10-31 10:52:34.951027'),(21,4.25,6,1,15,'2022-10-31 10:53:08.401170'),(22,2.75,3,1,15,'2022-10-31 10:53:13.018042'),(23,3,1,3,15,'2022-10-31 13:04:29.892188'),(24,5,1,7,15,'2022-10-31 13:04:39.838648'),(25,6,1,4,15,'2022-10-31 13:05:05.583593'),(27,4,1,1,15,'2022-10-31 13:05:27.804528'),(28,1,1,8,15,'2022-10-31 14:14:12.521416'),(29,6,1,1,15,'2022-10-31 14:14:20.434558'),(30,2.25,1,1,2,'2022-10-31 14:15:11.487373'),(31,2.75,1,1,2,'2022-10-31 14:15:13.260916'),(32,6,9,1,14,'2022-10-31 14:15:23.469681'),(33,1,1,8,14,'2022-10-31 14:15:25.966296');
/*!40000 ALTER TABLE `Grades` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `GroupSubject`
--

DROP TABLE IF EXISTS `GroupSubject`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `GroupSubject` (
  `GroupsId` int(11) NOT NULL,
  `SubjectsId` int(11) NOT NULL,
  PRIMARY KEY (`GroupsId`,`SubjectsId`),
  KEY `IX_GroupSubject_SubjectsId` (`SubjectsId`),
  CONSTRAINT `FK_GroupSubject_Groups_GroupsId` FOREIGN KEY (`GroupsId`) REFERENCES `Groups` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_GroupSubject_Subjects_SubjectsId` FOREIGN KEY (`SubjectsId`) REFERENCES `Subjects` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `GroupSubject`
--

LOCK TABLES `GroupSubject` WRITE;
/*!40000 ALTER TABLE `GroupSubject` DISABLE KEYS */;
INSERT INTO `GroupSubject` VALUES (2,14),(8,2),(8,3),(8,8),(8,14),(8,15),(12,2),(12,14),(14,2),(14,8),(14,14),(14,15);
/*!40000 ALTER TABLE `GroupSubject` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Groups`
--

DROP TABLE IF EXISTS `Groups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Groups` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(5) NOT NULL,
  `TeacherId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Groups_TeacherId` (`TeacherId`),
  CONSTRAINT `FK_Groups_Teachers_TeacherId` FOREIGN KEY (`TeacherId`) REFERENCES `Teachers` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Groups`
--

LOCK TABLES `Groups` WRITE;
/*!40000 ALTER TABLE `Groups` DISABLE KEYS */;
INSERT INTO `Groups` VALUES (2,'2a',2),(8,'3a',4),(12,'4a',6),(14,'5x',7);
/*!40000 ALTER TABLE `Groups` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Messages`
--

DROP TABLE IF EXISTS `Messages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Messages` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Content` varchar(5000) NOT NULL,
  `GroupId` int(11) NOT NULL,
  `TeacherId` int(11) NOT NULL,
  `Date` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Messages_GroupId` (`GroupId`),
  KEY `IX_Messages_TeacherId` (`TeacherId`),
  CONSTRAINT `FK_Messages_Groups_GroupId` FOREIGN KEY (`GroupId`) REFERENCES `Groups` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Messages_Teachers_TeacherId` FOREIGN KEY (`TeacherId`) REFERENCES `Teachers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Messages`
--

LOCK TABLES `Messages` WRITE;
/*!40000 ALTER TABLE `Messages` DISABLE KEYS */;
INSERT INTO `Messages` VALUES (1,'wiadomo≈õƒá¬†to klasy 5xssssssssssssssssssssssssssssssssssss',14,7,'2022-11-11 15:35:29.214083'),(2,'nie bedzie lekcji 3a',8,7,'2022-11-11 15:36:08.723458'),(4,'test',14,6,'2022-11-11 15:56:47.907727'),(5,'tessds',8,6,'2022-11-11 15:56:52.901898');
/*!40000 ALTER TABLE `Messages` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Parents`
--

DROP TABLE IF EXISTS `Parents`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Parents` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Email` varchar(100) NOT NULL,
  `PasswordHash` varchar(100) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Salt` blob NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Parents`
--

LOCK TABLES `Parents` WRITE;
/*!40000 ALTER TABLE `Parents` DISABLE KEYS */;
INSERT INTO `Parents` VALUES (1,'123@123','/MVMQobcmlPbtrR3xIiVL6FMLMM/B1194gTGoeNxSgo=','Rodzic1','&À(∫ƒw2¨˘˝Ú´•}2:'),(2,'124@123','QQ47OZRfszcVwr9ldf0nOqJAyS/03goST1Aq6G4j3GY=','Rodzic2','√u‘( 4	zª“_Ã÷!t'),(4,'rodzic@r','cL083EEaMZPPHnufTOrR/ILTkcJWtF/wAAqAqPWo7NE=','Rodzic3','X„#´ÂØ9vµ\r±Í');
/*!40000 ALTER TABLE `Parents` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Students`
--

DROP TABLE IF EXISTS `Students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Students` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Email` varchar(100) NOT NULL,
  `PasswordHash` varchar(100) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `ParentId` int(11) NOT NULL DEFAULT 0,
  `Salt` blob NOT NULL,
  `GroupId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Students_ParentId` (`ParentId`),
  KEY `IX_Students_GroupId` (`GroupId`),
  CONSTRAINT `FK_Students_Groups_GroupId` FOREIGN KEY (`GroupId`) REFERENCES `Groups` (`Id`) ON DELETE SET NULL,
  CONSTRAINT `FK_Students_Parents_ParentId` FOREIGN KEY (`ParentId`) REFERENCES `Parents` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Students`
--

LOCK TABLES `Students` WRITE;
/*!40000 ALTER TABLE `Students` DISABLE KEYS */;
INSERT INTO `Students` VALUES (1,'uczen1@mail','mGOUF3v7cOx3OaTxbnZxL3ptrMBJyATioX3n4PVnaxM=','Ucze≈Ñ1r1',1,'ô´:\Z øÖ!Nwª9s0',8),(2,'uczen2@mail','P+9c79oK/K14DQIhHR6M5Uv83n3BrHbuuJUDCyOmT8g=','Ucze≈Ñ2r2',2,'Îp\rQ4%Cã[_mXÌ·÷',2),(3,'uczen3@mail','7vrTSA+PcF3QHKJCt3uVSk7r7vlIafK1Lhk7xK6w494=','Ucze≈Ñ3r2',2,'ÇÏø4ÂÏ 302Üß™·',14),(4,'uczen4@mail','ab5NO5jTQXJhXYmWf+IIGAUVPgZRD8B7+XK3EHqeUQ4=','Ucze≈Ñ4r2',2,'îÜÿ\r€ˆ¢pañ£‰n«¥',14),(6,'uczen@a','29UUk8hcy+AjDsF7FmgX7GvRk6r2mo96tTp1tnag8rc=','12345',2,'VÖ^jÈMn¯-ﬁdÅ`6;h',14),(7,'uuuu@aaa','XyLkIApC4KqqQxhGKd/Mud6jjxpv2ihxxbZUnYUETKE=','ucze≈Ñ12121',2,'∏z£Ë √dæÜΩñDˇ¡',14),(8,'uczen5@uczen','TivgHfdguVQuEE4e9QYCPxUn5Cnq3E0QJJEDEKi86R8=','uczen5',1,'˝≥çØ˜‹Üß]C¸Y6Sñ/',8),(9,'uczen6@u','DhSVTT0ZkmrsDlKcCUXbRTyqoY68TYU4Rn64CAPADGM=','uczen6',4,'#K)OTâH\"π…ïˆL',14),(10,'uczen7@u','BBG1J7850DytRqd61WXfj6oiWaLred0wkshaS2M6AoQ=','uczen7',4,'eïÑA ÅùËMM?‡<é',NULL);
/*!40000 ALTER TABLE `Students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Subjects`
--

DROP TABLE IF EXISTS `Subjects`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Subjects` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `TeacherId` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `IX_Subjects_TeacherId` (`TeacherId`),
  CONSTRAINT `FK_Subjects_Teachers_TeacherId` FOREIGN KEY (`TeacherId`) REFERENCES `Teachers` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Subjects`
--

LOCK TABLES `Subjects` WRITE;
/*!40000 ALTER TABLE `Subjects` DISABLE KEYS */;
INSERT INTO `Subjects` VALUES (2,'Biologia',1),(3,'Matematyka',2),(8,'Fizyka',4),(14,'Chemia',1),(15,'J. polski',7);
/*!40000 ALTER TABLE `Subjects` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Teachers`
--

DROP TABLE IF EXISTS `Teachers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Teachers` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Email` varchar(100) NOT NULL,
  `PasswordHash` varchar(100) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Salt` blob NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Teachers`
--

LOCK TABLES `Teachers` WRITE;
/*!40000 ALTER TABLE `Teachers` DISABLE KEYS */;
INSERT INTO `Teachers` VALUES (1,'nauczyciel@mail','WSHZQJdfuann/AD3yAl4IFSzs0PInFbgJsLG2NizJqI=','Nauczyciel1','ÚN\r\'ºÿ«∂º≥DçË'),(2,'nauczyciel2@mail','3Rjx/nhuq+AwWa6zmMpAOAWc4I/AUveJPI537biYuqo=','Nauczyciel2','¥∞HçmÚúºM–ˆß\''),(4,'trzeci@mail','BneZVs8sf1XT34OmDHiblNlmrbXAO3xkopJA6q1quz4=','Nauczyciel3','Ù¬˜LååÏ»≈Ω’sú(D'),(6,'n4@email','pHEfcZvvFsFXwmRg/VhZIreK+/4lqVtpSckzprEdZ1M=','nauczyciel4','§S¢+∞ÆÆÇ;^oß˙Ä'),(7,'nauczyciel@n','ZosxeDxu69WVYf11NPeD8fMLGOQD+J153q/3o/3XofY=','nauczycielDoLogowania',';’\nfa5SzBeïÔù');
/*!40000 ALTER TABLE `Teachers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` VALUES ('20221111102235_InitialCreate','6.0.10'),('20221111102519_NullableTeacherAnnoucments','6.0.10'),('20230120132011_Content','6.0.10');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-01-20 15:56:14

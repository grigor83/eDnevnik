-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: e_dnevnik
-- ------------------------------------------------------
-- Server version	8.0.35

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Dumping data for table `izostao`
--

LOCK TABLES `izostao` WRITE;
/*!40000 ALTER TABLE `izostao` DISABLE KEYS */;
INSERT INTO `izostao` VALUES (1,'2023-12-13',1,NULL,'opravdan');
/*!40000 ALTER TABLE `izostao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `kabinet`
--

LOCK TABLES `kabinet` WRITE;
/*!40000 ALTER TABLE `kabinet` DISABLE KEYS */;
INSERT INTO `kabinet` VALUES (23),(201),(1010);
/*!40000 ALTER TABLE `kabinet` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `korisnik`
--

LOCK TABLES `korisnik` WRITE;
/*!40000 ALTER TABLE `korisnik` DISABLE KEYS */;
INSERT INTO `korisnik` VALUES (1,'Marko Marković','admin','admin','administrator'),(2,'Ivo Andrić','jedan','jedan','nastavnik'),(3,'Alan Tjuring','dva','dva','nastavnik'),(4,'Branko Ćopić','3','3','nastavnik'),(5,'Ivana Španović','4','4','nastavnik'),(6,'Nikola Tesla','5','5','nastavnik'),(7,'Maja Gojković','6','6','roditelj'),(8,'Jovan Jovanović','7','7','roditelj'),(9,'Đuro Đurić','8','8','roditelj');
/*!40000 ALTER TABLE `korisnik` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `pohađa`
--

LOCK TABLES `pohađa` WRITE;
/*!40000 ALTER TABLE `pohađa` DISABLE KEYS */;
INSERT INTO `pohađa` VALUES (7,3,'a','2023/24'),(8,3,'a','2023/24'),(9,3,'a','2023/24'),(10,3,'b','2023/24'),(11,3,'b','2023/24'),(12,3,'b','2023/24'),(1,4,'a','2023/24'),(2,4,'a','2023/24'),(3,4,'a','2023/24'),(4,4,'b','2023/24'),(5,4,'b','2023/24'),(6,4,'b','2023/24');
/*!40000 ALTER TABLE `pohađa` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `predmet`
--

LOCK TABLES `predmet` WRITE;
/*!40000 ALTER TABLE `predmet` DISABLE KEYS */;
INSERT INTO `predmet` VALUES ('Fizičko vaspitanje'),('Matematika'),('Osnove elektrotehnike'),('Srpski jezik');
/*!40000 ALTER TABLE `predmet` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `predmet_kabinet`
--

LOCK TABLES `predmet_kabinet` WRITE;
/*!40000 ALTER TABLE `predmet_kabinet` DISABLE KEYS */;
INSERT INTO `predmet_kabinet` VALUES ('Srpski jezik',201),('Fizičko vaspitanje',1010);
/*!40000 ALTER TABLE `predmet_kabinet` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `predmet_zaključnaocjena`
--

LOCK TABLES `predmet_zaključnaocjena` WRITE;
/*!40000 ALTER TABLE `predmet_zaključnaocjena` DISABLE KEYS */;
INSERT INTO `predmet_zaključnaocjena` VALUES (12,3,'b','2023/24','Srpski jezik',2);
/*!40000 ALTER TABLE `predmet_zaključnaocjena` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `razred`
--

LOCK TABLES `razred` WRITE;
/*!40000 ALTER TABLE `razred` DISABLE KEYS */;
INSERT INTO `razred` VALUES (2,'b','2022/23',NULL),(4,'a','2023/24',NULL),(3,'a','2023/24',3),(4,'b','2023/24',4),(3,'b','2023/24',5);
/*!40000 ALTER TABLE `razred` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `roditelj_učenik`
--

LOCK TABLES `roditelj_učenik` WRITE;
/*!40000 ALTER TABLE `roditelj_učenik` DISABLE KEYS */;
INSERT INTO `roditelj_učenik` VALUES (7,2),(8,2),(9,3);
/*!40000 ALTER TABLE `roditelj_učenik` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `stručan`
--

LOCK TABLES `stručan` WRITE;
/*!40000 ALTER TABLE `stručan` DISABLE KEYS */;
INSERT INTO `stručan` VALUES (5,'Fizičko vaspitanje'),(3,'Matematika'),(6,'Osnove elektrotehnike'),(2,'Srpski jezik'),(4,'Srpski jezik');
/*!40000 ALTER TABLE `stručan` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `učenik`
--

LOCK TABLES `učenik` WRITE;
/*!40000 ALTER TABLE `učenik` DISABLE KEYS */;
INSERT INTO `učenik` VALUES (1,'Mitar Mirić'),(2,'Jovan Jovanović'),(3,'Ana Ivanović'),(4,'Dragan Stojković'),(5,'Dejan Savićević'),(6,'Robert Prosinečki'),(7,'Miloš Milošević'),(8,'Barak Bahar'),(9,'Milena Marić'),(10,'Milutin Banjac'),(11,'Vlado Lučić'),(12,'Kosta Nedeljković');
/*!40000 ALTER TABLE `učenik` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `završio`
--

LOCK TABLES `završio` WRITE;
/*!40000 ALTER TABLE `završio` DISABLE KEYS */;
INSERT INTO `završio` VALUES (12,2,'b','2022/23',5,'primjerno'),(12,3,'b','2023/24',4,'primjerno');
/*!40000 ALTER TABLE `završio` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `čas`
--

LOCK TABLES `čas` WRITE;
/*!40000 ALTER TABLE `čas` DISABLE KEYS */;
INSERT INTO `čas` VALUES ('2023-12-11',3,3,'b','2023/24','Matematika'),('2023-12-11',4,4,'b','2023/24','Matematika'),('2023-12-13',2,4,'b','2023/24','Matematika'),('2023-12-14',1,4,'a','2023/24','Matematika'),('2023-12-13',3,3,'a','2023/24','Osnove Elektrotehnike'),('2023-12-14',2,4,'a','2023/24','Osnove elektrotehnike'),('2023-12-13',1,4,'a','2023/24','Srpski jezik'),('2023-12-13',1,4,'b','2023/24','Srpski jezik'),('2023-12-13',5,4,'b','2023/24','Srpski jezik');
/*!40000 ALTER TABLE `čas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `predaje_sadrži`
--

LOCK TABLES `predaje_sadrži` WRITE;
/*!40000 ALTER TABLE `predaje_sadrži` DISABLE KEYS */;
INSERT INTO `predaje_sadrži` VALUES (3,'a','2023/24','Srpski jezik',NULL),(4,'a','2023/24','Srpski jezik',2),(3,'b','2023/24','Matematika',3),(4,'a','2023/24','Matematika',3),(4,'b','2023/24','Matematika',3),(3,'b','2023/24','Srpski jezik',4),(4,'b','2023/24','Srpski jezik',4),(4,'a','2023/24','Fizičko vaspitanje',5),(4,'b','2023/24','Osnove elektrotehnike',6);
/*!40000 ALTER TABLE `predaje_sadrži` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `ocjena`
--

LOCK TABLES `ocjena` WRITE;
/*!40000 ALTER TABLE `ocjena` DISABLE KEYS */;
INSERT INTO `ocjena` VALUES (6,4,'b','2023/24','Srpski jezik','2023-12-13',2),(10,3,'b','2023/24','Matematika','2023-12-11',5),(12,3,'b','2023/24','Matematika','2023-12-11',4);
/*!40000 ALTER TABLE `ocjena` ENABLE KEYS */;
UNLOCK TABLES;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-12-15 14:43:56

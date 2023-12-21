CREATE DATABASE  IF NOT EXISTS `e_dnevnik` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `e_dnevnik`;
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
-- Table structure for table `izostao`
--

DROP TABLE IF EXISTS `izostao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `izostao` (
  `UčenikID` int NOT NULL,
  `Datum` date NOT NULL,
  `RedniBroj` int NOT NULL,
  `Razlog` varchar(50) DEFAULT NULL,
  `Tip` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`UčenikID`,`Datum`,`RedniBroj`),
  KEY `Datum` (`Datum`,`RedniBroj`),
  CONSTRAINT `izostao_ibfk_1` FOREIGN KEY (`UčenikID`) REFERENCES `učenik` (`UčenikID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `izostao_ibfk_2` FOREIGN KEY (`Datum`, `RedniBroj`) REFERENCES `čas` (`Datum`, `RedniBroj`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `kabinet`
--

DROP TABLE IF EXISTS `kabinet`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `kabinet` (
  `BrojKabineta` int NOT NULL,
  PRIMARY KEY (`BrojKabineta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `korisnik`
--

DROP TABLE IF EXISTS `korisnik`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `korisnik` (
  `KorisnikID` int NOT NULL AUTO_INCREMENT,
  `Ime` varchar(50) NOT NULL,
  `KorisničkoIme` varchar(20) NOT NULL,
  `Lozinka` varchar(20) NOT NULL,
  `TipNaloga` varchar(20) NOT NULL,
  PRIMARY KEY (`KorisnikID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ocjena`
--

DROP TABLE IF EXISTS `ocjena`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ocjena` (
  `UčenikID` int NOT NULL,
  `Razred` int NOT NULL,
  `Odjeljenje` varchar(1) NOT NULL,
  `ŠkolskaGodina` varchar(10) NOT NULL,
  `NazivPredmeta` varchar(40) NOT NULL,
  `Datum` date NOT NULL,
  `Ocjena` int NOT NULL,
  PRIMARY KEY (`UčenikID`,`Razred`,`Odjeljenje`,`ŠkolskaGodina`,`NazivPredmeta`,`Datum`),
  KEY `Razred` (`Razred`,`Odjeljenje`,`ŠkolskaGodina`),
  KEY `NazivPredmeta` (`NazivPredmeta`),
  CONSTRAINT `ocjena_ibfk_1` FOREIGN KEY (`UčenikID`) REFERENCES `učenik` (`UčenikID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `ocjena_ibfk_2` FOREIGN KEY (`Razred`, `Odjeljenje`, `ŠkolskaGodina`) REFERENCES `razred` (`Razred`, `Odjeljenje`, `ŠkolskaGodina`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `ocjena_ibfk_3` FOREIGN KEY (`NazivPredmeta`) REFERENCES `predmet` (`NazivPredmeta`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `ocjena_chk_1` CHECK ((`Ocjena` between 1 and 5))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `unos_ocjene` BEFORE INSERT ON `ocjena` FOR EACH ROW begin
call učenik_pohađa_razred (NEW.UčenikID, NEW.Razred, NEW.Odjeljenje, NEW.ŠkolskaGodina);
call provjeri_čas(NEW.Datum, NEW.Razred, NEW.Odjeljenje, NEW.ŠkolskaGodina, NEW.NazivPredmeta);
call provjeri_prisustvo(NEW.UčenikID, NEW.Datum);
end */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `pohađa`
--

DROP TABLE IF EXISTS `pohađa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pohađa` (
  `UčenikID` int NOT NULL,
  `Razred` int NOT NULL,
  `Odjeljenje` varchar(1) NOT NULL,
  `ŠkolskaGodina` varchar(10) NOT NULL,
  PRIMARY KEY (`UčenikID`,`Razred`,`Odjeljenje`,`ŠkolskaGodina`),
  KEY `Razred` (`Razred`,`Odjeljenje`,`ŠkolskaGodina`),
  CONSTRAINT `pohađa_ibfk_1` FOREIGN KEY (`UčenikID`) REFERENCES `učenik` (`UčenikID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `pohađa_ibfk_2` FOREIGN KEY (`Razred`, `Odjeljenje`, `ŠkolskaGodina`) REFERENCES `razred` (`Razred`, `Odjeljenje`, `ŠkolskaGodina`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `predaje_sadrži`
--

DROP TABLE IF EXISTS `predaje_sadrži`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `predaje_sadrži` (
  `Razred` int NOT NULL,
  `Odjeljenje` varchar(1) NOT NULL,
  `ŠkolskaGodina` varchar(10) NOT NULL,
  `NazivPredmeta` varchar(40) NOT NULL,
  `PredavačID` int DEFAULT NULL,
  PRIMARY KEY (`Razred`,`Odjeljenje`,`ŠkolskaGodina`,`NazivPredmeta`),
  KEY `NazivPredmeta` (`NazivPredmeta`),
  KEY `PredavačID` (`PredavačID`),
  CONSTRAINT `predaje_sadrži_ibfk_1` FOREIGN KEY (`Razred`, `Odjeljenje`, `ŠkolskaGodina`) REFERENCES `razred` (`Razred`, `Odjeljenje`, `ŠkolskaGodina`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `predaje_sadrži_ibfk_2` FOREIGN KEY (`NazivPredmeta`) REFERENCES `predmet` (`NazivPredmeta`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `predaje_sadrži_ibfk_3` FOREIGN KEY (`PredavačID`) REFERENCES `korisnik` (`KorisnikID`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `odredi_predavača` BEFORE INSERT ON `predaje_sadrži` FOR EACH ROW begin
call provjeri_stručnost_nastavnika(NEW.PredavačID, NEW.NazivPredmeta);
end */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `predmet`
--

DROP TABLE IF EXISTS `predmet`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `predmet` (
  `NazivPredmeta` varchar(40) NOT NULL,
  PRIMARY KEY (`NazivPredmeta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `predmet_kabinet`
--

DROP TABLE IF EXISTS `predmet_kabinet`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `predmet_kabinet` (
  `NazivPredmeta` varchar(40) NOT NULL,
  `BrojKabineta` int NOT NULL,
  PRIMARY KEY (`NazivPredmeta`,`BrojKabineta`),
  KEY `BrojKabineta` (`BrojKabineta`),
  CONSTRAINT `predmet_kabinet_ibfk_1` FOREIGN KEY (`NazivPredmeta`) REFERENCES `predmet` (`NazivPredmeta`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `predmet_kabinet_ibfk_2` FOREIGN KEY (`BrojKabineta`) REFERENCES `kabinet` (`BrojKabineta`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `predmet_zaključnaocjena`
--

DROP TABLE IF EXISTS `predmet_zaključnaocjena`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `predmet_zaključnaocjena` (
  `UčenikID` int NOT NULL,
  `Razred` int NOT NULL,
  `Odjeljenje` varchar(1) NOT NULL,
  `ŠkolskaGodina` varchar(10) NOT NULL,
  `NazivPredmeta` varchar(40) NOT NULL,
  `ZaključnaOcjena` int NOT NULL,
  PRIMARY KEY (`UčenikID`,`Razred`,`Odjeljenje`,`ŠkolskaGodina`),
  KEY `Razred` (`Razred`,`Odjeljenje`,`ŠkolskaGodina`),
  KEY `NazivPredmeta` (`NazivPredmeta`),
  CONSTRAINT `predmet_zaključnaocjena_ibfk_1` FOREIGN KEY (`UčenikID`) REFERENCES `učenik` (`UčenikID`),
  CONSTRAINT `predmet_zaključnaocjena_ibfk_2` FOREIGN KEY (`Razred`, `Odjeljenje`, `ŠkolskaGodina`) REFERENCES `razred` (`Razred`, `Odjeljenje`, `ŠkolskaGodina`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `predmet_zaključnaocjena_ibfk_3` FOREIGN KEY (`NazivPredmeta`) REFERENCES `predmet` (`NazivPredmeta`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `razred`
--

DROP TABLE IF EXISTS `razred`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `razred` (
  `Razred` int NOT NULL,
  `Odjeljenje` varchar(1) NOT NULL,
  `ŠkolskaGodina` varchar(10) NOT NULL,
  `RazrednikID` int DEFAULT NULL,
  PRIMARY KEY (`Razred`,`Odjeljenje`,`ŠkolskaGodina`),
  KEY `RazrednikID` (`RazrednikID`),
  CONSTRAINT `razred_ibfk_1` FOREIGN KEY (`RazrednikID`) REFERENCES `korisnik` (`KorisnikID`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `roditelj_učenik`
--

DROP TABLE IF EXISTS `roditelj_učenik`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roditelj_učenik` (
  `RoditeljID` int NOT NULL,
  `UčenikID` int NOT NULL,
  PRIMARY KEY (`RoditeljID`,`UčenikID`),
  KEY `UčenikID` (`UčenikID`),
  CONSTRAINT `roditelj_učenik_ibfk_1` FOREIGN KEY (`RoditeljID`) REFERENCES `korisnik` (`KorisnikID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `roditelj_učenik_ibfk_2` FOREIGN KEY (`UčenikID`) REFERENCES `učenik` (`UčenikID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `stručan`
--

DROP TABLE IF EXISTS `stručan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stručan` (
  `NastavnikID` int NOT NULL,
  `NazivPredmeta` varchar(40) NOT NULL,
  PRIMARY KEY (`NastavnikID`,`NazivPredmeta`),
  KEY `NazivPredmeta` (`NazivPredmeta`),
  CONSTRAINT `stručan_ibfk_1` FOREIGN KEY (`NastavnikID`) REFERENCES `korisnik` (`KorisnikID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `stručan_ibfk_2` FOREIGN KEY (`NazivPredmeta`) REFERENCES `predmet` (`NazivPredmeta`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `učenik`
--

DROP TABLE IF EXISTS `učenik`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `učenik` (
  `UčenikID` int NOT NULL AUTO_INCREMENT,
  `Ime` varchar(50) NOT NULL,
  PRIMARY KEY (`UčenikID`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `završio`
--

DROP TABLE IF EXISTS `završio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `završio` (
  `UčenikID` int NOT NULL,
  `Razred` int NOT NULL,
  `Odjeljenje` varchar(1) NOT NULL,
  `ŠkolskaGodina` varchar(10) NOT NULL,
  `OpštiUspjeh` int NOT NULL,
  `Vladanje` varchar(15) NOT NULL,
  PRIMARY KEY (`UčenikID`,`Razred`,`Odjeljenje`,`ŠkolskaGodina`),
  KEY `Razred` (`Razred`,`Odjeljenje`,`ŠkolskaGodina`),
  CONSTRAINT `završio_ibfk_1` FOREIGN KEY (`UčenikID`) REFERENCES `učenik` (`UčenikID`),
  CONSTRAINT `završio_ibfk_2` FOREIGN KEY (`Razred`, `Odjeljenje`, `ŠkolskaGodina`) REFERENCES `razred` (`Razred`, `Odjeljenje`, `ŠkolskaGodina`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `čas`
--

DROP TABLE IF EXISTS `čas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `čas` (
  `Datum` date NOT NULL,
  `RedniBroj` int NOT NULL,
  `Razred` int NOT NULL,
  `Odjeljenje` varchar(1) NOT NULL,
  `ŠkolskaGodina` varchar(10) NOT NULL,
  `NazivPredmeta` varchar(40) NOT NULL,
  PRIMARY KEY (`Datum`,`RedniBroj`,`Razred`,`Odjeljenje`,`ŠkolskaGodina`),
  KEY `Razred` (`Razred`,`Odjeljenje`,`ŠkolskaGodina`),
  KEY `NazivPredmeta` (`NazivPredmeta`),
  CONSTRAINT `čas_ibfk_1` FOREIGN KEY (`Razred`, `Odjeljenje`, `ŠkolskaGodina`) REFERENCES `razred` (`Razred`, `Odjeljenje`, `ŠkolskaGodina`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `čas_ibfk_2` FOREIGN KEY (`NazivPredmeta`) REFERENCES `predmet` (`NazivPredmeta`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping events for database 'e_dnevnik'
--

--
-- Dumping routines for database 'e_dnevnik'
--
/*!50003 DROP PROCEDURE IF EXISTS `provjeri_čas` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `provjeri_čas`(in pDatum date, in pRazred int, in pOdjeljenje varchar(1), in pŠkolskaGodina varchar(10), in pNazivPredmeta varchar(40))
BEGIN
declare učenikImaČas bool default false;
declare poruka VARCHAR(255);

select count(*)>0 into učenikImaČas from čas
where pDatum=Datum and pRazred=Razred and pOdjeljenje=Odjeljenje and pŠkolskaGodina=ŠkolskaGodina and pNazivPredmeta=NazivPredmeta;

if not učenikImaČas then
	set poruka = CONCAT('Navedenog datuma učenik nema čas iz predmeta: ', pNazivPredmeta);
	SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = poruka;
end if;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `provjeri_prisustvo` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `provjeri_prisustvo`(in pID int, in pDatum date)
BEGIN
declare odsutan bool default false;
declare poruka VARCHAR(255);

select count(*)>0 into odsutan from izostao
where pID=UčenikID and pDatum=Datum;

if odsutan then
	set poruka = CONCAT('Navedenog datuma: ', pDatum,' učenik nije bio prisutan na času!');
	SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = poruka;
end if;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `provjeri_stručnost_nastavnika` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `provjeri_stručnost_nastavnika`(in pID int, in pNazivPredmeta varchar(40))
BEGIN
declare nastavnik_je_stručan bool default false;
declare poruka VARCHAR(255);

select count(*)>0 into nastavnik_je_stručan from stručan
where (pID=NastavnikID and pNazivPredmeta=NazivPredmeta) or pID is null;

if not nastavnik_je_stručan then
	set poruka = CONCAT('Nastavnik nije stručan za navedeni predmet: ', pNazivPredmeta,' i ne može ga predavati!');
	SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = poruka;
end if;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `učenik_pohađa_razred` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `učenik_pohađa_razred`(in pID int, in pRazred int, in pOdjeljenje varchar(1), in pŠkolskaGodina varchar(10))
BEGIN
declare vPohađa bool default false;
declare poruka VARCHAR(255);

select count(*)>0 into vPohađa from pohađa
where pID=UčenikID and pRazred=Razred and pOdjeljenje=Odjeljenje and pŠkolskaGodina=ŠkolskaGodina;

if not vPohađa then
	set poruka = CONCAT('Učenik ne pohađa navedeni razred: ', pRazred,' ''', pOdjeljenje,''' ', pŠkolskaGodina);
	SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = poruka;
end if;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-12-15 14:43:25

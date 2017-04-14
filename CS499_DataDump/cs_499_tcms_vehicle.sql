CREATE DATABASE  IF NOT EXISTS `cs_499_tcms` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `cs_499_tcms`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: cs_499_tcms
-- ------------------------------------------------------
-- Server version	5.7.17-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `vehicle`
--

DROP TABLE IF EXISTS `vehicle`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `vehicle` (
  `VehicleID` int(11) NOT NULL AUTO_INCREMENT,
  `Brand` varchar(20) CHARACTER SET latin1 DEFAULT NULL,
  `Year` year(4) DEFAULT NULL,
  `Model` varchar(20) CHARACTER SET latin1 DEFAULT NULL,
  `VehicleType` int(20) DEFAULT NULL,
  `Capacity` int(11) DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `CreatedBy` varchar(20) NOT NULL DEFAULT 'Default',
  `LastModifiedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `LastModifiedBy` varchar(20) NOT NULL DEFAULT 'Default',
  `Version` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`VehicleID`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vehicle`
--

LOCK TABLES `vehicle` WRITE;
/*!40000 ALTER TABLE `vehicle` DISABLE KEYS */;
INSERT INTO `vehicle` VALUES (4,'Mercedes-Benz',2004,'Actros',4,17500,'2017-03-26 21:30:09','cs_499_tcms','0000-00-00 00:00:00','cs_499_tcms',1),(5,'Volvo',2017,'VNL 300',7,160000,'2017-03-29 22:58:02','dc0059','0000-00-00 00:00:00','dc0059',1),(6,'Mercedes-Benz',1990,'Quester',3,15558,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(7,'Scania AB',2016,'Actros',5,21851,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(8,'Mitsubishi FUSO',2011,'FH16',5,20018,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(9,'Western Star',2010,'4900',5,20489,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(10,'Volvo',1996,'R-Series',2,11383,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(11,'Scania AB',2011,'P-Series',3,14091,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(12,'Western Star',2010,'S-Series',6,30913,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(13,'Mercedes-Benz',2000,'Shogun',3,14580,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(14,'Mercedes-Benz',2012,'VN',6,28769,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(15,'Scania AB',2007,'FH16',5,21995,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(16,'Volvo',1996,'4900',2,12292,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(17,'Scania AB',1994,'FH16',5,20736,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(18,'Western Star',1991,'S-Series',2,10405,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(19,'Western Star',1996,'Atlas',4,16177,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(20,'Mercedes-Benz',1994,'R-Series',5,23959,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(21,'Scania AB',2006,'FB',4,17587,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(22,'Mitsubishi FUSO',2010,'Quester',3,14708,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(23,'UD',2012,'4900',2,13805,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(24,'Scania AB',2017,'FLC',3,14281,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(25,'Mitsubishi FUSO',2008,'Actros',5,21720,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(26,'Mitsubishi FUSO',1993,'FH16',6,29271,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(27,'Volvo',1993,'P-Series',1,6589,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(28,'Mitsubishi FUSO',2015,'Actros',1,8551,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(29,'Western Star',1992,'Shogun',3,15822,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1),(30,'Mitsubishi FUSO',1999,'R-Series',4,19065,'2017-04-10 13:59:32','ztj0002','0000-00-00 00:00:00','ztj0002',1);
/*!40000 ALTER TABLE `vehicle` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`vehicle_bi` BEFORE INSERT ON cs_499_tcms.vehicle FOR EACH ROW 
BEGIN 
 
SET NEW.CREATEDDATE = CURRENT_TIMESTAMP; 
SET NEW.VERSION = 1; 
 
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`vehicle_bu` BEFORE UPDATE ON cs_499_tcms.vehicle FOR EACH ROW 
BEGIN 
 
SET NEW.VERSION = OLD.VERSION + 1; 
SET NEW.LASTMODIFIEDDATE = CURRENT_TIMESTAMP; 
 
INSERT INTO vehicle_log 
( 
VehicleID,
Brand,
Year,
Model,
VehicleType,
Capacity,
CreatedDate,
CreatedBy,
LastModifiedDate,
LastModifiedBy,
Version,
ModifiedStatus 
) 
VALUES 
( 
OLD.VehicleID,
OLD.Brand,
OLD.Year,
OLD.Model,
OLD.VehicleType,
OLD.Capacity,
OLD.CreatedDate,
OLD.CreatedBy,
OLD.LastModifiedDate,
OLD.LastModifiedBy,
OLD.Version,
'U'
); 
 
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`vehicle_bd` BEFORE DELETE ON cs_499_tcms.vehicle FOR EACH ROW 
BEGIN 
INSERT INTO vehicle_log 
( 
VehicleID,
Brand,
Year,
Model,
VehicleType,
Capacity,
CreatedDate,
CreatedBy,
LastModifiedDate,
LastModifiedBy,
Version,
ModifiedStatus,
DeletedDate 
) 
VALUES 
( 
OLD.VehicleID,
OLD.Brand,
OLD.Year,
OLD.Model,
OLD.VehicleType,
OLD.Capacity,
OLD.CreatedDate,
OLD.CreatedBy,
OLD.LastModifiedDate,
OLD.LastModifiedBy,
OLD.Version,
'D',
CURRENT_TIMESTAMP 
); 
 
END */;;
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

-- Dump completed on 2017-04-14  2:15:28
